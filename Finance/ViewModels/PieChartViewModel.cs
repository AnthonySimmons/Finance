using Finance.DataFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.ViewModels
{
    public class PieChartViewModel : ChartViewModel
    {
        public PieChartViewModel() : base()
        {

        }

        protected override void LoadDataPoints()
        {
            DataFilter filter = GetFilter(ReportType);
            DataPoints.Clear();
            var dataPoints = filter.Filter<PieChartDataPoint>(_dataModel.Transactions, StartDate, EndDate);
            foreach (var dataPoint in dataPoints)
            {
                DataPoints.Add(dataPoint);
            }
        }
    }
}
