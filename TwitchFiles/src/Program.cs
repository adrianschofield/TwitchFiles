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

        //This is the global to hold the channel Id translated from the channel name at startup

        public static string twitchId = null;

        //DoWork with arguments is called from the app.xaml
        //DoWork with no arguments is called from NotifyIconViewModel.cs
        //Overloaded for ease of use and lazy coding

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
                TwitchChannelJson myChannel = myApi.TwitchChannelsQueryApi(args[0].ToLower());

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
                //Make a call to overloaded DoWork to avoid code duplication
                DoWork();
                System.Threading.Thread.Sleep(pollingInterval);
            } while (continuePolling);

            return;
        }


        //Lazy coding DoWork makes all the necessary Twitch API calls.
        public static void DoWork()
        {
            CallTwitchApi("channels");
            CallTwitchApi("channelcommunity");
            CallTwitchApi("channelfollows");
            return;
        }


        //Wrapper to make the relevant Twitch API calls

        public static void CallTwitchApi(string ApiName)
        {
            TwitchApi myApi = new TwitchApi();
            Utilities myUtils = new Utilities();

            //Using a switch to pick out the relevant API calls
            switch (ApiName)
            {
                case "channels":

                    TwitchChannel myChannel = myApi.TwitchChannelsApi(twitchId);
                    //We have a List

                    if (myChannel != null)
                    {
                        //We only got one response in our list
                        //Console.WriteLine("Current game is {0}", myChannel.channels.ElementAt(0).game);
                        myUtils.DebugLog(string.Format("Current game is {0}", myChannel.game));
                        myUtils.WriteFile(myChannel.game, "Game.txt");
                        myUtils.WriteFile(myChannel.followers.ToString(), "NumberofFollowers.txt");
                        myUtils.WriteFile(myChannel.status, "Status.txt");
                    }
                    break;
                case "channelsquery":

                    TwitchChannelJson myTwitchChannelJson = myApi.TwitchChannelsQueryApi(twitchId);
                    break;
                case "channelcommunity":
                    TwitchCommunity myTwitchCommunity = myApi.TwitchChannelCommunityApi(twitchId);
                    myUtils.WriteFile(myTwitchCommunity.name, "Community.txt");
                    break;
                case "channelfollows":
                    TwitchFollowsJson myTwitchFollowsJson = myApi.TwitchChannelFollows(twitchId);
                    string myFollowers = null;
                    foreach (TwitchFollow follow in myTwitchFollowsJson.follows)
                    {
                        myFollowers += follow.user.display_name + ", ";                 
                    }
                    myUtils.WriteFile(myFollowers, "Followers.txt");
                    break;
                default:
                    break;
            }

            return;
        }


    }
}
