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

        public delegate void CreateWidgets(WidgetConfig[] widgets);
        public CreateWidgets Create_Widgets_After_Login;

        public void Init(string serviceUrl)
        {
            _httpManager = new HttpManager();
            _httpManager.Init(serviceUrl);
        }

        public async void Login()
        {
            if (_session != null && _session.IsLoggedIn) return;

            Debug.WriteLine("LOGGING IN");
            var loginToken = await _httpManager.StartSession();
            var sessionString = await _httpManager.ConfirmAuthentication(loginToken);
            _session = JsonConvert.DeserializeObject<Session>(sessionString);
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
    }
}
