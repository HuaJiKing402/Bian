﻿using Newtonsoft.Json;
namespace BianCore.DataType.API.Modrinth
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
    }
}
