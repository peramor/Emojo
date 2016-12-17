using Android.App;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Support.V4.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using Java.Lang;
using Emojo.Droid.Fragments;
using Emojo.Lib;
using System.Threading.Tasks;
using Java.IO;
using Android.Graphics;
using Android.Provider;
using Android.Content.PM;
using System;
using Android.Widget;
using Uri = Android.Net.Uri;
using Emojo.Droid.Helpers;

namespace Emojo.Droid
{
    [Activity(Label = "Emojo", MainLauncher = true,
        Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout mDrawerLayout;
        List<string> photoLinks = new List<string>() {
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/1530641_831063783599337_1611920088_n.jpg?ig_cache_key=OTA4NTExOTkxMTU3NTIwMDE5.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/10601685_1452490401680454_1631356791_n.jpg?ig_cache_key=NzkwNjE0MDM3ODUyMzUzNzk3.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/12107612_775475549230876_87204900_n.jpg?ig_cache_key=MTEwODIxOTA3MDI2NzgxNDk0NA==.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e35/12826150_867009390087732_1342751490_n.jpg?ig_cache_key=MTIwNDg0MzM1ODM1MjU3NzI4Mg==.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e35/924987_190961084619412_19814228_n.jpg?ig_cache_key=MTIxNDYwMjQ5NTA1NTYzMDY0MQ==.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e35/13117946_261828130831913_1628675311_n.jpg?ig_cache_key=MTIzOTI1OTk0MDMxOTEzNzk4MA==.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e35/13151248_237514076605241_2139360198_n.jpg?ig_cache_key=MTI0NjY3ODc0MzMxOTU4MTY1Nw==.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e35/13181501_1692961907634211_853381316_n.jpg?ig_cache_key=MTI1MzgyODk3MTg0ODAxMjU3Nw==.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/10601685_1452490401680454_1631356791_n.jpg?ig_cache_key=NzkwNjE0MDM3ODUyMzUzNzk3.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/891457_546413035487528_1755091881_n.jpg?ig_cache_key=NzkwNjE0NTM1OTg0Njc0MDYy.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/10831806_323697501170361_629419214_n.jpg?ig_cache_key=ODc3NjkzNzEwNTQ0OTU5MzIw.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/1530641_831063783599337_1611920088_n.jpg?ig_cache_key=OTA4NTExOTkxMTU3NTIwMDE5.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/11176379_720832298042528_2008360608_n.jpg?ig_cache_key=OTY3MTc5ODcwODYxODE3NDg3.2",
            "https://scontent.cdninstagram.com/t51.2885-15/s150x150/e15/11313543_380222042178029_1812153444_n.jpg?ig_cache_key=OTkxMjY2MDgzMzUyNjM5MDY0.2"
        };
        private ImageView _imageView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // photoLinks.AddRange(Intent.GetStringArrayExtra("photos"));

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            SetSupportActionBar(toolBar);
            SupportActionBar ab = SupportActionBar;
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            SetUpViewPager(viewPager);
            tabs.SetupWithViewPager(viewPager);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            string SnackbarActionName = "CAMERA IS NOT FOUND";
            Action<View> SnackbarAction = (v) => { };

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
                SnackbarActionName = "Take a picture";
                SnackbarAction = TakeAPicture;
            }

            fab.Click += (o, e) =>
            {
                View anchor = o as View;

                Snackbar.Make(anchor, "20 PHOTOS", Snackbar.LengthLong)
                    .SetAction(SnackbarActionName, SnackbarAction)
                    .Show();
            };
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "Emojo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(View v)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, string.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);

            // Resource: https://developer.xamarin.com/recipes/android/other_ux/camera_intent/take_a_picture_and_save_using_camera_app/
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            //adapter.AddFragment(new Fragment1(), "Collage");
            adapter.AddFragment(new Fragment3(), "Emotions");
            adapter.AddFragment(new Fragment2(photoLinks), "My photos");

            viewPager.Adapter = adapter;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                e.MenuItem.SetChecked(true);
                mDrawerLayout.CloseDrawers();
            };
        }

        public class TabAdapter : FragmentPagerAdapter
        {
            public List<SupportFragment> Fragments { get; set; }
            public List<string> FragmentNames { get; set; }

            public TabAdapter(SupportFragmentManager sfm) : base(sfm)
            {
                Fragments = new List<SupportFragment>();
                FragmentNames = new List<string>();
            }

            public void AddFragment(SupportFragment fragment, string name)
            {
                Fragments.Add(fragment);
                FragmentNames.Add(name);
            }

            public override int Count
            {
                get
                {
                    return Fragments.Count;
                }
            }

            public override SupportFragment GetItem(int position)
            {
                return Fragments[position];
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(FragmentNames[position]);
            }
        }
    }

    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }
}

