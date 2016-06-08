using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IoT_Mirror
{
    public sealed partial class ScrolledTextBlock : UserControl
    {
        private int _intervalInSeconds = 1000;
        private ScrolledVM _model = new ScrolledVM();

        private string _fullText = "This is very long text, no time for lorem ipsum. Please make it scrollable later.";

        public ScrolledTextBlock()
        {
            InitializeComponent();
            DataContext = _model;
            _model.VisibleText = _fullText + " ";
            _model.IntervalInSeconds = _intervalInSeconds;
        }

        public class ScrolledVM : INotifyPropertyChanged
        {
            private int _intervalInSeconds;
            public int IntervalInSeconds { get { return _intervalInSeconds; } set { _intervalInSeconds = value; CreateTimer(); } }
            private DispatcherTimer _timer = null;
            private string _visibleText = null;
            private string _fullText = null;

            public string VisibleText
            {
                get { return _visibleText; }
                set
                {
                    _fullText = value;
                    _visibleText = value;
                    CreateTimer();
                    NotifyPropertyChanged("VisibleText");
                }
            }

            private void CreateTimer()
            {
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer = null;
                }
                _timer = new DispatcherTimer();
                _timer.Interval = new TimeSpan(_intervalInSeconds * 1000);
                _timer.Tick += Callback;
                _timer.Start();
            }

            private void Callback(object sender, object e)
            {
                //if (string.IsNullOrEmpty(_visibleText))
                //{
                //    _timer.Stop();
                //    _timer.Interval = new TimeSpan(_intervalInSeconds * 1000);
                //    return;
                //}
                var move = _visibleText.First();
                VisibleText += move;
                VisibleText = _visibleText.Substring(1);
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(string info)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
