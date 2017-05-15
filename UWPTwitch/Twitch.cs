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

        #region DirectURL
        struct TwitchTokenResponse
        {
            public string token;
            public string sig;
        }
        public struct StreamQuality
        {
            public string source;
            public string high;
            public string medium;
            public string low;
            public string mobile;
        }
        #endregion

        HttpClient web = new HttpClient();
        HttpClient web_user = new HttpClient();

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

        public async Task<StreamQuality> GetQualities(string channel)
        {
            string res = await web.GetStringAsync("http://api.twitch.tv/api/channels/" + Uri.EscapeDataString(channel) + "/access_token");
            TwitchTokenResponse tok = JsonConvert.DeserializeObject<TwitchTokenResponse>(res);

            res = await web_user.GetStringAsync("http://usher.twitch.tv/api/channel/hls/" + Uri.EscapeDataString(channel) + ".m3u8?" +
                                           "player=twitchweb&token=" + Uri.EscapeDataString(tok.token) + "&sig=" + Uri.EscapeDataString(tok.sig) +
                                           "&allow_audio_only=true&allow_source=true&type=any&p=23423");


            StreamQuality quality = new StreamQuality();

            string[] m3u = res.Split(new string[] { "#EXT-X-STREAM-INF" }, StringSplitOptions.None);

            foreach (string s in m3u)
            {
                if (!s.Contains("VIDEO=\""))
                    continue;

                string type = s.Split(new string[] { "VIDEO=\"" }, StringSplitOptions.None)[1].Split('"')[0];
                if (type == "720p60")
                {
                    quality.high = s.Split('\n')[1];
                }
                else if (type == "480p30")
                {
                    quality.medium = s.Split('\n')[1];
                }
                else if (type == "360p30")
                {
                    quality.low = s.Split('\n')[1];
                }
                else if (type == "160p30")
                {
                    quality.mobile = s.Split('\n')[1];
                }
                else if (type == "chunked")
                {
                    quality.source = s.Split('\n')[1];
                }
            }
            return quality;
        }
    }
}