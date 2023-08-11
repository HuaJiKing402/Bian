﻿using BianCore.Tools;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;

namespace BianCore.API
{
    public class Bing : IDisposable
    {
        private Network network = new Network();
        private bool disposedValue;

        public string Url;
        public string UrlBase;
        public string Copyright;
        public string Title;

        public Bing()
        {
            JObject Data = Json.ToJson(network.HttpGet("https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN").Content.ReadAsStringAsync().Result.ToString()) ?? throw new NullReferenceException();
            var JData = Data["images"] ?? throw new NullReferenceException();
            var image = JData[0] ?? throw new NullReferenceException();
            Url = "https://cn.bing.com" + (string)image["url"];
            UrlBase = "https://cn.bing.com" + (string)image["urlbase"];
            Copyright = (string)image["copyright"];
            Title = (string)image["title"];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    network.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
