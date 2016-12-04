

using FinanceModel.Models;
using System.Linq;

namespace FinanceModel.Transfers
{
    public class TransferManager
    {
        public static string[] TransferDescriptions = new string[]
        {
            @"TRANSFER",
            @"AUTHORIZED PMT BANK OF AMERI",
        };

        public static bool IsTransfer(Transaction transaction)
        {
            return TransferDescriptions.Any(d => transaction.Description.Contains(d) || transaction.Payee.Contains(d));
        }


    }
}
