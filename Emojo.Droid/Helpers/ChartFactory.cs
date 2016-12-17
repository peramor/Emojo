using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MikePhil.Charting.Charts;

namespace Emojo.Droid.Helpers
{
    class ChartFactory
    {
        private static ChartFactory _default;

        public static ChartFactory Default
        {
            get
            {
                if (_default == null)
                    _default = new ChartFactory();
                return _default;
            }
        }

        //IChartBuilder<PieChart> _pieChart = new PieChartBuilder();
        //IChartBuilder<LineChart> _lineChart = new LineChartBuilder();

        public IChartBuilder<T> GetChartBuilder<T>(T chart)
        {
            if (typeof(T) == typeof(PieChart))
                return (IChartBuilder<T>)new PieChartBuilder(chart as PieChart);
            else if (typeof(T) == typeof(LineChart))
                return (IChartBuilder<T>)new LineChartBuilder(chart as LineChart);
            else
                throw new NotImplementedException("This type of chart is not implemented yet");
        }
    }
}