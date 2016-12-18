using Android.OS;
using Android.Views;
using Android.Widget;
using Emojo.Droid.Helpers;
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
    public partial class FragmentEmotions : SupportFragment
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

            var builder = ChartFactory.Default.GetChartBuilder(pie);
            builder.Description = "from " + Repository.Photos.Count + " photo";

            Dictionary<string, double> dataSet = Repository.GetOverall().Result;

            builder.SetData(dataSet);

            var profilePicture = view.FindViewById<CircleImageView>(Resource.Id.profilePicture);
            var name = view.FindViewById<TextView>(Resource.Id.textName);
            var bio = view.FindViewById<TextView>(Resource.Id.textBio);

            profilePicture.SetImageBitmap(BitmapHelpers.GetImageBitmapFromUrl(Repository.User.ProfilePhoto));

            name.Text = Repository.User.UserName;
            bio.Text = Repository.User.FullName;

            return view;
        }
    }
}