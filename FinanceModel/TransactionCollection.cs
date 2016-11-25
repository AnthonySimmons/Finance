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

        public event EventHandler TransactionsChanged;

        public TransactionCollection()
        {

        }

        
        public new void Add(Transaction transaction)
        {
            transaction.PropertyChanged += Transaction_PropertyChanged;
            base.Add(transaction);
            OnTransactionsChanged();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            
            if (StartDate > transaction.Date)
            {
                StartDate = transaction.Date;
            }

            if (EndDate < transaction.Date)
            {
                EndDate = transaction.Date;
            }
        }

        private void Transaction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnTransactionsChanged();
        }

        protected virtual void OnTransactionsChanged()
        {
            TransactionsChanged?.Invoke(this, EventArgs.Empty);
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
