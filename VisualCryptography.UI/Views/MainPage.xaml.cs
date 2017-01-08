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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void EncryptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EncryptionPage());
        }

        private void DecryptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DecryptionPage());
        }
    }
}
