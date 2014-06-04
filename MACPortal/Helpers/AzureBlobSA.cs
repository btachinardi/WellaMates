using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using MACPortal.Controllers;
using Microsoft.Ajax.Utilities;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using WellaMates.DAL;
using WellaMates.Models;
using File = WellaMates.Models.File;

namespace MACPortal.Helpers
{
    public class AzureBlobSA {

        public const string TEST_CONTAINER = "productimages";
        public const string REFUND_FILES_CONTAINER = "refundfiles";
        public const string TWO_MINUTE_POLICY = "twominutepolicy";
        public const string THIRTY_MINUTE_POLICY = "thirtyminutepolicy";

        public static IEnumerable<IListBlobItem> GetContainerFiles(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
            var storageClient = storageAccount.CreateCloudBlobClient();
            var storageContainer = storageClient.GetContainerReference(containerName);
            return storageContainer.ListBlobs();

        }

        public static string UploadImage(string containerName, Stream stream, string fileName, string contentType)
        {
            var storageAccount = CloudStorageAccount.Parse(
                    ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);

            var blobStorage = storageAccount.CreateCloudBlobClient();
            var container = blobStorage.GetContainerReference(containerName);
            container.CreateIfNotExist();
            var uniqueBlobName = string.Format("{0}/{1}{2}",
                    containerName, Guid.NewGuid(), Path.GetExtension(fileName));
            var blob = blobStorage.GetBlockBlobReference(uniqueBlobName);
            blob.Properties.ContentType = contentType;
            blob.UploadFromStream(stream);
            return blob.Name;
        }

        public static string GetSasUrl(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExist();
            var containerPermissions = new BlobContainerPermissions();
            containerPermissions.SharedAccessPolicies.Add(
              TWO_MINUTE_POLICY, new SharedAccessPolicy()
              {
                  SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-1),
                  SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(2),
                  Permissions = SharedAccessPermissions.Read
              });
            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Off;
            container.SetPermissions(containerPermissions);
            var sas = container.GetSharedAccessSignature(new SharedAccessPolicy(), TWO_MINUTE_POLICY);
            return sas;
        }

        public static string GetSasBlobUrl(string containerName, string fileName, string sas)
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
            var sasBlobClient = new CloudBlobClient(storageAccount.BlobEndpoint,
                new StorageCredentialsSharedAccessSignature(sas));
            var blob = sasBlobClient.GetBlobReference(containerName + @"/" + fileName);
            return blob.Uri.AbsoluteUri + sas;
        }

        static public string EncodeTo64(string toEncode)
        {
            var toEncodeAsBytes = System.Text.Encoding.ASCII.GetBytes(toEncode);
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static public string DecodeFrom64(string encodedData)
        {
            var encodedDataAsBytes = Convert.FromBase64String(encodedData);
            var returnValue = System.Text.Encoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        private static List<File> _ProcessedFiles = new List<File>(); 
        public static IEnumerable<WellaMates.Models.File> ProcessFiles(IEnumerable<WellaMates.Models.File> files)
        {
            var storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(AzureBlobSA.REFUND_FILES_CONTAINER);

            var containerPermissions = new BlobContainerPermissions();
            containerPermissions.SharedAccessPolicies.Add(
              AzureBlobSA.THIRTY_MINUTE_POLICY, new SharedAccessPolicy()
              {
                  SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                  SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(30),
                  Permissions = SharedAccessPermissions.Read
              });
            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Off;
            container.SetPermissions(containerPermissions);
            var sas = container.GetSharedAccessSignature(new SharedAccessPolicy(), AzureBlobSA.THIRTY_MINUTE_POLICY);
            var sasBlobClient = new CloudBlobClient(storageAccount.BlobEndpoint,
                new StorageCredentialsSharedAccessSignature(sas));

            var processFiles = files as File[] ?? files.ToArray();
            foreach (var file in processFiles)
            {
                File oldFile = _ProcessedFiles.FirstOrDefault(f => f.FileID == file.FileID);
                if (oldFile != null)
                {
                    file.FilePath = oldFile.FilePath;
                }
                else
                {
                    file.FilePath =
                       sasBlobClient.GetBlobReference(AzureBlobSA.REFUND_FILES_CONTAINER + @"/" + file.FilePath)
                           .Uri.AbsoluteUri + sas;
                    _ProcessedFiles.Add(file);
                }
                
            }
            return processFiles;
        }
    }
}