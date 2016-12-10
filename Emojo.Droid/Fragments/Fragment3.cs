using Android.OS;
using Android.Views;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Data;
using MikePhil.Charting.Util;
using System;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Emojo.Droid.Fragments
{
    public class Fragment3 : SupportFragment

    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //View view = inflater.Inflate(Resource.Layout.Fragment3, container, false);

            BarChart chart = new BarChart(Context)
            {
                Description = new MikePhil.Charting.Components.Description() { Text = "Sample Description"}
            };
            Random gen = new Random();
            List<BarEntry> entries = new List<BarEntry>();
            entries.Add(new BarEntry(0, gen.Next(0,10), "January"));
            entries.Add(new BarEntry(1, gen.Next(0, 10), "February"));
            entries.Add(new BarEntry(2, gen.Next(0, 10), "March"));
            entries.Add(new BarEntry(3, gen.Next(0, 10), "April"));
            entries.Add(new BarEntry(4, gen.Next(0, 10), "May"));
            entries.Add(new BarEntry(5, gen.Next(0, 10), "June"));
            entries.Add(new BarEntry(6, gen.Next(0, 10), "Jule"));
            BarDataSet dataSet = new BarDataSet(entries, "# of calls");

            BarData data = new BarData(dataSet);
            chart.Data = data;

            return chart;
        }
    }
}