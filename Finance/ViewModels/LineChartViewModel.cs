using Finance.DataFilters;
using FinanceModel.Models;
using System.Collections.Generic;
using System.Windows;

namespace Finance.ViewModels
{

    public class LineChartViewModel : ChartViewModel
    {           
        public LineChartViewModel(DataModel dataModel)
               : base(dataModel)
        {

        }

        protected override void LoadDataPoints()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DataFilter filter = GetFilter(ReportType);
                DataPoints.Clear();

                var dataPoints = new List<LineChartDataPoint>();

                //Add an empty data point to start at the origin.
                dataPoints.Add(new LineChartDataPoint { X = StartDate, Y = 0, });

                dataPoints.AddRange(filter.Filter<LineChartDataPoint>(_dataModel.Transactions, StartDate, EndDate));
                foreach (var dataPoint in dataPoints)
                {
                    DataPoints.Add(dataPoint);
                }
            });
        }     

    }
}
