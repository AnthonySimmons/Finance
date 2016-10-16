using System.IO;
using System;
using FinanceModel;

namespace Finance
{
    public static class Config
    {
        public static string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Finance");
        
        public static string DataDirectoryPath => Path.Combine(AppDataPath, "Data");

        public static string ExpensesDirectoryPath => Path.Combine(DataDirectoryPath, "Expenses");
        

    }
}
