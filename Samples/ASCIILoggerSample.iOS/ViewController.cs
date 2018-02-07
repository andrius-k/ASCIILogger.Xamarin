using System;

using UIKit;
using ASCIILogger.iOS;

namespace ASCIILoggerSample.iOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Console.WriteLine(imageView.Image.Asciify());
        }
    }
}
