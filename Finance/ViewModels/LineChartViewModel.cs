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
        
        protected override void LoadDataPoints()
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
        }
     

    }
}
