using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Mirror.Widgets.DataModels
{
    class Tweet : IEquatable<Tweet>
    {
        public class UserModel
        {
            [JsonProperty("screen_name")]
            public string Name { get; set; }
        }

        [JsonProperty("user")]
        public UserModel User { get; set; }

        [JsonProperty("created_at")]
        public string CreationDate { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public bool Equals(Tweet other)
        {
            return Text == other.Text;
        }

        public override int GetHashCode()
        {
            int first = Text == null ? 0 : Text.GetHashCode();
            int second = User.Name.GetHashCode();
            return first ^ second;
        }

        public override string ToString()
        {
            return "@" + User.Name + ": " + Text;
        }
    }
}
