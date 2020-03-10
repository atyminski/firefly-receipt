namespace Gevlee.FireflyReceipt.Application.Models.Firefly
{
    using Newtonsoft.Json;

    public class Datum<TAttributes>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("attributes")]
        public TAttributes Attributes { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }


    public class Links
    {
        [JsonProperty("0")]
        public Link Link { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public class Link
    {
        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class ResponseLinks
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
}
