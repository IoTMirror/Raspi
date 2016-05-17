using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Mirror.Widgets.DataModels
{
    class EmailMessageInfo : IEquatable<EmailMessageInfo>
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string date { get; set; }

        public bool Equals(EmailMessageInfo other)
        {
            return subject == other.subject && 
                from == other.from &&
                to == other.to &&
                date == other.date;
        }

        public override int GetHashCode()
        {
            int first = subject == null ? 0 : subject.GetHashCode();
            int second = date.GetHashCode();
            int third = from.GetHashCode();
            return first ^ second ^ third;
        }
    }
}
