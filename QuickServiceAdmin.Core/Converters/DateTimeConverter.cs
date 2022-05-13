using System;
using System.Globalization;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            writer.WriteValueAsync(value?.ToString(
                "MMMM dd, yyyy hh:mm tt", CultureInfo.InvariantCulture));
        }

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return existingValue;
        }
    }
}