using Android.OS;
using Android.Views;
using Emojo.Droid;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Emojo.Droid.Fragments
{
    public class Fragment2 : SupportFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment2, container, false);

            return view;
        }
    }
}