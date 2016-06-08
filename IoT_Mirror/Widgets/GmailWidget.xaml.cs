using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IoT_Mirror.Widgets.DataModels;

namespace IoT_Mirror
{
    public sealed partial class GmailWidget : UserControl
    {
        private HttpManager _httpManager = null;
        private DispatcherTimer _timer = null;
        private int _intervalInSeconds = 40;
        private List<Email> _cachedEmails = new List<Email>();

        public GmailWidget()
        {
            InitializeComponent();
        }

        public void Init(HttpManager httpManager)
        {
            _httpManager = httpManager;
            Refresh();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, _intervalInSeconds);
            _timer.Start();
        }

        public async void Refresh()
        {
            try
            {
                var emailString = await _httpManager.GetGmail();
                var _emails = JsonConvert.DeserializeObject<List<Email>>(emailString);
                if (_emails.SequenceEqual(_cachedEmails))
                    return;
                _cachedEmails = _emails;
                listView.ItemsSource = _emails;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to deserialize gmail");
            }
        }

        private void Callback(object sender, object e)
        {
            Refresh();
        }
    }
}
