using PropertyChanged;

namespace Gevlee.FireflyReceipt.Application.Models
{
    [AddINotifyPropertyChangedInterface]
    [Equals(DoNotAddEqualityOperators = true)]
    public class FlatTransaction
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public string Currency { get; set; }
    }
}
