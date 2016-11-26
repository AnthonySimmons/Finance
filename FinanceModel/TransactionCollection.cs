using FinanceModel.Models;
using System;
using System.Collections.Generic;

namespace FinanceModel
{
    public class TransactionCollection : List<Transaction>
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
            
            if (StartDate > transaction.Date && transaction.Date > DateTime.MinValue)
            {
                StartDate = transaction.Date;
            }

            if (EndDate < transaction.Date)
            {
                EndDate = transaction.Date;
            }
        }
        
        public new void Remove(Transaction transaction)
        {
            transaction.PropertyChanged -= Transaction_PropertyChanged;
            base.Remove(transaction);
        }

        public new void Clear()
        {
            foreach(var transaction in this)
            {
                transaction.PropertyChanged -= Transaction_PropertyChanged;
            }
            base.Clear();
        }

        private void Transaction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnTransactionsChanged();
        }

        protected virtual void OnTransactionsChanged()
        {
            TransactionsChanged?.Invoke(this, EventArgs.Empty);
        }

        public new void AddRange(IEnumerable<Transaction> items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }
    }
}
