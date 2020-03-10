using Gevlee.FireflyReceipt.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gevlee.FireflyReceipt.Application.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<FlatTransaction>> GetFlatTransactions();
    }
}