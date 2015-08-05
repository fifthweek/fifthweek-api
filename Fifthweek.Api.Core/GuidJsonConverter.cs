namespace Fifthweek.Api.Core
{
    using System;

    using Fifthweek.Shared;

    using Newtonsoft.Json;

    public class GuidJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var guid = (Guid)value;
            writer.WriteValue(guid.EncodeGuid());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ((string)reader.Value).DecodeGuid();
        }
    }
}