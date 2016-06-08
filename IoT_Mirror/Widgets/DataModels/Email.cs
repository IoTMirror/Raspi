using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Mirror.Widgets.DataModels
{
    class Email : IEquatable<Email>
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        public bool Equals(Email other)
        {
            return Subject == other.Subject && 
                From == other.From &&
                To == other.To &&
                Date == other.Date;
        }

        public override int GetHashCode()
        {
            int first = Subject == null ? 0 : Subject.GetHashCode();
            int second = Date.GetHashCode();
            int third = From.GetHashCode();
            return first ^ second ^ third;
        }
    }
}
