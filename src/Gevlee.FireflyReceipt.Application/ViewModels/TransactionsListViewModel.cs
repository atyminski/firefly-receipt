using Gevlee.FireflyReceipt.Application.Models;
using Gevlee.FireflyReceipt.Application.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using static Gevlee.FireflyReceipt.Application.Models.TransactionsListViewModel;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public partial class TransactionsListViewModel : ReactiveObject
    {
        private ObservableCollection<ReceiptTransaction> transactions;
        private ReceiptsListItem currentReceipt;

        public TransactionsListViewModel()
        {
            LoadTransactions();
            this.WhenAnyValue(x => x.CurrentReceipt)
                .Subscribe(_ => OnCurrentReceiptChanged());
        }

        public ObservableCollection<ReceiptTransaction> Transactions { get => transactions; set => this.RaiseAndSetIfChanged(ref transactions, value); }

        public ReceiptsListItem CurrentReceipt { get => currentReceipt; set => this.RaiseAndSetIfChanged(ref currentReceipt, value); }

        private void LoadTransactions()
        {
            var service = Locator.Current.GetService<ITransactionService>();
            Observable.FromAsync(service.GetFlatTransactions)
                .Subscribe(result =>
                    {
                        Transactions = new ObservableCollection<ReceiptTransaction>(result.Select(ReceiptTransaction.FromFlatTransaction).ToList());
                        OnCurrentReceiptChanged();
                    });
        }

        private void OnCurrentReceiptChanged()
        {
            foreach(var transaction in Transactions ?? new ObservableCollection<ReceiptTransaction>())
            {
                transaction.HasAssignedReceipt = CurrentReceipt != null && CurrentReceipt.TransactionId.HasValue && CurrentReceipt.TransactionId == transaction.Id;
            }
        }
    }
}