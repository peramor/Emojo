using Android.App;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Widget;
using Android.Views;
using Emojo.Droid.Helpers;
using Emojo.Droid.Fragments;
using Emojo.Lib;
using System;
using Context = Android.Content.Context;
using MikePhil.Charting.Charts;
using System.Collections.Generic;

namespace Emojo.Droid
{
    [Activity(Label = "Photo detail", Theme = "@style/Theme.DesignDemo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AccountDetailActivity : AppCompatActivity
    {
        private string uri;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Activity_Detail);

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            uri = Intent.GetStringExtra("PHOTO_URI");

            CollapsingToolbarLayout collapsingToolBar = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);

            PieChart pie = FindViewById<PieChart>(Resource.Id.pieChartOne);
            var builder = ChartFactory.Default.GetChartBuilder(pie);
            var gen = new Random();

            Dictionary<string, double> dataSet = new Dictionary<string, double>
            {
                { "Happy", gen.Next(0, 100) },
                { "Sad", gen.Next(0, 100) },
                { "Fear", gen.Next(0, 100) },
                { "Angry", gen.Next(0, 100) },
                { "Surprise", gen.Next(0, 100) },
            };

            builder.SetData(dataSet);

            LoadBackDrop();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void LoadBackDrop()
        {
            ImageView imageView = FindViewById<ImageView>(Resource.Id.backdrop);
            imageView.SetImageBitmap(BitmapHelpers.GetImageBitmapFromUrl(uri));
        }
    }
}