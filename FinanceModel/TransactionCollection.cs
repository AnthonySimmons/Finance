using FinanceModel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceModel
{
    public class TransactionCollection : IList<Transaction>
    {
        private IList<Transaction> Items { get; set; } = new List<Transaction>();

        public double RunningTotal { get; set; }

        public int Count => Items.Count;

        public bool IsReadOnly => false;

        public Transaction this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        public void Add(string description, double amount, DateTime date, TransactionType type, string payee)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                DateTime = date,
                Description = description,
                TransactionType = type,
                Payee = payee,
            };
            Add(transaction);
        }

        public void Add(Transaction transaction)
        {
            RunningTotal += transaction.Amount;
            Items.Add(transaction);
        }

        public int IndexOf(Transaction item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, Transaction item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(Transaction item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(Transaction[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public bool Remove(Transaction item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<Transaction> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
