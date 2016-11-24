
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FinanceModel.Settings
{
    public static class StaticSettings
    {
        public const string QifFileExtension = ".qif";

        public static string CreateFileTypeAssociations(string exeName, string exePath)
        {
            string errors = string.Empty;
            
            try
            {   
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "FileTypeAssociation.exe";
                proc.StartInfo.RedirectStandardError = false;
                proc.StartInfo.ErrorDialog = false;
                proc.StartInfo.Verb = "runas";
                
                //FileTypeAssociation.exe <ExeName> <ExePath> <FileTypeExtension>
                proc.StartInfo.Arguments = $"\"{exeName}\" \"{exePath}\" \"{QifFileExtension}\"";
                proc.Start();
                proc.WaitForExit();

                int exitCode = proc.ExitCode;
                if(exitCode != 0)
                {
                    errors = $"Unexpected Exit Code: {exitCode}";
                }
            }
            catch (Exception ex)
            {
                errors = ex.Message;
            }
            return errors;
        }

        public static bool HasFileTypeAssociations()
        {
            FileTypeAssociation.FileTypeAssociation fileTypeAssociation = new FileTypeAssociation.FileTypeAssociation(QifFileExtension);
            return fileTypeAssociation.Exists;            
        }
    }
}
