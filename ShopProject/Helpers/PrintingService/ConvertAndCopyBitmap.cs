using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ShopProject.Helpers.PrintingServise
{
    internal static class ConvertAndCopyBitmap
    {
        public static BitmapImage BitmapForBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        public static Bitmap CopyBitmapInBitmap(Bitmap image, int x, int y, int width, int height, int bottom)
        {
            Bitmap resultCopy = new Bitmap(width, height);
            for (int i = x; i < width; i++)
            {
                for (int j = y; j < height - bottom; j++)
                {
                    if (i < image.Width & j < image.Height)
                        resultCopy.SetPixel(i, j, image.GetPixel(i, j));
                }
            }

            return resultCopy;
        }
        public static BitmapSource BitmapForBitmapSource(Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
