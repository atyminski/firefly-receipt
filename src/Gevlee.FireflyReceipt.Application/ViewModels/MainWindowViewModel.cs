using System;
using ReactiveUI;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.ReceiptsBrowserModel.SelectedRecipt).Subscribe(receipt => TransactionsListModel.CurrentReceipt = receipt);
        }

        public ReceiptsSearchSettingsViewModel ReceiptsSearchSettingsModel { get; } = new ReceiptsSearchSettingsViewModel();

        public ReceiptsBrowserViewModel ReceiptsBrowserModel { get; } = new ReceiptsBrowserViewModel();

        public TransactionsListViewModel TransactionsListModel { get; } = new TransactionsListViewModel();
    }
}
