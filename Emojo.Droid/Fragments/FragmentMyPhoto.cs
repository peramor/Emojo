using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Emojo.Droid;
using Java.Lang;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Content;
using MikePhil.Charting.Charts;
using System.Collections.Generic;
using Android.Graphics;
using System.Net;
using System.Threading.Tasks;
using Emojo.Lib;
using Context = Android.Content.Context;
using Emojo.Droid.Helpers;

namespace Emojo.Droid.Fragments
{
    public class FragmentMyPhoto : SupportFragment
    {
        List<string> _photoLinks;

        public FragmentMyPhoto(List<string> photoLinks)
        {
            _photoLinks = photoLinks;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        GridView gridView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment2, container, false);
            gridView = view.FindViewById<GridView>(Resource.Id.gridview);

            gridView.Adapter = new ImageAdapter(this.Context, _photoLinks);
            
            return view;
        }
    }

    public class ImageAdapter : BaseAdapter
    {
        Context context;
        List<string> _source;

        public ImageAdapter(Context c, List<string> source)
        {
            context = c;
            _source = source;
        }

        public override int Count
        {
            get { return _source.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;

            if (convertView == null)
            {  
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(150, 150);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.Click += (a, e) =>
                {
                    Context context = imageView.Context;
                    Intent intent = new Intent(context, typeof(AccountDetailActivity));
                    intent.PutExtra("PHOTO_URI", _source[position]);
                    context.StartActivity(intent);
                };
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            var imageBitmap = BitmapHelpers.GetImageBitmapFromUrl(_source[position]);
            imageView.SetImageBitmap(imageBitmap);

            TextView emojo = new TextView(context);
            emojo.Text = BitmapHelpers.SmileDic[(Emotions)new Random().Next(5)];
            emojo.TextSize = 12;

            return imageView;
        }
    }
}