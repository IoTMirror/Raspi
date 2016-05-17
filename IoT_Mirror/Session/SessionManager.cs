using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.ApplicationModel.Core;

namespace IoT_Mirror
{
    class SessionManager
    {
        private HttpManager _httpManager = null;
        private Session _session = null;
        private CameraManager _cameraManager = null;

        public delegate void CreateWidgets(WidgetConfig[] widgets);
        public CreateWidgets Create_Widgets_After_Login;

        public void Init(HttpManager httpManager)
        {
            _httpManager = httpManager;
            _cameraManager = new CameraManager();
        }

        public async void Login()
        {
            if (_session != null && _session.IsLoggedIn) return;

            Debug.WriteLine("LOGGING IN");
            var loginToken = await _httpManager.StartSession();
            var sessionString = await _httpManager.ConfirmAuthentication(loginToken);
            _session = JsonConvert.DeserializeObject<Session>(sessionString);
            Credentials.Token = JsonConvert.DeserializeAnonymousType(sessionString, new { Token = "" }).Token;
            Create_Widgets_After_Login(_session.Widgets);

            _session.IsLoggedIn = true;
        }

        public void Logout()
        {
            if (!_session.IsLoggedIn) return;

            Debug.WriteLine("LOGGING OUT");
            CoreApplication.Exit();

            _session.IsLoggedIn = false;
        }

        private async void SendPicture()
        {
            //TODO
            var cameraManager = new CameraManager();
            var photoStream = await cameraManager.TakePicture();
            //var photoString = await cameraManager.ConvertToByteArray(photoStream);
            //_httpManager.PushPhoto(serviceUri, photoString);
        }
    }
}
