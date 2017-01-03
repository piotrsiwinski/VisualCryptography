using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using VisualCryptography.UI.Algorithms.Abstract;
using VisualCryptography.UI.Utils;
using Color = System.Drawing.Color;

namespace VisualCryptography.UI.Algorithms
{
    public class VisualCryptographyAlgorithm : IVisualCryptographyAlgorithm
    {
        private const string BlackColorName = "ff000000";
        private List<int[,]> _whitePixelsMatrix;
        private List<int[,]> _blackPixelsMatrix;
        private Dictionary<int, Color> _colorsDictionary;

        public VisualCryptographyAlgorithm()
        {
            _colorsDictionary = new Dictionary<int, Color>
            {
                {0, Color.White},
                {1, Color.Black}
            };
            _blackPixelsMatrix = new List<int[,]>
            {
                new[,] {{1, 0}, {0, 1}},
                new[,] {{0, 1}, {1, 0}}
            };

            _whitePixelsMatrix = new List<int[,]>
            {
                new[,] {{0, 1}, {0, 1}},
                new[,] {{1, 0}, {1, 0}}
            };
        }

        public Bitmap[] EncryptBitmap(Bitmap bitmap, int shareCount)
        {
            Random random = new Random(1);

            Bitmap firstEncryptedBitmap = new Bitmap(bitmap.Width*2, bitmap.Height);
            Bitmap secondEncryptedBitmap = new Bitmap(bitmap.Width*2, bitmap.Height);


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
                        firstEncryptedBitmap.SetPixel(x*2, y, _colorsDictionary[result[0, 0]]);
                        firstEncryptedBitmap.SetPixel(x*2 + 1, y, _colorsDictionary[result[0, 1]]);
                        secondEncryptedBitmap.SetPixel(x*2, y, _colorsDictionary[result[1, 0]]);
                        secondEncryptedBitmap.SetPixel(x*2 + 1, y, _colorsDictionary[result[1, 1]]);
                    }
                    else
                    {
                        var result = _whitePixelsMatrix[index];
                        firstEncryptedBitmap.SetPixel(x*2, y, _colorsDictionary[result[0, 0]]);
                        firstEncryptedBitmap.SetPixel(x*2 + 1, y, _colorsDictionary[result[0, 1]]);
                        secondEncryptedBitmap.SetPixel(x*2, y, _colorsDictionary[result[1, 0]]);
                        secondEncryptedBitmap.SetPixel(x*2 + 1, y, _colorsDictionary[result[1, 1]]);
                    }
                }
            }

            return new[] {firstEncryptedBitmap, secondEncryptedBitmap};
        }

        private int ConvertColorToIndex(Color c)
        {
            return c.Name.ToLower() == BlackColorName ? 1 : 0;
        }


        public Bitmap DecryptBitmap(Bitmap[] sharesBitmaps)
        {
            Random random = new Random(1);
            var share = sharesBitmaps.First();
            Size size = new Size(share.Width/2, share.Height);
            Bitmap originalBitmap = new Bitmap(size.Width, size.Height);

            Bitmap firstEncryptedBitmap = sharesBitmaps[0];
            Bitmap secondEncryptedBitmap = sharesBitmaps[1];

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    var firstPixel = firstEncryptedBitmap.GetPixel(x*2, y);
                    var secondPixel = firstEncryptedBitmap.GetPixel(x*2+1, y);
                    var thirdPixel = secondEncryptedBitmap.GetPixel(x*2, y);
                    var fourthPixel = secondEncryptedBitmap.GetPixel(x*2+1, y);

                    Color[,] colors = {{firstPixel, secondPixel}, {thirdPixel, fourthPixel}};
                    int[,] matrix = new int[colors.GetLength(0), colors.GetLength(1)];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix[i, j] = ConvertColorToIndex(colors[i, j]);
                        } 
                    }

                    var result = matrix;

                    var test = _blackPixelsMatrix.ContainsMatrix(matrix) ? Color.Black : Color.White;
                    originalBitmap.SetPixel(x, y, test);
                }
            }



            return originalBitmap;

        }


    }

}