﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BianCore.DataType.Minecraft.Launcher
{
    public struct ArgumentsStruct
    {
        public struct ArgumentStruct
        {
            public struct RuleStruct
            {
#nullable enable
                public bool IsAllow { get; set; }

                public string? OS_Name { get; set; }

                public string? OS_Arch { get; set; }
            }

            public string[] Values { get; set; }

            public RuleStruct[] Rules { get; set; }
        }

        [JsonIgnore]
        public ArgumentStruct[] Game { get; set; }

        [JsonIgnore]
        public ArgumentStruct[] JVM { get; set; }

        [JsonProperty("game")]
        private JToken game;

        [JsonProperty("jvm")]
        private JToken jvm;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            List<ArgumentStruct> args = new List<ArgumentStruct>();

            // game 参数
            if (game != null)
            {
                foreach (var item in game)
                {
                    if (item.Type == JTokenType.String)
                    {
                        ArgumentStruct arg = new ArgumentStruct
                        {
                            Values = new string[1] { item.ToString() }
                        };
                        args.Add(arg);
                    }
                }
                Game = args.ToArray();
                args.Clear();
            }

            // JVM 参数
            if (jvm != null)
            {
                foreach (var item in jvm)
                {
                    if (item.Type == JTokenType.String)
                    {
                        ArgumentStruct arg = new ArgumentStruct
                        {
                            Values = new string[1] { item.ToString() }
                        };
                        args.Add(arg);
                    }
                    else
                    {
                        ArgumentStruct arg = new ArgumentStruct();
                        if (item["value"].Type == JTokenType.String)
                        {
                            arg.Values = new string[1] { item["value"].ToString() };
                        }
                        else
                        {
                            List<string> strings = new List<string>();
                            foreach (var value in item["value"])
                            {
                                strings.Add(value.ToString());
                            }
                            arg.Values = strings.ToArray();
                        }

                        List<ArgumentStruct.RuleStruct> rules = new List<ArgumentStruct.RuleStruct>();
                        foreach (var rule in item["rules"])
                        {
                            var ruleData = new ArgumentStruct.RuleStruct
                            {
                                IsAllow = rule["action"]?.ToString() == "allow" || rule["action"]?.ToString() == null,
                                OS_Name = rule["os"]?["name"]?.ToString(),
                                OS_Arch = rule["os"]?["arch"]?.ToString()
                            };
                            rules.Add(ruleData);
                        }
                        arg.Rules = rules.ToArray();

                        args.Add(arg);
                    }
                }
                JVM = args.ToArray();
            }
        }
    }
}
