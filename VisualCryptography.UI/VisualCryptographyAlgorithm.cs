using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Documents;

namespace VisualCryptography.UI
{
    public class VisualCryptographyAlgorithm
    {
        private List<int[,]> _whitePixelsMatrix = new List<int[,]>
        {
            new int[2, 2] {{0, 1}, {0, 1}},
            new int[2, 2] {{1, 0}, {1, 0}}
        };

        private List<int[,]> _blackPixelsMatrix = new List<int[,]>
        {
            new int[2, 2] {{1, 0}, {0, 1}},
            new int[2, 2] {{0, 1}, {1, 0}}
        };

        public Bitmap[] EncryptedBitmaps(Bitmap bitmap)
        {
            Bitmap firstEncryptedBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height);
            Bitmap secondEncryptedBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pixel = bitmap.GetPixel(i, j);
                }
            }




            return new[] {firstEncryptedBitmap, secondEncryptedBitmap};
        }

    }
    
}