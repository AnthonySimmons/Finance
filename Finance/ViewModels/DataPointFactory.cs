﻿using FinanceModel.Models;
using System;

namespace Finance.ViewModels
{
    class DataPointFactory
    {
        internal static DataPoint GetDataPoint(Type dataPointType, Transaction transaction, decimal total)
        {
            DataPoint dataPoint = null;
            
            if (dataPointType == typeof(LineChartDataPoint))
            {
                dataPoint = new LineChartDataPoint
                {
                    X = transaction.Date,
                    Y = total,
                };
            }
            else if (dataPointType == typeof(PieChartDataPoint))
            {
                dataPoint = new PieChartDataPoint
                {
                    Name = transaction.Payee,
                    Value = transaction.Amount,
                };
            }
            else
            {
                throw new ArgumentException($"Invalid Data Point Type {dataPointType}");
            }
            return dataPoint;
        }

    }
}
