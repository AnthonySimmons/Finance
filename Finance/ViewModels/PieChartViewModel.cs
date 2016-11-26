using Finance.DataFilters;
using FinanceModel.Models;
using System.Windows;

namespace Finance.ViewModels
{
    public class PieChartViewModel : ChartViewModel
    {
        public PieChartViewModel(DataModel dataModel)
            : base(dataModel)
        {

        }

        protected override void LoadDataPoints()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DataFilter filter = GetFilter(ReportType);
                DataPoints.Clear();
                var dataPoints = filter.Filter<PieChartDataPoint>(_dataModel.Transactions, StartDate, EndDate);
                foreach (var dataPoint in dataPoints)
                {
                    DataPoints.Add(dataPoint);
                }
            });
        }
    }
}
