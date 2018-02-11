using System;
using UIKit;
using Foundation;
using CoreGraphics;
namespace ASCIILoggerSample.iOS
{
    public class ImageCell : UICollectionViewCell
    {
        private UIImageView _imageView { get; set; }

        public UIImage Image
        {
            get => _imageView.Image;
            set => _imageView.Image = value;
        }

        [Export("initWithFrame:")]
        public ImageCell(CGRect frame) : base (frame)
        {
            _imageView = new UIImageView();
            _imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            ContentView.AddSubview(_imageView);

            Constrain();
        }

        private void Constrain()
        {
            _imageView.TranslatesAutoresizingMaskIntoConstraints = false;

            _imageView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            _imageView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            _imageView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            _imageView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;
        }
    }
}
