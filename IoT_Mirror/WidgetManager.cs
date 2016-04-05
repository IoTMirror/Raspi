using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace IoT_Mirror
{
    class WidgetManager
    {
        public void CreateWidget(Grid grid, int type, int x, int y, int width, int height)
        {
            if(type == 0)
            {
                var control = new MyUserControl1();
                Grid.SetRow(control, y);
                Grid.SetColumn(control, x);
                Grid.SetColumnSpan(control, width);
                Grid.SetRowSpan(control, height);
                grid.Children.Add(control);
            }
        }
    }
}
