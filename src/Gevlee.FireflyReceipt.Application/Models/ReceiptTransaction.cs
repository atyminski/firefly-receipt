using Gevlee.FireflyReceipt.Application.Models;
using PropertyChanged;

namespace Gevlee.FireflyReceipt.Application.Models
{
    public partial class TransactionsListViewModel
    {
        [AddINotifyPropertyChangedInterface]
        public class ReceiptTransaction : FlatTransaction
        {
            public bool HasAssignedReceipt { get; set; }

            public static ReceiptTransaction FromFlatTransaction(FlatTransaction flatTransaction)
            {
                return new ReceiptTransaction
                {
                    Id = flatTransaction.Id,
                    Amount = flatTransaction.Amount,
                    Currency = flatTransaction.Currency,
                    Description = flatTransaction.Description,
                    Type = flatTransaction.Type
                };
            }
        }
    }
}