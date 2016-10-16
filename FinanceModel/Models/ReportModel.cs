using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceModel.Models
{
    public class ReportModel
    {
        private ITransactionLoader _transactionLoader;

        private TransactionCollection _transactions = new TransactionCollection();

        public ReportModel(ITransactionLoader transactionLoader)
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
        
    }
}
