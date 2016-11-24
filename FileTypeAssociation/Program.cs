using System;
using System.IO;
using Logger;

namespace FileTypeAssociation
{
    class Program
    {
        private static void CreateFileTypeAssociation(string exeName, string exePath, string fileTypeExt, string iconPath)
        {
            FileTypeAssociation fileType = new FileTypeAssociation(fileTypeExt);
            fileType.Create(exeName, exePath, iconPath);
        }
        
        private static void PrintUsage()
        {
            string usage = @"FileTypeAssociation.exe <ExeName> <ExePath> <FileTypeExtension> <IconPath>";
            Console.WriteLine(usage);
        }

        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Logs.Instance.Log($"Insufficient number of arguments: {args.Length}");
                Environment.Exit(1);
            }
            if (!File.Exists(args[1]))
            {
                Logs.Instance.Log($"Invalid Exe Path: {args[1]}");
                Environment.Exit(2);
            }
            if(!File.Exists(args[3]))
            {
                Logs.Instance.Log($"Invalid Icon Path: {args[3]}");
                Environment.Exit(2);
            }

            Logs.Instance.Log(string.Join(" ", args));

            try
            {
                CreateFileTypeAssociation(args[0], args[1], args[2], args[3]);
            }
            catch (Exception ex)
            {
                Logs.Instance.Log("Exception");
                Logs.Instance.Log(ex);
                Environment.Exit(3);
            }
            Logs.Instance.Log($"Successfully created filetype associations for {args[0]} {args[2]}");

        }
    }
}
