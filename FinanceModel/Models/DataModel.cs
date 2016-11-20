using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace FinanceModel.Models
{
    public class DataModel
    {
        private ITransactionLoader _transactionLoader;

        private TransactionCollection _transactions = new TransactionCollection();

        public DataModel(ITransactionLoader transactionLoader)
        {
            _transactionLoader = transactionLoader;
        }

        public void Clear()
        {
            _transactions.Clear();
        }

        public void LoadDataFromFolder(string dataDirectory)
        {
            try
            {
                var files = Directory.EnumerateFiles(dataDirectory);
                foreach (var file in files)
                {
                    Transactions.AddRange(_transactionLoader.Load(file));
                }
            }
            catch
            {
                //TODO Log.
            }
        }

        public void LoadDataFromFile(string dataFilePath)
        {
            try
            {
                Transactions.AddRange(_transactionLoader.Load(dataFilePath));
            }
            catch
            {

            }
        }

        public TransactionCollection Transactions => _transactions;
     
        public DateTime GetEarliestTransaction()
        {
            DateTime val = DateTime.Now;
            Transaction tran = Transactions.OrderBy(t => t.Date).FirstOrDefault();
            if(tran != null && tran.Date > DateTime.MinValue)
            {
                val = tran.Date;
            }
            return val;
        }
        
        public DateTime GetLatestTransaction()
        {
            DateTime val = DateTime.Now;
            Transaction tran = Transactions.OrderByDescending(t => t.Date).FirstOrDefault();
            if(tran != null)
            {
                val = tran.Date;
            }
            return val;
        }

    }
}
