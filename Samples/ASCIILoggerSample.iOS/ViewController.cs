using System;

using UIKit;
using ASCIILogger.iOS;
using System.Collections.Generic;
using CoreGraphics;

namespace ASCIILoggerSample.iOS
{
    public partial class ViewController : UIViewController
    {
        private string[] imageIds = { "image1.jpg", "image2", "image3.jpg" };

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Select an Image";

            var images = new List<UIImage>();
            foreach(string imageId in imageIds)
            {
                images.Add(UIImage.FromBundle(imageId));
            }

            collectionView.RegisterClassForCell(typeof(ImageCell), ImageCollectionViewSource.IMAGE_CELL_ID);
            var layout = new UICollectionViewFlowLayout();
            layout.ItemSize = new CGSize(collectionView.Bounds.Width, 200);
            collectionView.CollectionViewLayout = layout;

            collectionView.Source = new ImageCollectionViewSource(images, id =>
            {
                var controller = (AsciiViewController)Storyboard.InstantiateViewController("AsciiViewController");
                controller.Image = images[id];
                NavigationController.PushViewController(controller, true);
            });
        }
    }
}
