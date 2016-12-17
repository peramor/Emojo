using System;
using System.Collections.Generic;
using Android.Graphics;

namespace Emojo.Droid.Helpers
{
    public static class Account
    {
        private static Random RANDOM = new Random();
        
        public static int RandomPhotoDrawable
        {
            get
            {
                switch (RANDOM.Next(5))
                {
                    default:
                    case 0:
                        return Resource.Drawable.face_1;
                    case 1:
                        return Resource.Drawable.face_2;
                    case 2:
                        return Resource.Drawable.face_3;
                    case 3:
                        return Resource.Drawable.face_4;
                    case 4:
                        return Resource.Drawable.face_5;
                }
            }
        }        
        public static List<string> CheeseStrings
        {
            get
            {
                return new List<string>() {
                    "_nickfox__", "_leilakh", "hero23m", "di_dianaev", "kursachka", "damerica",
                "d4uka", "izgagin_aleksei", "shelokovdim", "katyativan", "zabelka"};
            }        
        }

        public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > reqHeight || width > reqWidth)
            {

                // Calculate ratios of height and width to requested height and
                // width
                int heightRatio = height / reqHeight;
                int widthRatio = width / reqWidth;

                // Choose the smallest ratio as inSampleSize value, this will
                // guarantee
                // a final image with both dimensions larger than or equal to the
                // requested height and width.
                inSampleSize = heightRatio < widthRatio ? heightRatio : widthRatio;
            }

            return inSampleSize;
        }
    }
}