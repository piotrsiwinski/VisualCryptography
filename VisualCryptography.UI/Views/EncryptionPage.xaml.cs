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
    /// Interaction logic for EncryptionPage.xaml
    /// </summary>
    public partial class EncryptionPage : Page
    {
        private string _originalImagePath;
        private const int GenerateImageCount = 2;
        private Bitmap[] _encryptedBitmaps;
        private readonly IVisualCryptographyAlgorithm _algorithm;
        public EncryptionPage()
        {
            InitializeComponent();
            _algorithm = new VisualCryptographyAlgorithm();
        }

        private void EncryptImage_Click(object sender, RoutedEventArgs e)
        {
            if (OriginalImage.Source == null)
            {
                MessageBox.Show("Proszę wybrać obrazek");
                return;
            }
            var original = (OriginalImage.Source as BitmapImage)?.ConvertToBitmap();
            var result = _algorithm.EncryptBitmap(original, 2);

            FirstImage.Source = result[0].ToImageSource();
            SecondImage.Source = result[1].ToImageSource();

            if (FirstImage.Source != null && SecondImage.Source != null)
            {
                SaveDecryptedImagesToFile.IsEnabled = true;
            }
        }

        private void OpenOriginalImageButton_OnClick(object sender, RoutedEventArgs e)
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
                _originalImagePath = openFileDialog.FileName;
                OriginalImage.Source = new BitmapImage(new Uri(_originalImagePath));
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się zapisać do pliku. Szczegóły błędu: {exception.Message}");
            }
        }

        private void SaveDecryptedImagesToFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var firstImageBitmap = (FirstImage.Source as BitmapImage)?.ConvertToBitmap();
                var secondImageBitmap = (SecondImage.Source as BitmapImage)?.ConvertToBitmap();

                var saveFileDialog = new SaveFileDialog
                {
                    Title = "Select a picture",
                    Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphic (*.png)|*.png",
                    DefaultExt = "jpg"
                };

                if (saveFileDialog.ShowDialog() != true) return;
                firstImageBitmap?.Save(saveFileDialog.FileName);
                if (saveFileDialog.ShowDialog() != true) return;
                secondImageBitmap?.Save(saveFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się zapisać do pliku. Szczegóły błędu: {exception.Message}");
            }
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
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się zapisać do pliku. Szczegóły błędu: {exception.Message}");
            }
        }
    }
}
