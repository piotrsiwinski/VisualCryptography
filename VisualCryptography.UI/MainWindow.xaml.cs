using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VisualCryptography.UI.Utils;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace VisualCryptography.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _originalImagePath;
        private const int GenerateImageCount= 2;
        private Bitmap[] _encryptedBitmaps;
        private VisualCryptographyAlgorithm _algorithm;

        public MainWindow()
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
            var result = _algorithm.EncryptBitmap(original);

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

        
    }
}
