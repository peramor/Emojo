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

namespace Emojo.Droid.Fragments
{
    public class Fragment2 : SupportFragment
    {
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

            gridView.Adapter = new ImageAdapter(this.Context, gridView.Width / 3);


            gridView.ItemClick += (s, a) => { };
            
            return view;
        }
    }

    public class ImageAdapter : BaseAdapter
    {
        Context context;
        double length;

        int[] thumbIds = {
                Resource.Drawable.face_1, Resource.Drawable.face_2,
                Resource.Drawable.face_3, Resource.Drawable.face_4,
                Resource.Drawable.face_5, Resource.Drawable.face_1,
                Resource.Drawable.face_1, Resource.Drawable.face_2,
                Resource.Drawable.face_3, Resource.Drawable.face_4,
                Resource.Drawable.face_5, Resource.Drawable.face_1,
                Resource.Drawable.face_1, Resource.Drawable.face_2,
                Resource.Drawable.face_3, Resource.Drawable.face_4,
                Resource.Drawable.face_5, Resource.Drawable.face_1,
                Resource.Drawable.face_1, Resource.Drawable.face_2,
                Resource.Drawable.face_3, Resource.Drawable.face_4,
                Resource.Drawable.face_5, Resource.Drawable.face_1
            };

        public ImageAdapter(Context c, double length)
        {
            context = c;
            this.length = length;
        }

        public override int Count
        {
            get { return thumbIds.Length; }
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

            imageView.SetImageResource(thumbIds[position]);
            return imageView;
        }
    }
}