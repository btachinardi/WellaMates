using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Codeplex.Data;
using MACPortal.DAL;
using MACPortal.Helpers;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using WellaMates.DAL;
using WellaMates.Filters;

namespace MACPortal.Controllers
{

    [RedirectToAgreeToTerms]
    [AuthorizeUser(Roles = "Member")]
    public class FilesController : ApiController
    {
        /// <summary>
        /// This action is called when the browser supports FileReader
        /// It will return a JSON object to the client.
        /// It will return 'applicaiton/json' as the content type
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("FineUpload")]
        public async Task<FineUpload> FineUpload()
        {
            var fineUpload = await ProcessData();
            return fineUpload;
        }

        /// <summary>
        /// This action is called when the browser doesn't support FileReader
        /// It will always return an Ok and it will include the JSON object 
        /// in text/plain content type.
        /// The method is called IE9 because IE9 cannot handle 'application/json' content type with Fine Uploader
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("FineUploadIe9")]
        public async Task<HttpResponseMessage> FineUploadIe9()
        {    
            var fineUpload = await ProcessData();

            var res = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(DynamicJson.Serialize(fineUpload), Encoding.UTF8, "text/plain")
            };
            return res;
        }

        

        /// <summary>
        /// This method will actually save the the file to Azure
        /// </summary>
        /// <returns></returns>
        private async Task<FineUpload> ProcessData()
        {
            var fineUpload = new FineUpload();
            try
            {
                var provider = new ImageMultipartFormDataStreamProvider(Path.GetTempPath());              
                await Request.Content.ReadAsMultipartAsync(provider);

                var storageAccount = CloudStorageAccount.Parse(
                            ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
                var blobStorage = storageAccount.CreateCloudBlobClient();
                var container = blobStorage.GetContainerReference(AzureBlobSA.REFUND_FILES_CONTAINER);

                foreach (var fileData in provider.FileData)
                {
                    var value = fileData.Headers.ContentDisposition;
                    var name = value.Name;
                    var info = new FileInfo(fileData.LocalFileName);

                    if (name == "\"qqfile\"")
                    {
                        var fileName = info.Name;
                        using (var fileStream = File.OpenRead(fileData.LocalFileName))
                        {
                            var uniqueBlobName = string.Format("{0}/{1}{2}",
                                    AzureBlobSA.REFUND_FILES_CONTAINER, Guid.NewGuid(), Path.GetExtension(fileName));

                            var blob = blobStorage.GetBlockBlobReference(uniqueBlobName);
                            blob.Properties.ContentType = fileData.Headers.ContentType.MediaType;
                            blob.UploadFromStream(fileStream);
                            fineUpload.newUuid = blob.Name;
                            fineUpload.preventRetry = true;
                        }
                    }

                    File.Delete(fileData.LocalFileName);
                }
                fineUpload.success = true;
            }
            catch (Exception e)
            {
                fineUpload.success = false;
                fineUpload.error = e.Message;
            }
            return fineUpload;
        }

        /// <summary>
        /// This action is called when the browser supports FileReader
        /// It will return a JSON object to the client.
        /// It will return 'application/json' as the content type
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("FineDelete")]
        public HttpResponseMessage FineDelete()
        {
            var response = DeleteData();
            var res = new HttpResponseMessage(response);
            return res;
        }

        /// <summary>
        /// This action is called when the browser doesn't support FileReader
        /// It will always return an Ok and it will include the JSON object 
        /// in text/plain content type.
        /// The method is called IE9 because IE9 cannot handle 'application/json' content type with Fine Uploader
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("FineDeleteIe9")]
        public HttpResponseMessage FineDeleteIe9(string id)
        {
            var response = DeleteData();
            var res = new HttpResponseMessage(response);
            return res;
        }

        private HttpStatusCode DeleteData()
        {
            HttpStatusCode status;
            var httpContext = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            var targetId = httpContext.Request.Form["qquuid"];
            try
            {
                var storageAccount = CloudStorageAccount.Parse(
                            ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
                var blobStorage = storageAccount.CreateCloudBlobClient();
                var container = blobStorage.GetContainerReference(AzureBlobSA.REFUND_FILES_CONTAINER);
                var blockBlob = container.GetBlockBlobReference(targetId);
                blockBlob.Delete();
                status = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                status = HttpStatusCode.InternalServerError;
            }
            return status;
        }


        /// <summary>
        /// This action is called when the browser supports FileReader
        /// It will return a JSON object to the client.
        /// It will return 'application/json' as the content type
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("RefundItemSession")]
        public FineUploadSession[] RefundItemSession(int id, DateTime? qqtimestamp)
        {
            var session = GetRefundItemSession(id);
            return session;
        }

        /// <summary>
        /// This action is called when the browser doesn't support FileReader
        /// It will always return an Ok and it will include the JSON object 
        /// in text/plain content type.
        /// The method is called IE9 because IE9 cannot handle 'application/json' content type with Fine Uploader
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("RefundItemSessionIe9")]
        public HttpResponseMessage RefundItemSessionIe9(int id, DateTime? qqtimestamp)
        {
            var session = GetRefundItemSession(id);

            var res = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(DynamicJson.Serialize(session), Encoding.UTF8, "text/plain")
            };
            return res;
        }

        private FineUploadSession[] GetRefundItemSession(int targetId)
        {
            var session = new List<FineUploadSession>();
            try
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

                using (var db = new PortalContext())
                {
                    var files = db.RefundItems.First(r => r.RefundItemID == targetId).Files;
                    session.AddRange(files.Select(file => new FineUploadSession
                    {
                        name = file.Name,
                        thumbnailUrl = sasBlobClient.GetBlobReference(AzureBlobSA.REFUND_FILES_CONTAINER + @"/" + file.FilePath).Uri.AbsoluteUri + sas,
                        uuid = file.FilePath
                    }));
                }
            }
            catch (Exception e)
            {
                session = null;
            }
            return session == null ? null : session.ToArray();
        }
    }



    public class FineUpload
    {
        public bool success { get; set; }
        public string error { get; set; }
        public string newUuid { get; set; }
        public bool preventRetry { get; set; }
        public string echoAgent { get; set; }
    }

    public class FineUploadSession
    {
        public string name { get; set; }
        public int size { get; set; }
        public string uuid { get; set; }
        public string thumbnailUrl { get; set; }
    }
}
