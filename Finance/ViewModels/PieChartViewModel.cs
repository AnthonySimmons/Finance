using Finance.DataFilters;
using FinanceModel.Models;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;

namespace Finance.ViewModels
{
    public class PieChartViewModel : ChartViewModel
    {
        public PieChartViewModel(DataModel dataModel)
            : base(dataModel)
        {

        }

        private DataPoint GetDataPoint(string name)
        {
            return DataPoints.FirstOrDefault(dp => (dp as PieChartDataPoint)?.Name == name);
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
                    DataPoint existingDataPoint = GetDataPoint(dataPoint.Name);
                    
                    if (existingDataPoint != null)
                    {
                        PieChartDataPoint newDataPoint = new PieChartDataPoint
                        {
                            Name = dataPoint.Name,
                            Value = ((PieChartDataPoint)existingDataPoint).Value += dataPoint.Value,
                        };

                        DataPoints.Remove(existingDataPoint);
                        DataPoints.Add(newDataPoint);
                    }
                    else
                    {
                        DataPoints.Add(dataPoint);
                    }
                }
            });
        }
    }
}
