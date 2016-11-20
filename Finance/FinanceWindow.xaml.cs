using Finance.ViewModels;
using FinanceModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
