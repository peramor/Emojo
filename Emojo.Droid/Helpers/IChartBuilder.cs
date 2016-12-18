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
    interface IChartBuilder<T>
    {
        string Description { get; set; }
        string Text { get; set; }
        T Chart { get; set; }
        int[] Colors { get; set; }        
        void SetData(Dictionary<string, double> dataSet);
    }
}