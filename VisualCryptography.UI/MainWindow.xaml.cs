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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VisualCryptography.UI.Utils;

namespace VisualCryptography.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _originalImagePath;

        public MainWindow()
        {
            InitializeComponent();
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
                FirstImage.Source = new BitmapImage(new Uri(_originalImagePath));
                SecondImage.Source = new BitmapImage(new Uri(_originalImagePath));
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
                         "Portable Network Graphic (*.png)|*.png"
                };

                if (saveFileDialog.ShowDialog() != true) return;
                firstImageBitmap?.Save(saveFileDialog.FileName);
                secondImageBitmap?.Save(saveFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Nie udało się zapisać do pliku. Szczegóły błędu: {exception.Message}");
            }
        }

        private void EncryptImage_Click(object sender, RoutedEventArgs e)
        {
            if (FirstImage.Source != null && SecondImage.Source != null)
            {
                SaveDecryptedImagesToFile.IsEnabled = true;
            }
        }
    }
}
