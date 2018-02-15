using System;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using Java.Lang;
using Android.App;

namespace ASCIILoggerSample.Droid
{
    public class ImagesListViewAdapter : BaseAdapter
    {
        private IList<int> _ids;
        private Activity _activity;

        public ImagesListViewAdapter(IList<int> ids, Activity activity)
        {
            _ids = ids ?? throw new ArgumentNullException(nameof(ids));
            _activity = activity ?? throw new ArgumentNullException(nameof(activity));
        }

        public override int Count => _ids.Count;

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
            var view = convertView ?? 
                _activity.LayoutInflater.Inflate(Resource.Layout.image_item, null);

            var imageView = view.FindViewById<ImageView>(Resource.Id.image_view);
            imageView.SetImageResource(_ids[position]);

            return view;
        }
    }
}
