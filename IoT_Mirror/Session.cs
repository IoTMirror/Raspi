using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Mirror
{
    class Session
    {
        [Newtonsoft.Json.JsonIgnore]
        public bool IsLoggedIn { get; set; }
        public string Token { get; set; }
        public WidgetConfig[] Widgets { get; set; }
    }
}
