using System;
using Gevlee.FireflyReceipt.Application.Models.Firefly;
using Newtonsoft.Json;

namespace Gevlee.FireflyReceipt.Application.Models.Firefly
{
    public class GetAttatchmentsResponse : BaseListResponse<AttatchmentsAttributes>
    {
    }

    public class AttatchmentsAttributes
    {
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("attachable_id")]
        public int AttachableId { get; set; }

        [JsonProperty("attachable_type")]
        public string AttachableType { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("download_uri")]
        public string DownloadUri { get; set; }

        [JsonProperty("upload_uri")]
        public string UploadUri { get; set; }

        [JsonProperty("title")]
        public object Title { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }

        [JsonProperty("mime")]
        public string Mime { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }
    }

    //public partial class GetAttatchmentsResponse
    //{
    //    public static GetAttatchmentsResponse FromJson(string json) => JsonConvert.DeserializeObject<GetAttatchmentsResponse>(json, Gevlee.FireflyReceipt.Models.Firefly.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this GetAttatchmentsResponse self) => JsonConvert.SerializeObject(self, Gevlee.FireflyReceipt.Models.Firefly.Converter.Settings);
    //}

    //internal static class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters =
    //        {
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
