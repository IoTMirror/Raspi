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
        private List<Task> _cached = new List<Task>();

        public TasksWidget()
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
                var tasksString = await _httpManager.GetTasks();
                Debug.WriteLine(tasksString);
                tasksString = JsonConvert.DeserializeAnonymousType(tasksString, new { error = "" }).error;
                var _tasks = JsonConvert.DeserializeObject<Tasks>(tasksString);
                if (_tasks.rest.SequenceEqual(_cached))
                    return;
                _cached =  _tasks.rest.ToList();
                listView.ItemsSource = _cached;
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
