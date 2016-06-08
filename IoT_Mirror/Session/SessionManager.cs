using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;

namespace IoT_Mirror
{
    class SessionManager
    {
        private HttpManager _httpManager = null;
        private Session _session = null;
        private CameraManager _cameraManager = null;
        private ProgressRing _progressRing = null;
        private TextBlock _sayHello = null;

        public delegate void CreateWidgets(WidgetConfig[] widgets);
        public CreateWidgets Create_Widgets_After_Login;

        public void Init(HttpManager httpManager, ProgressRing progressRing, TextBlock sayHello)
        {
            _httpManager = httpManager;
            _cameraManager = new CameraManager();
            _progressRing = progressRing;
            _sayHello = sayHello;
        }

        public async void Login()
        {
            if (_session != null && _session.IsLoggedIn) return;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                _progressRing.IsActive = true;
                _sayHello.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            });

            Debug.WriteLine("LOGGING IN");
            var startResponse = await _httpManager.StartSession();
            var loginToken = JsonConvert.DeserializeAnonymousType(startResponse, new { RecognitionToken = "" }).RecognitionToken;

            var photo = await _cameraManager.TakePicture();
            //var photoBytes = await _cameraManager.ConvertToByteArray(photo);
            var pushResponse = await _httpManager.PushPhoto(loginToken, photo);
            var isOk = JsonConvert.DeserializeAnonymousType(pushResponse, new { recognizedUser = 999 }).recognizedUser;  
            if(isOk == -1)
            {
                Debug.WriteLine("HERE NOOOOOO");
            }
            else
            {
                Debug.WriteLine("HERE YEEEEES");
            }
            var sessionString = await _httpManager.ConfirmAuthentication(loginToken);
            _session = JsonConvert.DeserializeObject<Session>(sessionString);
            Credentials.Token = JsonConvert.DeserializeAnonymousType(sessionString, new { Token = "" }).Token;
            Create_Widgets_After_Login(_session.Widgets);

            _session.IsLoggedIn = true;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                _progressRing.IsActive = false;
            });
        }

        public void Logout()
        {
            if (!_session.IsLoggedIn) return;

            Debug.WriteLine("LOGGING OUT");
            CoreApplication.Exit();

            _session.IsLoggedIn = false;
        }
    }
}
