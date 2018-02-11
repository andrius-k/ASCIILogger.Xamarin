using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
namespace ASCIILoggerSample.iOS
{
    public class ImageCollectionViewSource : UICollectionViewSource
    {
        private List<UIImage> _images;
        private Action<int> _itemSelected;

        public ImageCollectionViewSource(List<UIImage> images, Action<int> itemSelected)
        {
            _images = images;
            _itemSelected = itemSelected;
        }

        public const string IMAGE_CELL_ID = "ImageCell";

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var animalCell = (ImageCell)collectionView.DequeueReusableCell(IMAGE_CELL_ID, indexPath);

            animalCell.Image = _images[indexPath.Row];

            return animalCell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _images.Count;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            _itemSelected?.Invoke(indexPath.Row);
        }
    }
}
