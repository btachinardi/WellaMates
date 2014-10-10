using System.Net.Http;

namespace WellaMates.DAL
{
    public class ImageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public ImageMultipartFormDataStreamProvider(string path)
            : base(path)
        {}
 
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Replace("\"",string.Empty); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }
    }
}