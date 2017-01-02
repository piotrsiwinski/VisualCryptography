using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace VisualCryptography.UI.Utils
{
    public static class ImageUtils
    {
        public static Bitmap ConvertToBitmap(this BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage)); enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }
    }
}