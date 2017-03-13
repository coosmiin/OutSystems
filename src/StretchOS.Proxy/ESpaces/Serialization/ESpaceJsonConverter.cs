using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace StretchOS.Proxy.ESpaces.Serialization
{
	public class ESpaceJsonConverter : JsonConverter
	{
		readonly Type _type;

		public ESpaceJsonConverter(Type type)
		{
			_type = type;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == _type;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JToken token = JToken.FromObject(value);

			token.WriteTo(writer);

			if (token.Type != JTokenType.Object)
			{
				token.WriteTo(writer);
				return;
			}

			var jObject = (JObject)token;
		}
	}
}
