using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LifeCMS.Services.ContentCreation.API.Services.Newsletter.HtmlGeneration
{
    class NodeTypeConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Object)
            {
                var resolvedTypeObject = token.Value<string>("resolvedName");

                return resolvedTypeObject;
            }
            else
            {
                return token.Value<string>();
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

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}