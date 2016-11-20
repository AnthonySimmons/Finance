using System;
using System.IO;
using System.Collections.Generic;
using Hazzik.Qif;
using Hazzik.Qif.Transactions;

namespace FinanceModel
{
    public class QuickenTransactionLoader : ITransactionLoader
    {
        public TransactionCollection Load(string filePath)
        {
            QifDocument doc = GetQif(filePath);

            return GetExpenseModels(doc);
        }
        
        private QifDocument GetQif(string filePath)
        {
            QifDocument doc;
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                doc = QifDocument.Load(stream);
            }
            return doc;
        }

        protected TransactionCollection GetExpenseModels(QifDocument doc)
        {
            var expenses = new TransactionCollection();

            AddTransactions(doc.BankTransactions, expenses);
            AddTransactions(doc.CreditCardTransactions, expenses);

            return expenses;
        }

        private void AddTransactions(IEnumerable<BasicTransaction> source, TransactionCollection target)
        {
            double total = 0;

            foreach (var transaction in source)
            {
                double amount = (double)transaction.Amount;
                total += amount;
                target.Add(transaction.Memo, amount, transaction.Date, transaction.Payee);
            }
        }
             

    }

}
