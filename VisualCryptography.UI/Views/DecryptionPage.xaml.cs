using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VisualCryptography.UI.Algorithms;
using VisualCryptography.UI.Algorithms.Abstract;
using VisualCryptography.UI.Utils;

namespace VisualCryptography.UI.Views
{
    /// <summary>
    /// Interaction logic for DecryptionPage.xaml
    /// </summary>
    public partial class DecryptionPage : Page
    {
        private string _originalImagePath;
        private const int GenerateImageCount = 2;
        private Bitmap[] _encryptedBitmaps;
        private readonly IVisualCryptographyAlgorithm _algorithm;

        public DecryptionPage()
        {
            InitializeComponent();
            _algorithm = new VisualCryptographyAlgorithm();
        }

        private void OpenSharedImages_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Select a picture",
                    Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphic (*.png)|*.png"
                };

                if (openFileDialog.ShowDialog() != true) return;
                FirstImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                if (openFileDialog.ShowDialog() != true) return;
                SecondImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                DecryptButton.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się zapisać do pliku. Szczegóły błędu: {exception.Message}");
            }
        }

        private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var first = ((BitmapImage)FirstImage.Source)?.ConvertToBitmap();
                var second = ((BitmapImage)SecondImage.Source)?.ConvertToBitmap();

                var test = _algorithm.DecryptBitmap(new[] { first, second });
                OriginalImageTest.Source = test.ToImageSource();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się odszyfrować obrazka. Informacje o błędzie: {exception.Message}");
            }

        }

        private void SaveOriginalImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var originalImageTest = (OriginalImageTest.Source as BitmapImage)?.ConvertToBitmap();

                var saveFileDialog = new SaveFileDialog
                {
                    Title = "Select a picture",
                    Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphic (*.png)|*.png",
                    DefaultExt = "jpg"
                };

                if (saveFileDialog.ShowDialog() != true) return;
                originalImageTest?.Save(saveFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się zapisać do pliku. Szczegóły błędu: {exception.Message}");
            }
        }
    }
}
