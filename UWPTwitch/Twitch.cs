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

        public struct Game
        {
            public string name;
            public Logo logo;
            public int popularity;
        }

        struct TwitchGamesResponse
        {
            public Game[] games;
        }

        public struct Stream
        {
            public struct Channel
            {
                public string status;
                public uint followers;
                public uint views;
                public string name;
            }
            public Channel channel;
            public Logo preview;
            public uint viewers;
        }

        struct TwitchStreamsResponse
        {
            public Stream[] streams;
        }

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

        public async Task<Stream[]> GetStreams(string game)
        {
            string res = await web.GetStringAsync("https://api.twitch.tv/kraken/streams/?game=" + game);
            TwitchStreamsResponse resp = JsonConvert.DeserializeObject<TwitchStreamsResponse>(res);
            return resp.streams;
        }
    }
}