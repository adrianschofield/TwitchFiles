using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchTools
{
    class Twitch
    {
        //Wrapper for the channels query API call
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

        //Wrapper for the followers on a specific channel API call
        public class TwitchFollowsJson
        {
            public int _total { get; set; }
            public string _cursor { get; set; }
            public List<TwitchFollow> follows { get; set; }
        }

        public class TwitchUser
        {
            public string display_name { get; set; }
            public string _id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string bio { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string logo { get; set; }
        }

        public class TwitchFollow
        {
            public string created_at { get; set; }
            public bool notifications { get; set; }
            public TwitchUser user { get; set; }
        }

        

        //Required for Communities Beta

        public class TwitchCommunity
        {
            public string _id { get; set; }
            public string owner_id { get; set; }
            public string name { get; set; }
            public string summary { get; set; }
            public string description { get; set; }
            public string description_html { get; set; }
            public string rules { get; set; }
            public string rules_html { get; set; }
            public string language { get; set; }
            public string avatar_image_url { get; set; }
            public string cover_image_url { get; set; }
        }
    }
}
