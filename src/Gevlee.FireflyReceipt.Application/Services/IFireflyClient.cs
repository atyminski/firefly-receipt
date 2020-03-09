using Gevlee.FireflyReceipt.Models.Firefly;
using System.Threading.Tasks;

namespace Gevlee.FireflyReceipt.Application.Services
{
    public interface IFireflyClient
    {
        Task<GetAttatchmentsResponse> GetAttatchementsAsync(int? page = null);
    }
}