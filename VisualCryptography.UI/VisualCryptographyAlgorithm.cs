using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Documents;

namespace VisualCryptography.UI
{
    public class VisualCryptographyAlgorithm
    {
        private const string BlackColorName = "ff000000";
        private List<int[,]> _whitePixelsMatrix;
        private List<int[,]> _blackPixelsMatrix;
        private Dictionary<int, Color> _colorsDictionary;

        public VisualCryptographyAlgorithm()
        {
            _colorsDictionary = new Dictionary<int, Color>
            {
                {0, Color.White },
                {1, Color.Black }
            };
            _blackPixelsMatrix = new List<int[,]>
            {
                new int[2, 2] {{1, 0}, {0, 1}},
                new int[2, 2] {{0, 1}, {1, 0}}
            };

            _whitePixelsMatrix = new List<int[,]>
            {
                new int[2, 2] {{0, 1}, {0, 1}},
                new int[2, 2] {{1, 0}, {1, 0}}
            };
        }

        public Bitmap[] EncryptBitmap(Bitmap bitmap, int shareCount)
        {
            Random random = new Random(1);
            
            Bitmap firstEncryptedBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height);
            Bitmap secondEncryptedBitmap = new Bitmap(bitmap.Width * 2, bitmap.Height);


            Bitmap[] bitmaps = new Bitmap[shareCount];
            bitmaps[0] = firstEncryptedBitmap;
            bitmaps[1] = secondEncryptedBitmap;

            
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var index = random.Next(bitmaps.Length); 

                    if (pixel.Name.ToLower() == BlackColorName)
                    {

                        var result = _blackPixelsMatrix[index];
                        firstEncryptedBitmap.SetPixel(x*2, y, _colorsDictionary[result[0,0]]);
                        firstEncryptedBitmap.SetPixel(x*2+1, y, _colorsDictionary[result[0, 1]]);
                        secondEncryptedBitmap.SetPixel(x*2, y, _colorsDictionary[result[1, 0]]);
                        secondEncryptedBitmap.SetPixel(x*2+1, y, _colorsDictionary[result[1, 1]]);
                    }
                    else
                    {
                        var result = _whitePixelsMatrix[index];
                        firstEncryptedBitmap.SetPixel(x * 2, y, _colorsDictionary[result[0, 0]]);
                        firstEncryptedBitmap.SetPixel(x * 2 + 1, y, _colorsDictionary[result[0, 1]]);
                        secondEncryptedBitmap.SetPixel(x * 2, y, _colorsDictionary[result[1, 0]]);
                        secondEncryptedBitmap.SetPixel(x * 2 + 1, y, _colorsDictionary[result[1, 1]]);
                    }
                }
            }

            return new[] {firstEncryptedBitmap, secondEncryptedBitmap};
        }

    }
    
}