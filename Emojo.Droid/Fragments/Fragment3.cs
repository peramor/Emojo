using Android.OS;
using Android.Views;
using Android.Widget;
using Emojo.Lib;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Data;
using MikePhil.Charting.Util;
using Refractored.Controls;
using System;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Emojo.Droid.Fragments
{
    public partial class Fragment3 : SupportFragment

    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        private Dictionary<Emotions, string> SmileDic = new Dictionary<Emotions, string>
        {
            {Emotions.Anger, "😡" },
            {Emotions.Fear, "😱" },
            {Emotions.Happiness, "😁" },
            {Emotions.Sadness, "😳" },
            {Emotions.Surprise, "😳" }
        }; // 😡 😱 😁 😭 😳

        private int[] colors = new int[]
            {
                ColorTemplate.JoyfulColors[1],
                ColorTemplate.JoyfulColors[4],
                ColorTemplate.JoyfulColors[3],
                ColorTemplate.JoyfulColors[0],
                ColorTemplate.JoyfulColors[2]
            };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment3, container, false);
            PieChart pie = view.FindViewById<PieChart>(Resource.Id.pieChart1);
            SetUpPieChart(pie);

            Random gen = new Random();

            List<PieEntry> pieEntries = new List<PieEntry>
            {
                new PieEntry(gen.Next(0, 100), "Happy"),
                new PieEntry(gen.Next(0, 100), "Sad"),
                new PieEntry(gen.Next(0, 100), "Fear"),
                new PieEntry(gen.Next(0, 100), "Angry"),
                new PieEntry(gen.Next(0, 100), "Surprise"),
            };

            PieDataSet dataset = new PieDataSet(pieEntries, "");
            dataset.SetColors(colors);
            PieData pieData = new PieData(dataset);
            pie.Data = pieData;

            var profilePicture = view.FindViewById<CircleImageView>(Resource.Id.profilePicture);
            var name = view.FindViewById<TextView>(Resource.Id.textName);
            var bio = view.FindViewById<TextView>(Resource.Id.textBio);

            profilePicture.SetImageResource(Resource.Drawable.face_2);
            name.Text = "Natali_zc";
            bio.Text = "Natalie Zauceva";

            return view;
        }

        private void SetUpPieChart(PieChart pie)
        {
            pie.Description = new MikePhil.Charting.Components.Description()
            {
                Text = "from 20 photos",
                XOffset = 16,
                YOffset = 20
            };
            //pie.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;

            //pie.SetUsePercentValues(true);
            pie.SetDrawCenterText(true);
            pie.CenterText = SmileDic[(Emotions)new Random().Next(5)];
            pie.SetCenterTextSize(40);

        }
    }
}