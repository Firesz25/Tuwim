using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using Newtonsoft.Json;

namespace DiscordBot
{
    class Program
    {
        private static List<Item> LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                return items;
            }
        }
        

        private class Item
        {
            public string prefix;
            public string token;
            
        }

        private static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
        private static string Getjson(string fileinjson)
        {
            var config = LoadJson("configBot.json");
            {
                foreach (var conf in config)
                {
                    if (fileinjson == "prefix")
                    {
                        fileinjson = conf.prefix;
                    }else if (fileinjson == "token")
                    {
                        fileinjson = conf.token;
                    }
                }
            }
            return fileinjson;
        }

        private static async Task MainAsync()
        {

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = Getjson("token"),
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping")) 
                    await e.Message.RespondAsync("pong!");

            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}