using System;
using UIKit;
using CoreGraphics;
using System.Runtime.InteropServices;
using System.Text;

namespace ASCIILogger.iOS
{
    /// <summary>
    /// Provides an extension method <see cref="Asciify"/> to UIImage class that returns
    /// the ASCII version of provided UIImage.
    /// </summary>
    public static class ASCIILogger
    {
        private const int MAX_IMAGE_SIZE = 200;

        private static readonly char[] asciiPixels = { '@', '%', '#', '*', '+', '=', '-', ':', '.', ' ' };

        /// <summary>
        /// Returns an ASCII version of provided image. The bigger the maxImageSize the more 
        /// accurate ASCII version will be. But it is not recommended to make this value too big 
        /// because it will take a long time for this method to complete the conversion.
        /// </summary>
        public static string Asciify(this UIImage image, int maxImageSize = MAX_IMAGE_SIZE)
        {
            // Scale image down if needed
            image = ScaleImageDown(image, maxImageSize);

            int width = (int)image.Size.Width - 1;
            int height = (int)image.Size.Height - 1;

            // Get raw pixel data
            var rawData = GetRawPixelData(image, width, height);

            var resultBuilder = new StringBuilder((width * 2 + 1) * height);

            for (int y = 0; y < height; y++)
            {
                resultBuilder.AppendLine();

                for (int x = 0; x < width; x++)
                {
                    (int red, int green, int blue, int alpha) color = GetPixelColor(x, y, width, rawData);

                    int grayscale = (int)(0.2989 * color.red + 0.5870 * color.green + 0.1140 * color.blue);

                    char asciiPixel = asciiPixels[grayscale / 26];
                    resultBuilder.Append(asciiPixel, 2);
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

        /// <summary>
        /// Returns one dimensional byte array with pixel color info (0 - 255).
        /// Each 4 consecutive pixels represent r, g, b, a values of one pixel.
        /// Use <see cref="GetPixelColor"/> method to get a tuple with each component
        /// for a specific pixel.
        /// </summary>
        private static byte[] GetRawPixelData(UIImage image, int width, int height)
        {
            var rawData = new byte[width * height * 4];
            var handle = GCHandle.Alloc(rawData);

            try
            {
                using (var colorSpace = CGColorSpace.CreateDeviceRGB())
                {
                    using (var context = new CGBitmapContext(rawData, width, height, 8, width * 4, colorSpace, CGImageAlphaInfo.PremultipliedLast))
                    {
                        context.DrawImage(new CGRect(0, 0, image.Size.Width, image.Size.Height), image.CGImage);

                        return rawData;
                    }
                }
            }
            finally
            {
                handle.Free();
            }
        }

        /// <summary>
        /// Returns r, g, b, a values for a specific pixel from raw image data.
        /// </summary>
        private static (int red, int green, int blue, int alpha) GetPixelColor(int x, int y, int width, byte[] rawData)
        {
            int position = (y * width * 4) + (x * 4);
            return (rawData[position], rawData[position + 1], rawData[position + 2], rawData[position + 3]);
        }
    }
}
