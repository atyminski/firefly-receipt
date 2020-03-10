using PropertyChanged;

namespace Gevlee.FireflyReceipt.Application.Models
{
    [AddINotifyPropertyChangedInterface]
    [Equals(DoNotAddEqualityOperators = true)]
    public class ReceiptsListItem
    {
        public string Path { get; set; }

        [AlsoNotifyFor(nameof(IsAlreadyAssigned))]
        public long? TransactionId { get; set; }

        public bool IsAlreadyAssigned => TransactionId.HasValue;
    }
}
