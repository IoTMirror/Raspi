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
        private SessionManager _sessionManager = null;
        private SpeechManager _speechManager = null;
        private string serviceUrl = Credentials.ServiceUrl;

        public MainPage()
        {
            InitializeComponent();
            _widgetManager = new WidgetManager();
            _widgetManager.Init(grid);

            _sessionManager = new SessionManager();
            _sessionManager.Init(serviceUrl);
            _sessionManager.Create_Widgets_After_Login += _widgetManager.CreateWidgets;

            _speechManager = new SpeechManager();
            _speechManager.Init();
            _speechManager.Login_Start += _sessionManager.Login;
            _speechManager.Logout_Start += _sessionManager.Logout;
        }

        //private async void SendPicture()
        //{
        //    var cameraManager = new CameraManager();
        //    var photoStream = await cameraManager.TakePicture();
        //    var photoString = new ImageModel()
        //    {
        //        Image = await cameraManager.ConvertToByte64(photoStream)
        //    };
        //    //(new HttpManager()).PushPhoto(serviceUri, photoString);
        //}
    }
}
