using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;
using static ASCIILogger.Droid.ASCIILogger;

namespace ASCIILoggerSample.Droid
{
    [Activity(Label = "AsciiActivity")]
    public class AsciiActivity : Activity
    {
        public const string IMAGE_RES_ID_KEY = "imageResIdKey";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_ascii);

            int resId = Intent.GetIntExtra(IMAGE_RES_ID_KEY, 0);
            var webView = FindViewById<WebView>(Resource.Id.web_view);

            string ascii = Asciify(resId, Resources);

            // We could also use Asciify extension methods on Bitmap or Drawable:
            // string ascii = someBitmap.Asciify();
            // string ascii = someDrawable.Asciify();

            // Display ASCII in pre tag to make sure monospace font is used
            string html = $"<html></head><body style='font-size: 2px; line-height: 1;'><pre>{ascii}</pre></body></html>";

            webView.LoadDataWithBaseURL(null, html, "text/html", "UTF-8", "UTF-8");
        }
    }
}
