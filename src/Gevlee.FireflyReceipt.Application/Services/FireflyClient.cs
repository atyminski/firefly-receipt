using Gevlee.FireflyReceipt.Application.Settings;
using Gevlee.FireflyReceipt.Models.Firefly;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;
using RestSharp.Serialization.Json;
using System.Threading.Tasks;

namespace Gevlee.FireflyReceipt.Application.Services
{
    public class FireflyClient : IFireflyClient
    {
        private RestClient _restClient;

        public FireflyClient(IOptions<GeneralSettings> options)
        {
            _restClient = new RestClient(!string.IsNullOrWhiteSpace(options.Value.FireflyUrl) ? options.Value.FireflyUrl.TrimEnd('/') : string.Empty);
            _restClient.UseSerializer(() => new JsonNetSerializer());
            _restClient.Authenticator = new JwtAuthenticator(options.Value.FireflyPersonalAccessToken);
        }

        public async Task<GetAttatchmentsResponse> GetAttatchementsAsync(int? page)
        {
            var request = new RestRequest("api/v1/attachments", Method.GET, DataFormat.Json);
            request.AddParameter("page", page);

            try
            {
                return await _restClient.GetAsync<GetAttatchmentsResponse>(request);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public class JsonNetSerializer : IRestSerializer
        {
            public string Serialize(object obj) =>
                JsonConvert.SerializeObject(obj);

            public string Serialize(Parameter parameter) =>
                JsonConvert.SerializeObject(parameter.Value);

            public T Deserialize<T>(IRestResponse response) =>
                JsonConvert.DeserializeObject<T>(response.Content);

            public string[] SupportedContentTypes { get; } =
            {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

            public string ContentType { get; set; } = "application/json";

            public DataFormat DataFormat { get; } = DataFormat.Json;
        }
    }
}
