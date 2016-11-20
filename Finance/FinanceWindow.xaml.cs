using Finance.ViewModels;
using FinanceModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            LineChartViewModel viewModel = DataContext as LineChartViewModel;
            if(viewModel != null)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.InitialDirectory = Config.DataDirectoryPath;
                fileDialog.ShowDialog();
                viewModel.LoadDataFromFiles(fileDialog.FileNames);                 
            }
        }
        
    }
}
