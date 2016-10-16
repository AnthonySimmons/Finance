using Finance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.ViewModels
{
    public class LineChartDataPoint : DataPoint
    {
        public DateTime X { get; set; }

        public double Y { get; set; }
    }
}
