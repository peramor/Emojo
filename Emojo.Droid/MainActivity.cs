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
    [Activity(Label = "Emojo", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait,
        Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout mDrawerLayout;
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

            #region Add Floating Action Button
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fabMain);

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

                Snackbar.Make(anchor, Repository.Photos.Count + " PHOTOS", Snackbar.LengthLong)
                    .SetAction(SnackbarActionName, SnackbarAction)
                    .Show();
            };
            #endregion
        }

        public override void OnBackPressed()
        { }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.sample_actions, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            return true;
        }

        #region Add Camera
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
            //int width = _imageView.Height;
            //App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            //if (App.bitmap != null)
            //{
            //    _imageView.SetImageBitmap(App.bitmap);
            //    App.bitmap = null;
            //}

            // Dispose of the Java side bitmap.
            GC.Collect();
        }
        #endregion

        private void SetUpViewPager(ViewPager viewPager)
        {
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            //adapter.AddFragment(new Fragment1(), "Collage");
            adapter.AddFragment(new FragmentEmotions(), "Emotions");
            adapter.AddFragment(new FragmentMyPhoto(), "My photos");

            viewPager.Adapter = adapter;
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

