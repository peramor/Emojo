using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
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
            View view = inflater.Inflate(Resource.Layout.Fragment3, container, false);
            PieChart pie = view.FindViewById<PieChart>(Resource.Id.pieChart1);

            Random gen = new Random();

            pie.Description = new MikePhil.Charting.Components.Description() { Text = "Emotions" };
            pie.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;

            pie.SetUsePercentValues(true);
            pie.HoleRadius = 7;

            int[] colors = new int[5];
            for (int i = 0; i < 5; i++)
            {
                colors[i] = ColorTemplate.JoyfulColors[i];
            }

            List<PieEntry> pieEntries = new List<PieEntry>
            {
                new PieEntry(gen.Next(0, 100), "happy"),
                new PieEntry(gen.Next(0, 100), "Sad"),
                new PieEntry(gen.Next(0, 100), "Fear"),
                new PieEntry(gen.Next(0, 100), "Angry"),
                new PieEntry(gen.Next(0, 100), "Surprise"),
            };

            PieDataSet dataset = new PieDataSet(pieEntries, "emotions");
            dataset.SetColors(colors);
            PieData pieData = new PieData(dataset);
            pie.Data = pieData;

            return view;
        }
    }
}