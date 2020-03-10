using Newtonsoft.Json;

namespace Gevlee.FireflyReceipt.Application.Models.Firefly
{
    public class BaseResponse<TAttributes>
    {
        [JsonProperty("data")]
        public Datum<TAttributes>[] Data { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("links")]
        public ResponseLinks Links { get; set; }
    }
}
