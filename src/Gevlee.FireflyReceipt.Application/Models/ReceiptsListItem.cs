using PropertyChanged;

namespace Gevlee.FireflyReceipt.Application.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ReceiptsListItem
    {
        public string Path { get; set; }

        public bool IsAlreadyAssigned { get; set; }
    }
}
