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
using MikePhil.Charting.Data;
using MikePhil.Charting.Util;
using Emojo.Lib;
using Emojo.Droid.Fragments;

namespace Emojo.Droid.Helpers
{
    class PieChartBuilder : IChartBuilder<PieChart>
    {
        public PieChart Chart { get; set; }
        public int[] Colors { get; set; }
        public string Description {
            get { return Description; }
            set
            {
                Chart.Description = new MikePhil.Charting.Components.Description()
                {
                    Text = value,
                    XOffset = 16,
                    YOffset = 20
                };
            }
        }

        public PieChartBuilder(PieChart chart)
        {
            Chart = chart;
            Description = "";

            Colors = new int[]
            {
                ColorTemplate.JoyfulColors[1],
                ColorTemplate.JoyfulColors[4],
                ColorTemplate.JoyfulColors[3],
                ColorTemplate.JoyfulColors[0],
                ColorTemplate.JoyfulColors[2]
            };

            SetUpChart();
        }

        public void SetData(Dictionary<string, double> dataSet)
        {
            var pieEntries = (from d in dataSet
                              select new PieEntry((float)d.Value, d.Key))
                             .ToList();
            PieDataSet pieDataSet = new PieDataSet(pieEntries, "");
            pieDataSet.SetColors(Colors);

            PieData pieData = new PieData(pieDataSet);
            Chart.Data = pieData;
        }

        private void SetUpChart()
        {
            Chart.SetDrawCenterText(true);
            Chart.CenterText = BitmapHelpers.SmileDic[(Emotions)new Random().Next(5)];
            Chart.SetCenterTextSize(40);
        }
    }
}