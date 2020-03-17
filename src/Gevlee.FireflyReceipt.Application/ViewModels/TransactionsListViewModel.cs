using Gevlee.FireflyReceipt.Application.Models;
using Gevlee.FireflyReceipt.Application.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static Gevlee.FireflyReceipt.Application.Models.TransactionsListViewModel;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class TransactionsListViewModel : ReactiveObject
    {
        private ObservableCollection<ReceiptTransaction> transactions;
        private ReceiptsListItem currentReceipt;

        public TransactionsListViewModel()
        {
            LoadTransactions();
            this.WhenAnyValue(x => x.CurrentReceipt)
                .Subscribe(_ => RefreshAssignment());
        }

        public ObservableCollection<ReceiptTransaction> Transactions { get => transactions; set => this.RaiseAndSetIfChanged(ref transactions, value); }

        public ReceiptsListItem CurrentReceipt { get => currentReceipt; set => this.RaiseAndSetIfChanged(ref currentReceipt, value); }

        public ReactiveCommand<long, Unit> OnAssign => ReactiveCommand.CreateFromTask<long>(
            AssignReceipt,
            this.WhenAnyValue(
                x => x.CurrentReceipt,
                x => x.CurrentReceipt.IsAlreadyAssigned)
                .Select(r => !r.Item1.TransactionId.HasValue));

        private async Task AssignReceipt(long arg)
        {
            var service = Locator.Current.GetService<IAttachmentService>();
            await service.AssignReceipt(CurrentReceipt.Path, arg);
            CurrentReceipt.TransactionId = arg; //not modifies collection item but immutable copy - to fix
            RefreshAssignment();
        }

        private void LoadTransactions()
        {
            var service = Locator.Current.GetService<ITransactionService>();
            Observable.FromAsync(service.GetFlatTransactions)
                .Subscribe(result =>
                    {
                        Transactions = new ObservableCollection<ReceiptTransaction>(result.Select(ReceiptTransaction.FromFlatTransaction).ToList());
                        RefreshAssignment();
                    });
        }

        private void RefreshAssignment()
        {
            foreach(var transaction in Transactions ?? new ObservableCollection<ReceiptTransaction>())
            {
                transaction.HasAssignedReceipt = CurrentReceipt != null && CurrentReceipt.TransactionId.HasValue && CurrentReceipt.TransactionId == transaction.Id;
            }
        }
    }
}