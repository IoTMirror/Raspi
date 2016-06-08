using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Mirror.Widgets.DataModels
{
    public class Task
    {
        public class TaskListInfo
        {
            public string title { get; set; }
        }

        [JsonProperty("title")]
        public string Title { get; set; }
        public TaskListInfo tasklist_info { get; set; }
    }
}
