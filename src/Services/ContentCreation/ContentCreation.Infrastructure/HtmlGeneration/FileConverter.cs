using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration
{
    public class FileConverter : JsonConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Object)
            {
                var name = token.Value<string>("name");

                var url = token.Value<string>("url");

                var size = token.Value<int>("size");

                return new CraftJsFile(name, url, size);
            }

            throw new Exception();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(string));
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
