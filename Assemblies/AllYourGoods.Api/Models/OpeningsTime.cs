using AllYourGoods.Api.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AllYourGoods.Api.Models
{
    public class OpeningsTime : BaseEntity
    {
        [JsonConverter(typeof(TimeOnlyNullableJsonConverter))]
        public TimeOnly? Opening { get; set; }

        [JsonConverter(typeof(TimeOnlyNullableJsonConverter))]
        public TimeOnly? Closing { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Day? Day { get; set; }

        public Guid RestaurantId { get; set; }

        public virtual Restaurant? Restaurant { get; set; }

        public OpeningsTime(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public void SetTime(ref TimeOnly? timeField, string? timeString)
        {
            timeField = string.IsNullOrEmpty(timeString) ? null : TimeOnly.Parse(timeString);
        }
    }

    public class TimeOnlyNullableJsonConverter : JsonConverter<TimeOnly?>
    {
        public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var timeString = reader.GetString();
            return string.IsNullOrEmpty(timeString) ? (TimeOnly?)null : TimeOnly.Parse(timeString);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("HH:mm"));
            else
                writer.WriteNullValue();
        }
    }
}