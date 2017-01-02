using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Documents;

namespace VisualCryptography.UI
{
    public class VisualCryptographyAlgorithm
    {
        private readonly Color BlackColor = Color.Black ;
        private const string BlackColorName = "ff000000";
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

        public Bitmap[] EncryptBitmap(Bitmap bitmap)
        {
            Random random = new Random(1);
            Bitmap firstEncryptedBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height);
            Bitmap secondEncryptedBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    if (pixel.Name.ToLower() == BlackColorName)
                    {
                        firstEncryptedBitmap.SetPixel(x*2, y, Color.Black);
                        firstEncryptedBitmap.SetPixel(x*2+1, y, Color.White);
                        secondEncryptedBitmap.SetPixel(x*2, y, Color.White);
                        secondEncryptedBitmap.SetPixel(x*2+1, y, Color.Black);
                    }
                    else
                    {
                        firstEncryptedBitmap.SetPixel(x * 2, y, Color.White);
                        firstEncryptedBitmap.SetPixel(x * 2 + 1, y, Color.Black);
                        secondEncryptedBitmap.SetPixel(x * 2, y, Color.White);
                        secondEncryptedBitmap.SetPixel(x * 2 + 1, y, Color.Black);
                    }
                }
            }

            return new[] {firstEncryptedBitmap, secondEncryptedBitmap};
        }

    }
    
}