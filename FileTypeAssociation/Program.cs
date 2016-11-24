using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTypeAssociation
{
    class Program
    {
        private static void CreateFileTypeAssociation(string exeName, string exePath, string fileTypeExt)
        {
            FileTypeAssociation fileType = new FileTypeAssociation(fileTypeExt);
            fileType.Create(exeName, exePath);
        }
        
        private static void PrintUsage()
        {
            string usage = @"FileTypeAssociation.exe <ExeName> <ExePath> <FileTypeExtension>";
            Console.WriteLine(usage);
        }

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.Error.WriteLine($"Insufficient number of arguments: {args.Length}");
                Environment.Exit(1);
            }
            if (!File.Exists(args[1]))
            {
                Console.Error.WriteLine($"Invalid Exe Path: {args[1]}");
                Environment.Exit(2);
            }
            try
            {
                CreateFileTypeAssociation(args[0], args[1], args[2]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{ex.Message}\n{ex.InnerException?.InnerException}");
                Environment.Exit(3);
            }
            Console.Out.WriteLine($"Successfully created filetype associations for {args[0]} {args[2]}");

        }
    }
}
