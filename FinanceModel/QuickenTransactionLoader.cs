using System.IO;
using System.Collections.Generic;
using Hazzik.Qif;
using Hazzik.Qif.Transactions;
using System;
using Transaction = FinanceModel.Models.Transaction;
using FinanceModel.Models;
using FinanceModel.Transfers;

namespace FinanceModel
{
    public class QuickenTransactionLoader : ITransactionLoader
    {
        public IEnumerable<Transaction> Load(string filePath)
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

        protected IEnumerable<Transaction> GetExpenseModels(QifDocument doc)
        {
            var expenses = new List<Transaction>();

            AddTransactions(doc.BankTransactions, expenses, () => new BankTransaction());
            AddTransactions(doc.CreditCardTransactions, expenses, () => new CreditCardTransaction());

            return expenses;
        }

        private void AddTransactions(IEnumerable<BasicTransaction> source, IList<Transaction> target, Func<Transaction> transactionFactory)
        {
            foreach (var tran in source)
            {
                var transaction = transactionFactory();

                transaction.Amount = tran.Amount;
                transaction.Date = tran.Date;
                transaction.Description = tran.Memo;
                transaction.Payee = tran.Payee;
                transaction.Included = !TransferManager.IsTransfer(transaction);
                target.Add(transaction);
            }
        }
             

    }

}
