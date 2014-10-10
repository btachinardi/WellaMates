

using System.Collections.Generic;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace WellaMates.App_Start
{
    public class CorsConfig
    {
        public static void Configure()
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
            var client = storageAccount.CreateCloudBlobClient();

            var blobServiceProperties = client.GetServiceProperties();

            // Enable and Configure CORS
            ConfigureCors(blobServiceProperties);

            // Commit the CORS changes into the Service Properties
            client.SetServiceProperties(blobServiceProperties);
        }

        private static void ConfigureCors(ServiceProperties serviceProperties)
        {
            serviceProperties.Cors = new CorsProperties();
            serviceProperties.Cors.CorsRules.Add(new CorsRule()
            {
                AllowedHeaders = new List<string>() { "*" },
                AllowedMethods = CorsHttpMethods.Put | CorsHttpMethods.Get | CorsHttpMethods.Head | CorsHttpMethods.Post,
                AllowedOrigins = new List<string>() { "*" },
                ExposedHeaders = new List<string>() { "Access-Control-Allow-Origin" },
                MaxAgeInSeconds = 1800 // 30 minutes
            });
        }
    }
}