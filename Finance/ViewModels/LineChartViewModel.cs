using Finance.DataFilters;
using Finance.ViewModels;
using FinanceModel;
using FinanceModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.ViewModels
{

    public class LineChartViewModel : ChartViewModel
    {   

        public LineChartViewModel() : base()
        {
            LoadTransactions();
        }
        

        private void LoadTransactions()
        {
            _dataModel = new ReportModel(new QuickenTransactionLoader());
            LoadDataFromFolder(Config.ExpensesDirectoryPath);
        }
        


        protected override void LoadDataPoints()
        {
            DataFilter filter = GetFilter(ReportType);
            DataPoints.Clear();

            //Add an empty data point to start at the origin.
            var dataPoints = new List<LineChartDataPoint>();

            dataPoints.Add(new LineChartDataPoint());
            dataPoints.AddRange(filter.Filter<LineChartDataPoint>(_dataModel.Transactions, StartDate, EndDate));
            foreach (var dataPoint in dataPoints)
            {
                DataPoints.Add(dataPoint);
            }
        }
     

    }
}
