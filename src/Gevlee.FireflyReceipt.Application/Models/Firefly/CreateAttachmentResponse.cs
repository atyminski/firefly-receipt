using Newtonsoft.Json;
using System;

namespace Gevlee.FireflyReceipt.Application.Models.Firefly
{
    public class CreateAttachmentResponse : BaseResponse<CreateAttachmentResponse.Attributes>
    {
        public class Attributes
        {
            [JsonProperty("created_at")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("updated_at")]
            public DateTimeOffset UpdatedAt { get; set; }

            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("attachable_type")]
            public string AttachableType { get; set; }

            [JsonProperty("attachable_id")]
            public long AttachableId { get; set; }

            [JsonProperty("md5")]
            public string Md5 { get; set; }

            [JsonProperty("download_uri")]
            public Uri DownloadUri { get; set; }

            [JsonProperty("upload_uri")]
            public Uri UploadUri { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("notes")]
            public string Notes { get; set; }

            [JsonProperty("mime")]
            public string Mime { get; set; }

            [JsonProperty("size")]
            public long Size { get; set; }
        }
    }
}
