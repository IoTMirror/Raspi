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
    public sealed partial class TasksWidget : UserControl
    {
        private HttpManager _httpManager = null;
        private DispatcherTimer _timer = null;
        private int _intervalInSeconds = 60;

        public TasksWidget()
        {
            this.InitializeComponent();
        }

        public void Init(HttpManager httpManager)
        {
            _httpManager = httpManager;
            Refresh();
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, _intervalInSeconds);
            _timer.Tick += Callback;
            _timer.Start();
                //new Timer(Callback, null, _intervalInSeconds * 1000, Timeout.Infinite);
        }

        public async void Refresh()
        {
            //{"timed": [], "rest": [{"tasklist_info": {"title": "Dawid Chr\u00f3\u015bcielski's list"}, "title": "Testowe zadanie"}, {"tasklist_info": {"title": "Dawid Chr\u00f3\u015bcielski's list"}, "title": "Sko\u0144czy\u0107 projekt"}]}
            try
            {
                var tasksString = await _httpManager.GetTasks();
                Debug.WriteLine(tasksString);
                var _tasks = JsonConvert.DeserializeObject<Tasks>(tasksString);
                listView.ItemsSource = _tasks.rest;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to deserialize tasks");
            }
        }

        private void Callback(object sender, object e)
        {
            Refresh();
        }
    }
}
