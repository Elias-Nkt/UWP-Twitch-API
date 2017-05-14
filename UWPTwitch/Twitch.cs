using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UWPTwitch
{
    public class Twitch
    {
        public struct Logo
        {
            public string large;
            public string medium;
            public string small;
        }

        #region Games
        public struct Game
        {
            public string name;
            public Logo box;
            public int popularity;
        }

        struct TwitchGamesResponse
        {
            public Game[] games;
        }
        #endregion

        #region Streams
        public struct Stream
        {
            public struct Channel
            {
                public string status;
                public string name;
                public uint followers;
                public uint views;
            }
            public Channel channel;
            public Logo preview;
            public uint viewers;
        }

        struct TwitchStreamsResponse
        {
            public Stream[] streams;
        }
        #endregion

        #region Channels
        public struct Channel
        {
            public string display_name;
            public string game;
            public string status;
            public string logo;
            public string name;
            public uint views;
            public uint followers;
        }

        struct TwitchChannelsResponse
        {
            public Channel[] channels;
        }
        #endregion

        HttpClient web = new HttpClient();
        public Twitch(string api)
        {
            web.DefaultRequestHeaders.Add("Client-ID", api);
            web.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
        }

        public async Task<Game[]> SearchGames(string query)
        {
            string res = await web.GetStringAsync("https://api.twitch.tv/kraken/search/games/?query=" + query);
            TwitchGamesResponse resp = JsonConvert.DeserializeObject<TwitchGamesResponse>(res);
            return resp.games;
        }
        public async Task<Channel[]> SearchChannels(string query)
        {
            string res = await web.GetStringAsync("https://api.twitch.tv/kraken/search/channels/?query=" + query);
            TwitchChannelsResponse resp = JsonConvert.DeserializeObject<TwitchChannelsResponse>(res);
            return resp.channels;
        }

        public async Task<Stream[]> SearchStreams(string query)
        {
            string res = await web.GetStringAsync("https://api.twitch.tv/kraken/search/streams/?query=" + query);
            TwitchStreamsResponse resp = JsonConvert.DeserializeObject<TwitchStreamsResponse>(res);
            return resp.streams;
        }

        public async Task<Stream[]> GetLiveStreams(string game)
        {
            string res = await web.GetStringAsync("https://api.twitch.tv/kraken/streams/?game=" + game);
            TwitchStreamsResponse resp = JsonConvert.DeserializeObject<TwitchStreamsResponse>(res);
            return resp.streams;
        }
    }
}