using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using static TwitchTools.Twitch;

namespace TwitchTools
{
    class TwitchApi
    {
        //Force twitch API to be version 5
        private static string twitchAcceptv5 = "application/vnd.twitchtv.v5+json";
        //Client-ID required by Twitch
        private static string twitchClientId = "fqyfzn1c10xqpzc54uok2vuv6l7bs1";

        private Utilities myTwitchUtils = new Utilities();


        public TwitchChannelJson TwitchChannelsQueryApi(string user)
        {
            //TODO remove the query parameter and separate out into a new call
            //because the query and the normal return different types

            
            //Build an HttpRequest for the relevant Twitch endpoint
            HttpWebRequest myWebRequest = WebRequest.CreateHttp("https://api.twitch.tv/kraken/search/channels?query=" + user);

            //Add Headers and Accept to force v5
            myWebRequest = AddHeaders(myWebRequest);
           
            //Create the Json object outside the using so we can access it later
            TwitchChannelJson myChannel = null;

            //Make the http request and handle the response
            using (System.IO.Stream s = myWebRequest.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                    myTwitchUtils.DebugLog(String.Format("Response: {0}", jsonResponse));

                    //Deserialize the response using Newtonsoft and manage exceptions
                    try
                    {                       
                        myChannel = JsonConvert.DeserializeObject<TwitchChannelJson>(jsonResponse);
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Failed deserialzing with {0}", ex.Message);
                        myTwitchUtils.DebugLog(string.Format("Failed deserialzing with {0}", ex.Message));
                    }
                }
            }

            return myChannel;
        }

        public TwitchChannel TwitchChannelsApi(string user)
        {
            //TODO remove the query parameter and separate out into a new call
            //because the query and the normal return different types

            HttpWebRequest myWebRequest = null;
            
            myWebRequest = WebRequest.CreateHttp("https://api.twitch.tv/kraken/channels/" + user);


            //Add Headers and Accept to force v5
            myWebRequest = AddHeaders(myWebRequest);

            //Create the Json object outside the using so we can access it later
            TwitchChannel myChannel = null;

            //Make the http request and handle the response
            using (System.IO.Stream s = myWebRequest.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                    myTwitchUtils.DebugLog(String.Format("Response: {0}", jsonResponse));

                    //Deserialize the response using Newtonsoft and manage exceptions
                    try
                    {
                        //myChannel = new TwitchChannelJson();
                        //myChannel.channels = new List<TwitchChannel>();
                        //myChannel.channels.Add(JsonConvert.DeserializeObject<TwitchChannel>(jsonResponse));

                        myChannel = JsonConvert.DeserializeObject<TwitchChannel>(jsonResponse);

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Failed deserialzing with {0}", ex.Message);
                        myTwitchUtils.DebugLog(string.Format("Failed deserialzing with {0}", ex.Message));
                    }
                }
            }

            return myChannel;
        }

        public TwitchCommunity TwitchChannelCommunityApi(string user)
        {
            HttpWebRequest myWebRequest = null;

            myWebRequest = WebRequest.CreateHttp("https://api.twitch.tv/kraken/channels/" + user + "/community");

            //Add Headers and Accept to force v5
            myWebRequest = AddHeaders(myWebRequest);

            //Create the Json object outside the using so we can access it later
            TwitchCommunity myCommunity = null;

            //Make the http request and handle the response
            using (System.IO.Stream s = myWebRequest.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                    myTwitchUtils.DebugLog(String.Format("Response: {0}", jsonResponse));

                    //Deserialize the response using Newtonsoft and manage exceptions
                    try
                    {
                        //myChannel = new TwitchChannelJson();
                        //myChannel.channels = new List<TwitchChannel>();
                        //myChannel.channels.Add(JsonConvert.DeserializeObject<TwitchChannel>(jsonResponse));

                        myCommunity = JsonConvert.DeserializeObject<TwitchCommunity>(jsonResponse);

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Failed deserialzing with {0}", ex.Message);
                        myTwitchUtils.DebugLog(string.Format("Failed deserialzing with {0}", ex.Message));
                    }
                }
            }

            return myCommunity;
        }

        public TwitchFollowsJson TwitchChannelFollows(string id)
        {
            HttpWebRequest myWebRequest = null;

            myWebRequest = WebRequest.CreateHttp("https://api.twitch.tv/kraken/channels/" + id + "/follows?limit=100");

            //Add Headers and Accept to force v5
            myWebRequest = AddHeaders(myWebRequest);

            //Create the Json object outside the using so we can access it later
            TwitchFollowsJson myFollows = null;

            //Make the http request and handle the response
            using (System.IO.Stream s = myWebRequest.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                    myTwitchUtils.DebugLog(String.Format("Response: {0}", jsonResponse));

                    //Deserialize the response using Newtonsoft and manage exceptions
                    try
                    {
                        //myChannel = new TwitchChannelJson();
                        //myChannel.channels = new List<TwitchChannel>();
                        //myChannel.channels.Add(JsonConvert.DeserializeObject<TwitchChannel>(jsonResponse));

                        myFollows = JsonConvert.DeserializeObject<TwitchFollowsJson>(jsonResponse);

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Failed deserialzing with {0}", ex.Message);
                        myTwitchUtils.DebugLog(string.Format("Failed deserialzing with {0}", ex.Message));
                    }
                }
            }

            return myFollows;
        }

        //Add Headers and Accept, Method and Type to every HttpWebRequest
        private HttpWebRequest AddHeaders(HttpWebRequest myWebRequest)
        {
            //Add Headers and Accept to force v5
            myWebRequest.Headers.Add("Client-ID", twitchClientId);
            myWebRequest.Accept = twitchAcceptv5;

            //Method and type
            myWebRequest.Method = "GET";
            myWebRequest.ContentType = "application/json";

            return myWebRequest;
        }
    }
}
