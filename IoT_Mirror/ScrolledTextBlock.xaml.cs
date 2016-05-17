using System;
using System.Collections.Generic;
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
        private int _intervalInSeconds = 1;
        private Timer _timer = null;
        private string _fullText = "This is very long text, no time for lorem ipsum. Please make it scrollable later.";
        private string _visibleText = null;
        public ScrolledTextBlock()
        {
            InitializeComponent();
            _visibleText = _fullText;
            Binding b = new Binding();
            b.Mode = BindingMode.TwoWay;
            b.Source = _visibleText;

            textBlock.SetBinding(TextBlock.TextProperty, b);
            _timer = new Timer(Callback, null, _intervalInSeconds * 1000, Timeout.Infinite);
        }

        private void Callback(Object state)
        {
            _visibleText = _visibleText.Substring(1);
            Debug.WriteLine(_visibleText);
            _timer.Change(_intervalInSeconds * 1000, Timeout.Infinite);
        }
    }
}
