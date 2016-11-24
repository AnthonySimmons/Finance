using System;

namespace Finance.ViewModels
{
    public class LineChartDataPoint : DataPoint
    {
        public DateTime X { get; set; }

        public decimal Y { get; set; }
    }
}
