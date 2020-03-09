namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReceiptsSearchSettingsViewModel ReceiptsSearchSettingsModel { get; } = new ReceiptsSearchSettingsViewModel();

        public ReceiptsBrowserViewModel ReceiptsBrowserModel { get; } = new ReceiptsBrowserViewModel();

        public TransactionsListViewModel TransactionsListModel { get; } = new TransactionsListViewModel();
    }
}
