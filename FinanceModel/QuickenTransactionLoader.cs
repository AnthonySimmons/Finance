using System.IO;
using System.Collections.Generic;
using Hazzik.Qif;
using Hazzik.Qif.Transactions;
using System;
using Transaction = FinanceModel.Models.Transaction;
using FinanceModel.Models;

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

            AddTransactions(doc.BankTransactions, expenses, () => new BankTransaction());
            AddTransactions(doc.CreditCardTransactions, expenses, () => new CreditCardTransaction());

            return expenses;
        }

        private void AddTransactions(IEnumerable<BasicTransaction> source, TransactionCollection target, Func<Transaction> transactionFactory)
        {
            foreach (var transaction in source)
            {
                target.Add(transaction.Memo, transaction.Amount, transaction.Date, transaction.Payee, transactionFactory);
            }
        }
             

    }

}
