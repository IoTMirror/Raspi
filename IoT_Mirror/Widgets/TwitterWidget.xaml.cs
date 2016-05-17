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
    public sealed partial class TwitterWidget : UserControl
    {
        private HttpManager _httpManager = null;
        private Timer _timer = null;
        private int _intervalInSeconds = 30;
        private List<Tweet> _cachedTweets = new List<Tweet>();

        public TwitterWidget()
        {
            InitializeComponent();
        }

        public void Init(HttpManager httpManager)
        {
            _httpManager = httpManager;
            Refresh();
            _timer = new Timer(Callback, null, _intervalInSeconds * 1000, Timeout.Infinite);
        }

        public async void Refresh()
        {
            try
            {
                var tweetsString = await _httpManager.GetTweets();
                var _tweets = JsonConvert.DeserializeObject<List<Tweet>>(tweetsString);
                if (_tweets.SequenceEqual(_cachedTweets))
                    return;
                _cachedTweets = _tweets;
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        listView.ItemsSource = _tweets;
                    });
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to deserialize tweets");
            }
        }

        private void Callback(Object state)
        {
            Refresh();
            _timer.Change(_intervalInSeconds * 1000, Timeout.Infinite);
        }
    }
}
