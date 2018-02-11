using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System;
using ASCIILogger.Droid;
using Android.Content;

namespace ASCIILoggerSample.Droid
{
    [Activity(Label = "ASCIILoggerSample.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private int[] _ids = { Resource.Drawable.image1, Resource.Drawable.image2, Resource.Drawable.image3 };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var listView = FindViewById<ListView>(Resource.Id.list_view);
            listView.Adapter = new ImagesListViewAdapter(_ids, this);
            listView.ItemClick += (sender, e) => 
            {
                var intent = new Intent(this, typeof(AsciiActivity));
                intent.PutExtra(AsciiActivity.IMAGE_RES_ID_KEY, _ids[e.Position]);
                StartActivity(intent);
            };
        }
    }
}

