using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Mirror.Widgets.DataModels
{
    class Tweet : IEquatable<Tweet>
    {
        public class User
        {
            public string screen_name { get; set; }
        }
        public User user { get; set; }
        public string created_at { get; set; }
        public string text { get; set; }

        public bool Equals(Tweet other)
        {
            return text == other.text;
        }

        public override int GetHashCode()
        {
            int first = text == null ? 0 : text.GetHashCode();
            int second = user.screen_name.GetHashCode();
            return first ^ second;
        }
    }
}
