using Gevlee.FireflyReceipt.Application.Models.Firefly;
using Gevlee.FireflyReceipt.Application.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;
using System.Net;
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

        public async Task<GetTransactionsResponse> GetTransactionsAsync(int? page)
        {
            var request = new RestRequest("api/v1/transactions", Method.GET, DataFormat.Json);
            request.AddParameter("page", page);

            try
            {
                return await _restClient.GetAsync<GetTransactionsResponse>(request);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<CreateAttachmentResponse> CreateAttachmentAsync(CreateAttachmentRequest requestModel)
        {
            var request = new RestRequest("api/v1/attachments", Method.POST, DataFormat.Json);
            request.AddJsonBody(requestModel);

            try
            {
                return await _restClient.PostAsync<CreateAttachmentResponse>(request);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task UploadAttachment(long attachmentId, byte[] fileBytes, string fileName)
        {
            var request = new RestRequest("api/v1/attachments/{id}/upload", Method.POST);
            request.AddParameter("id", attachmentId, ParameterType.UrlSegment);
            request.AddParameter("application/octet-stream", fileBytes, ParameterType.RequestBody);

            try
            {
                var result = await _restClient.ExecuteAsync(request);
                if (result.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new System.Exception("Invalid response code != 204");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private class JsonNetSerializer : IRestSerializer
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
