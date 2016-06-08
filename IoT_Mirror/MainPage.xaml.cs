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
        private HttpManager _httpManager = null;
        private SessionManager _sessionManager = null;
        private SpeechManager _speechManager = null;

        public MainPage()
        {
            InitializeComponent();

            _httpManager = new HttpManager();
            _httpManager.Init(Credentials.ServiceUrl);

            _widgetManager = new WidgetManager();
            _widgetManager.Init(grid, _httpManager);

            _sessionManager = new SessionManager();
            _sessionManager.Init(_httpManager, progressRing, sayHello);
            _sessionManager.Create_Widgets_After_Login += _widgetManager.CreateWidgets;

            _speechManager = new SpeechManager();
            _speechManager.Init();
            _speechManager.Login_Start += _sessionManager.Login;
            _speechManager.Logout_Start += _sessionManager.Logout;
        }

        private void buttonTmp_Click(object sender, RoutedEventArgs e)
        {
            _sessionManager.Login();
        }
    }
}
