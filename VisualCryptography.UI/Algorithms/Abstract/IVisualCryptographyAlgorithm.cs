using System.Drawing;

namespace VisualCryptography.UI.Algorithms.Abstract
{
    public interface IVisualCryptographyAlgorithm
    {
        Bitmap[] EncryptBitmap(Bitmap bitmap, int shareCount);
        Bitmap DecryptBitmap(Bitmap[] sharesBitmaps);
    }
}