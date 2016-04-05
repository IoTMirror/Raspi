using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace IoT_Mirror
{
    public sealed partial class MainPage : Page
    {
        private WidgetManager _widgetManager = null;
        private Uri serviceUri = new Uri("http://jacek.com");

        public MainPage()
        {
            InitializeComponent();
            _widgetManager = new WidgetManager();
            _widgetManager.CreateWidget(grid, 0, 0, 0, 1, 1);
            _widgetManager.CreateWidget(grid, 0, 3, 3, 2, 2);
        }

        private async void SendPicture()
        {
            var cameraManager = new CameraManager();
            var photoStream = await cameraManager.TakePicture();
            var photoString = new ImageModel()
            {
                Image = await cameraManager.ConvertToByte64(photoStream)
            };
            (new HttpManager()).SendMessage(serviceUri, photoString);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SendPicture();
        }
    }
}
