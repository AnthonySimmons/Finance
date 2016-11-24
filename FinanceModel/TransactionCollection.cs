using FinanceModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FinanceModel
{
    public class TransactionCollection :  ObservableCollection<Transaction>, IList<Transaction>
    {        
        public bool IsReadOnly => false;
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void Add(string description, double amount, DateTime date, string payee)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                Date = date,
                Description = description,
                Payee = payee,
            };
            transaction.PropertyChanged += Transaction_PropertyChanged;
            Add(transaction);

            if(StartDate > date)
            {
                date = StartDate;
            }

            if(EndDate < date)
            {
                date = EndDate;
            }
        }

        private void Transaction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void AddRange(IEnumerable<Transaction> items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }
    }
}
