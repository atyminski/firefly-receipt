using Gevlee.FireflyReceipt.Application.Models.Firefly;
using System.Threading.Tasks;

namespace Gevlee.FireflyReceipt.Application.Services
{
    public interface IFireflyClient
    {
        Task<GetAttatchmentsResponse> GetAttatchementsAsync(int? page = null);
        Task<GetTransactionsResponse> GetTransactionsAsync(int? page);
        Task<CreateAttachmentResponse> CreateAttachmentAsync(CreateAttachmentRequest requestModel);
        Task UploadAttachment(long attachmentId, byte[] fileBytes, string fileName);
    }
}