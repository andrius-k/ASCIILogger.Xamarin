using System;
using UIKit;
using CoreGraphics;
using System.Runtime.InteropServices;
using System.Text;
namespace ASCIILogger.iOS
{
    /// <summary>
    /// Provides an extension method see cref="Asciify"/> to UIImage class that returns
    /// the ASCII version of provided UIImage.
    /// </summary>
    public static class ASCIILogger
    {
        private const int MAX_IMAGE_SIZE = 100;

        /// <summary>
        /// Returns an ASCII version of provided image. The bigger the maxImageSize the more 
        /// accurate ASCII version will be. But it is not recommended to make this value too big 
        /// because it will take a long time for this method to complete the conversion.
        /// </summary>
        public static string Asciify(this UIImage image, int maxImageSize = MAX_IMAGE_SIZE)
        {
            // Scale image down if needed
            image = ScaleImageDown(image, maxImageSize);

            var resultBuilder = new StringBuilder();

            for (int y = 1; y < image.Size.Height - 1; y++)
            {
                resultBuilder.AppendLine();

                for (int x = 1; x < image.Size.Width - 1; x++)
                {
                    var color = GetPixelColor(new CGPoint(x, y), image);
                    color.GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

                    int grayscale = (int)(0.2989 * red * 255 + 0.5870 * green * 255 + 0.1140 * blue * 255);

                    if (grayscale < 25)
                    {
                        resultBuilder.Append("@");
                    } 
                    else if (grayscale< 50) 
                    {
                        resultBuilder.Append("%");
                    } 
                    else if (grayscale< 75) 
                    {
                        resultBuilder.Append("#");
                    } 
                    else if (grayscale< 100) 
                    {
                        resultBuilder.Append("*");
                    } 
                    else if (grayscale< 125) 
                    {
                        resultBuilder.Append("+");
                    } 
                    else if (grayscale< 150) 
                    {
                        resultBuilder.Append("=");
                    } 
                    else if (grayscale< 175) 
                    {
                        resultBuilder.Append("-");
                    } 
                    else if (grayscale< 200) 
                    {
                        resultBuilder.Append(":");
                    } 
                    else if (grayscale< 225) 
                    {
                        resultBuilder.Append(".");
                    } 
                    else 
                    {
                        resultBuilder.Append(" ");
                    }

                    // Repeat last char to "stretch" the image horizontally.
                    // This appears to keep the aspect ratio of the image
                    resultBuilder.Append(resultBuilder[resultBuilder.Length - 1]);
                }
            }

            return resultBuilder.ToString();
        }

        private static UIImage ScaleImageDown(UIImage sourceImage, int maxImageSize)
        {
            // Scale image to fit max size preserving aspect ratio

            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Min(maxImageSize / sourceSize.Width, maxImageSize / sourceSize.Height);

            if (maxResizeFactor > 1)
                return sourceImage;

            var width = maxResizeFactor * sourceSize.Width;
            var height = maxResizeFactor * sourceSize.Height;

            UIGraphics.BeginImageContext(new CGSize(width, height));
            sourceImage.Draw(new CGRect(0, 0, width, height));

            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return resultImage;
        }

        private static UIColor GetPixelColor(CGPoint point, UIImage image)
        {
            var rawData = new byte[4];
            var handle = GCHandle.Alloc(rawData);
            UIColor resultColor = null;
            try
            {
                using (var colorSpace = CGColorSpace.CreateDeviceRGB())
                {
                    using (var context = new CGBitmapContext(rawData, 1, 1, 8, 4, colorSpace, CGImageAlphaInfo.PremultipliedLast))
                    {
                        context.DrawImage(new CGRect(-point.X, point.Y - image.Size.Height, image.Size.Width, image.Size.Height), image.CGImage);
                        float red = (rawData[0]) / 255.0f;
                        float green = (rawData[1]) / 255.0f;
                        float blue = (rawData[2]) / 255.0f;
                        float alpha = (rawData[3]) / 255.0f;
                        resultColor = UIColor.FromRGBA(red, green, blue, alpha);
                    }
                }
            }
            finally
            {
                handle.Free();
            }

            return resultColor;
        }
    }
}
