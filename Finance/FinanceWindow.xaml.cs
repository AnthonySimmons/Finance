using Finance.ViewModels;
using FinanceModel;
using FinanceModel.Settings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Transaction = FinanceModel.Models.Transaction;

namespace Finance
{
    /// <summary>
    /// Interaction logic for FinanceWindow.xaml
    /// </summary>
    public partial class FinanceWindow : Window
    {
        
        public FinanceWindow()
        {
            InitializeComponent();
            Loaded += FinanceWindow_Loaded;
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

                var dataFiles = fileNames.ToArray();
                
                LoadLineChart(dataFiles);
                LoadPieChart(dataFiles);
                LoadDataGrid(dataFiles);
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

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.InitialDirectory = Config.DataDirectoryPath;
            fileDialog.ShowDialog();

            LoadLineChart(fileDialog.FileNames);
            LoadPieChart(fileDialog.FileNames);
            LoadDataGrid(fileDialog.FileNames);
        }

        private LineChartViewModel LoadLineChart(string [] filePaths)
        {
            Grid grid = (Grid)FindName("LineChartGrid");
            LineChartViewModel viewModel = (LineChartViewModel)grid.DataContext;
            viewModel.LoadDataFromFiles(filePaths);
            return viewModel;
        }

        private void LoadPieChart(string [] filePaths)
        {
            Grid grid = (Grid)FindName("PieChartGrid");
            PieChartViewModel viewModel = (PieChartViewModel)grid.DataContext;
            viewModel.LoadDataFromFiles(filePaths);
        }

        private void LoadDataGrid(string [] filePaths)
        {
            DataGrid grid = (DataGrid)FindName("DataViewGrid");
            DataGridViewModel viewModel = (DataGridViewModel)grid.DataContext;
            viewModel.LoadDataGridFromFile(filePaths);
        }
                       
    }
}
