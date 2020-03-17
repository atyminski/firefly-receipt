using Newtonsoft.Json;

namespace Gevlee.FireflyReceipt.Application.Models.Firefly
{
    public class CreateAttachmentRequest
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("model")]
        public string AttachableType { get; set; }

        [JsonProperty("model_id")]
        public long AttachableId { get; set; }

        //[JsonProperty("md5")]
        //public string Md5 { get; set; }

        //[JsonProperty("download_uri")]
        //public Uri DownloadUri { get; set; }

        //[JsonProperty("upload_uri")]
        //public Uri UploadUri { get; set; }

        //[JsonProperty("title")]
        //public string Title { get; set; }

        //[JsonProperty("notes")]
        //public string Notes { get; set; }
    }
}
