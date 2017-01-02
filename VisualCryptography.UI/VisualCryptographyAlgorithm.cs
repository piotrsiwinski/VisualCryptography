using System.Collections.Generic;
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
            return new [] {bitmap, bitmap};
        }

    }
    
}