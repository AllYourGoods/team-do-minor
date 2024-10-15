using AllYourGoods.Api.Models.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AllYourGoods.Api.Models
{
    public class OpeningsTime : BaseEntity
    {
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly? Opening { get; set; }

        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly? Closing { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public Day? Day { get; set; }

        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }

    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}