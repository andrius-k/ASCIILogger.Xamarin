using System;
using Android.Graphics;
using System.Text;
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace ASCIILogger.Droid
{
    public static class ASCIILogger
    {
        private const int MAX_IMAGE_SIZE = 200;

        private static readonly char[] asciiPixels = { '@', '%', '#', '*', '+', '=', '-', ':', '.', ' ' };

        public static string Asciify(this Drawable drawable, int maxImageSize = MAX_IMAGE_SIZE)
        {
            return ConvertToBitmap(drawable).Asciify(maxImageSize);
        }

        public static string Asciify(int res, Resources resources, int maxImageSize = MAX_IMAGE_SIZE)
        {
            var bitmap = BitmapFactory.DecodeResource(resources, res);
            return bitmap.Asciify(maxImageSize);
        }

        public static string Asciify(this Bitmap bitmap, int maxImageSize = MAX_IMAGE_SIZE)
        {
            // Scale image down if needed
            bitmap = ScaleImageDown(bitmap, maxImageSize);

            int width = bitmap.Width - 1;
            int height = bitmap.Height - 1;

            var resultBuilder = new StringBuilder((width * 2 + 1) * height);

            for (int y = 0; y < height; y++)
            {
                resultBuilder.AppendLine();

                for (int x = 0; x < width; x++)
                {
                    (int red, int green, int blue, int alpha) color = GetPixelColor(bitmap, x, y);

                    int grayscale = (int)(0.2989 * color.red + 0.5870 * color.green + 0.1140 * color.blue);

                    char asciiPixel = asciiPixels[grayscale / 26];
                    resultBuilder.Append(asciiPixel, 2);
                }
            }

            return resultBuilder.ToString();
        }

        private static Bitmap ScaleImageDown(Bitmap bitmap, int maxImageSize)
        {
            var maxResizeFactor = Math.Min((float)maxImageSize / bitmap.Width, (float)maxImageSize / bitmap.Height);

            if (maxResizeFactor > 1)
                return bitmap;

            var width = (int)(maxResizeFactor * bitmap.Width);
            var height = (int)(maxResizeFactor * bitmap.Height);

            return Bitmap.CreateScaledBitmap(bitmap, width, height, true);
        }

        /// <summary>
        /// Returns r, g, b, a values for a specific pixel from provided Bitmap.
        /// </summary>
        private static (int red, int green, int blue, int alpha) GetPixelColor(Bitmap bitmap, int x, int y)
        {
            int color = bitmap.GetPixel(x, y);

            int red = Color.GetRedComponent(color);
            int green = Color.GetGreenComponent(color);
            int blue = Color.GetBlueComponent(color);
            int alpha = Color.GetAlphaComponent(color);

            return (red, green, blue, alpha);
        }

        private static Bitmap ConvertToBitmap(Drawable drawable)
        {
            return ((BitmapDrawable)drawable).Bitmap;
            //Bitmap mutableBitmap = Bitmap.CreateBitmap(widthPixels, heightPixels, Bitmap.Config.Argb8888);
            //Canvas canvas = new Canvas(mutableBitmap);
            //drawable.SetBounds(0, 0, widthPixels, heightPixels);
            //drawable.Draw(canvas);

            //return mutableBitmap;
        }
    }
}
