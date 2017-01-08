using System.Drawing;
using VisualCryptography.UI.Algorithms.Abstract;

namespace VisualCryptography.UI.Algorithms
{
    public class VsCryptographyV2 : IVisualCryptographyAlgorithm
    {
        public Bitmap[] EncryptBitmap(Bitmap bitmap, int shareCount)
        {
            throw new System.NotImplementedException();
        }

        public Bitmap DecryptBitmap(Bitmap[] sharesBitmaps)
        {
            throw new System.NotImplementedException();
        }
    }
}