using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace IoT_Mirror
{
    class WidgetManager
    {
        private Grid _grid = null;
        public void Init(Grid grid)
        {
            _grid = grid;
        }

        public async void CreateWidgets(WidgetConfig[] widgets)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                foreach (var i in widgets)
                {
                    CreateWidget(i);
                }
            });
        }

        public void CreateWidget(WidgetConfig widget)
        {
            if (widget.WidgetName == "Twitter")
            {
                var control = new TwitterWidgetSmall();
                Grid.SetRow(control, widget.WidgetPosition.Y);
                Grid.SetColumn(control, widget.WidgetPosition.X);
                Grid.SetColumnSpan(control, widget.WidgetSize.X);
                Grid.SetRowSpan(control, widget.WidgetSize.Y);
                _grid.Children.Add(control);
            }
            else
            {
                var control = new MyUserControl1(widget.WidgetName);
                Grid.SetRow(control, widget.WidgetPosition.Y);
                Grid.SetColumn(control, widget.WidgetPosition.X);
                Grid.SetColumnSpan(control, widget.WidgetSize.X);
                Grid.SetRowSpan(control, widget.WidgetSize.Y);
                _grid.Children.Add(control);
            }
        }
    }
}
