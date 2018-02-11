using Foundation;
using System;
using UIKit;
using ASCIILogger.iOS;

namespace ASCIILoggerSample.iOS
{
    public partial class AsciiViewController : UIViewController
    {
        public UIImage Image { get; set; }

        public AsciiViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Asciified Image";

            string ascii = Image.Asciify();
            // Display ASCII in pre tag to make sure monospace font is used
            string html = $"<html></head><body style='font-size: 2px; line-height: 1;'><pre>{ascii}</pre></body></html>";

            //webView.ScalesPageToFit = true;
            webView.LoadHtmlString(html, null);
        }
    }
}