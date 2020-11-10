using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LifeCMS.Services.ContentCreation.API.Services.Newsletters.HtmlGeneration
{
    public class CraftJsObject : Dictionary<string, CraftJsNode> { }
    public class CraftJsProps
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("fontSize")]
        public int FontSize { get; set; }

        [JsonProperty("padding")]
        [JsonConverter(typeof(JsonValueOrArrayConverter<int>))]
        public Tuple<int, int, int, int> Padding { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public Dictionary<string, string> GetAttributes()
        {
            var attributes = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(ClassName))
            {
                attributes.Add("className", ClassName);
            }

            if (!string.IsNullOrEmpty(Id))
            {
                attributes.Add("id", Id);
            }

            attributes.Add("style", GetStyleString());

            return attributes;
        }

        private string GetStyleString()
        {
            var styleString = string.Join(
                "; ",
                GetStyleProperties().Select(attr => $"{attr.Key}: {attr.Value}")
            );

            return !string.IsNullOrEmpty(styleString)
                ? styleString + ";"
                : "";
        }

        private Dictionary<string, string> GetStyleProperties()
        {
            var props = new Dictionary<string, string>();

            if (Padding != null)
            {
                var paddingValues = new int[]
                {
                    Padding.Item1,
                    Padding.Item2,
                    Padding.Item3,
                    Padding.Item4,
                };

                props.Add("padding", StringifyValue(paddingValues, "rem"));
            }

            if (FontSize > 0)
            {
                props.Add("font-size", $"{FontSize}rem");
            }

            return props;
        }

        public string StringifyValue(int[] values, string unit)
        {
            return string.Join(" ", values.Select(v => $"{v}{unit}"));
        }
    }

    public class CraftJsNode
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(NodeTypeConverter))]
        public string Type { get; set; }

        [JsonProperty("nodes")]
        public IList<string> Nodes { get; set; }

        [JsonProperty("props")]
        public CraftJsProps Props { get; set; }
    }

    public class CraftJsBody
    {
        public CraftJsObject RootObject { get; private set; }

        public CraftJsBody(CraftJsObject rootObject)
        {
            RootObject = rootObject;
        }

        public static CraftJsBody Parse(string body)
        {
            var root = JsonConvert.DeserializeObject<CraftJsObject>(body);

            return new CraftJsBody(root);
        }

        public CraftJsNode GetNode(string key)
        {
            var res = RootObject.TryGetValue(key, out var node);

            return res ? node : null;
        }
    }
}
