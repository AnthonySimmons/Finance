
using FinanceModel.Models;
using System.Collections.Generic;

namespace FinanceModel
{
    public interface ITransactionLoader
    {

        IEnumerable<Transaction> Load(string filePath);
    }
}
