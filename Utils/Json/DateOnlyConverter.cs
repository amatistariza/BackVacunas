using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Utils.Json
{
    /// <summary>
    /// Serializa DateTime / DateTime? como solo fecha "yyyy-MM-dd" ignorando componente hora.
    /// Al deserializar acepta formatos ISO y recorta la hora a Date (UTCDate si especifica Z).
    /// </summary>
    public class DateOnlyConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s)) return default;
                if (DateTime.TryParse(s, out var dt))
                {
                    return dt.Date;
                }
                throw new JsonException($"Formato de fecha inválido: {s}");
            }
            throw new JsonException($"Token inesperado {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Date.ToString(Format));
        }
    }

    public class NullableDateOnlyConverter : JsonConverter<DateTime?>
    {
        private const string Format = "yyyy-MM-dd";
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s)) return null;
                if (DateTime.TryParse(s, out var dt))
                {
                    return dt.Date;
                }
                throw new JsonException($"Formato de fecha inválido: {s}");
            }
            throw new JsonException($"Token inesperado {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.Date.ToString(Format));
            else
                writer.WriteNullValue();
        }
    }
}
