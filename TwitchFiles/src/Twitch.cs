using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchTools
{
    class Twitch
    {
        public class TwitchChannelJson
        {
            public int _total { get; set; }
            public List<TwitchChannel> channels { get; set; }
        }

        public class TwitchChannel
        {
            public bool mature { get; set; }
            public string status { get; set; }
            public string broadcaster_language { get; set; }
            public string game { get; set; }
            public string language { get; set; }
            public uint _id { get; set; }
            public string name { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public bool partner { get; set; }
            public string logo { get; set; }
            public string video_banner { get; set; }
            public string profile_banner { get; set; }
            public string profile_banner_background_color { get; set; }
            public string url { get; set; }
            public uint views { get; set; }
            public uint followers { get; set; }
        }
    }
}
