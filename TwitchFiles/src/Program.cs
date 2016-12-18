using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using static TwitchTools.Twitch;
using static TwitchTools.TwitchApi;
using static TwitchTools.Utilities;



namespace TwitchTools
{
    class Program
    {

        //public static Config myConfig = new Config();

        public static string twitchId = null;

        
        public static void DoWork(string[] args)
        {
            const int pollingInterval = 5 * 60 * 1000;
            bool continuePolling = true;

            TwitchApi myApi = new TwitchApi();
            Utilities myUtils = new Utilities();

            if (args.Length == 0)
            {
                //There were no arguments the only way we have to alert the user is via a log
                
                myUtils.WriteFile("No User name specified please refer to READ.MD", "error.log");
            }
            else
            {
                //Let's assume that the first argument is the channel name
                //We really need the _Id for the channel as this is required per v5
                TwitchChannelJson myChannel = myApi.TwitchChannelsApi(args[0].ToLower(), true);

                //We should have a channel or list of channels
                if (myChannel.channels.Count == 1)
                {
                    //We only got one channel back from the API call, extract the _id change the global
                    twitchId = (myChannel.channels.ElementAt(0)._id).ToString();
                }
                else
                {
                    foreach (TwitchChannel channel in myChannel.channels)
                    {
                        //We need an exact match for the channel name we've been supplied
                        if(channel.name.ToLower() == args[0].ToLower())
                        {
                            twitchId = (channel._id).ToString();
                            break;
                        }
                        
                    }

                }
            }

            //Let's set up our polling interval

            do
            {
                CallTwitchApi("channels");
                System.Threading.Thread.Sleep(pollingInterval);
            } while (continuePolling);

            return;
        }


        public static void DoWork()
        {
            CallTwitchApi("channels");
            return;
        }

        public static void CallTwitchApi(string ApiName)
        {
            TwitchApi myApi = new TwitchApi();
            Utilities myUtils = new Utilities();

            switch (ApiName)
            {
                case "channels":

                    TwitchChannelJson myChannel = myApi.TwitchChannelsApi(twitchId, false);
                    //We have a List

                    if (myChannel.channels.Count == 1)
                    {
                        //We only got one response in our list
                        //Console.WriteLine("Current game is {0}", myChannel.channels.ElementAt(0).game);
                        myUtils.DebugLog(string.Format("Current game is {0}", myChannel.channels.ElementAt(0).game));
                        myUtils.WriteFile(myChannel.channels.ElementAt(0).game, "Game.txt");
                    }
                    else
                    {
                        //We got more than one reponse in the list so need to parse it further
                        //TODO parse the list further to ensure only one value returned.
                        foreach (TwitchChannel channel in myChannel.channels)
                        {

                            //Console.WriteLine("Current game is {0}", channel.game);
                            myUtils.DebugLog(string.Format("Current game is {0}", channel.game));

                        }
                    }
                    break;
            }

            return;
        }


    }
}
