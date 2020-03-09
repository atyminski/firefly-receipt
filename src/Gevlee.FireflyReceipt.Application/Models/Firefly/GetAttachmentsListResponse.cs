namespace Gevlee.FireflyReceipt.Models.Firefly
{
    using System;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class GetAttatchmentsResponse
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("links")]
        public GetAttatchmentsResponseLinks Links { get; set; }
    }

    public class Datum
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("links")]
        public DatumLinks Links { get; set; }
    }

    public class Attributes
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

    public class DatumLinks
    {
        [JsonProperty("0")]
        public The0 The0 { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public class The0
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class GetAttatchmentsResponseLinks
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("prev")]
        public string Prev { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }

    public class Meta
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("current_page")]
        public long CurrentPage { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }
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
