using Gevlee.FireflyReceipt.Application.Models;
using ReactiveUI;
using System.Collections.Generic;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class TransactionsListViewModel : ReactiveObject
    {
        private IEnumerable<Transaction> transactions;

        public TransactionsListViewModel()
        {
            LoadTransactions();
        }

        public IEnumerable<Transaction> Transactions { get => transactions; set => this.RaiseAndSetIfChanged(ref transactions, value); }

        private void LoadTransactions()
        {
            Transactions = new Transaction[] 
            { 
                new Transaction { Amount = (decimal)2.23, Name = "TEST Transaction 1", Currency = "PLN" },
                new Transaction { Amount = (decimal)5.99, Name = "TEST Transaction 2", Currency = "PLN" },
            };
        }
    }
}
