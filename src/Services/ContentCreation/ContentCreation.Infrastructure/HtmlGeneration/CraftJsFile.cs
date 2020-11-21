using Newtonsoft.Json;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration
{
    public class CraftJsFile
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("size")]
        public int Size { get; private set; }

        public CraftJsFile(string name, string url, int size)
        {
            Name = name;

            Url = url;

            Size = size;
        }
    }
}
