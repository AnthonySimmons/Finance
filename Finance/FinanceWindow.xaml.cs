using Finance.ViewModels;
using FinanceModel.Settings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Finance
{
    /// <summary>
    /// Interaction logic for FinanceWindow.xaml
    /// </summary>
    public partial class FinanceWindow : Window
    {

        public static RoutedCommand OpenCommand = new RoutedCommand();

        public static RoutedCommand SaveCommand = new RoutedCommand();       

        public FinanceWindow()
        {
            InitializeComponent();
            Loaded += FinanceWindow_Loaded;
            OpenCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        }


        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFiles();
        }

        private void OpenCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFiles();
        }

        private void FinanceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCommandLineArgs();
        }

        private void LoadCommandLineArgs()
        { 
            string[] args = Environment.GetCommandLineArgs();
            
            if (args.Length > 1)
            {
                var fileNames = new List<string>();
                for(int i = 1; i < args.Length; i++)
                {
                    if(File.Exists(args[i]))
                    {
                        fileNames.Add(args[i]);
                    }
                }

                LoadData(fileNames.ToArray());                
            }
            SetupFileAssociations();
        }

        private void SetupFileAssociations()
        {
            if(!StaticSettings.HasFileTypeAssociations())
            {
                MessageBoxResult result = MessageBox.Show("Always use this application to open .qif files?", "File Type Association", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string exeName = AppDomain.CurrentDomain.FriendlyName;
                    string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
                    string iconPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "Icon.bmp");
                    string errors = StaticSettings.CreateFileTypeAssociations(exeName, exePath, iconPath);
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        MessageBox.Show(errors);
                    }
                }
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFiles();
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFiles();
        }

        private void OpenFiles()
        { 
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.ShowDialog();
            LoadData(fileDialog.FileNames);
        }
              

        private void LoadData(string [] fileNames)
        { 
            ApplicationViewModel appViewModel = (ApplicationViewModel)DataContext;
            appViewModel.LoadData(fileNames);
        }

        private void SaveFiles()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = ".qif|Quicken Interexchange Format";
            
            fileDialog.DefaultExt = ".qif"; 
            fileDialog.ShowDialog();
            SaveData(fileDialog.FileName);
        }

        private void SaveData(string fileName)
        {
            ApplicationViewModel appViewModel = (ApplicationViewModel)DataContext;
            appViewModel.SaveData(fileName);
        }
                       
    }
}
