﻿using BianCore.DataType.API.Modrinth;
using BianCore.Tools;
using Newtonsoft.Json;

namespace BianCore.API
{
    public static class Modrinth
    {
        public class V2
        {
            private Network network = new Network();
            public SearchResponse Search()
            {
                string url = "https://api.modrinth.com/v2/search";
                using var httpResponse = network.HttpGetAsync(url);
                string responseStr = httpResponse.Result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<SearchResponse>(responseStr);
                return response;
            }
            public SearchResponse Search(string content)
            {
                string url = "https://api.modrinth.com/v2/search";
                using var httpResponse = network.HttpGetAsync(url);
                string responseStr = httpResponse.Result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<SearchResponse>(responseStr);
                return response;
            }
        }
    }
}
