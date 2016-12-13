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

namespace Emojo.Droid.Fragments
{
    public class Fragment2 : SupportFragment
    {
        List<string> _photoLinks;

        public Fragment2(List<string> photoLinks)
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


            gridView.ItemClick += (s, a) => { };
            
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

        // create a new ImageView for each item referenced by the Adapter
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;

            if (convertView == null)
            {  // if it's not recycled, initialize some attributes
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(150, 150);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.Click += (a, e) => { };
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            var imageBitmap = GetImageBitmapFromUrl(_source[position]);
            imageView.SetImageBitmap(imageBitmap);

            return imageView;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }


}