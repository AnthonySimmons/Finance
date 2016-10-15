using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceModel
{
    interface ITransactionLoader
    {

        IEnumerable<Transaction> Load(string filePath);
    }
}
