using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LifeCMS.Services.ContentCreation.Infrastructure.HtmlGeneration
{
    class JsonValueOrArrayConverter<T> : JsonConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Array)
            {
                var values = token.ToObject<List<T>>();

                return Tuple.Create(
                    values[0],
                    values[1],
                    values[2],
                    values[3]
                );
            }

            return new List<T> {
                token.ToObject<T>(),
                token.ToObject<T>(),
                token.ToObject<T>(),
                token.ToObject<T>(),
            };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(List<T>));
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
