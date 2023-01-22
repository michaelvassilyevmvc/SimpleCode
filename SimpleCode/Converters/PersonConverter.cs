using SimpleCode.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleCode.Converters
{
    public class PersonConverter : JsonConverter<Person>
    {
        public override Person? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var personName = "Undefined";
            var personAge = 0;

            while (reader.Read())
            {
                if(reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName?.ToLower())
                    {
                        case "age" when reader.TokenType == JsonTokenType.Number:
                            personAge = reader.GetInt32();
                            break;
                        case "age" when reader.TokenType == JsonTokenType.String:
                            string? stringValue = reader.GetString();
                            if(int.TryParse(stringValue, out int value))
                            {
                                personAge = value;
                            }
                            break;
                        case "name":
                            string? name = reader.GetString();
                            if (name != null) personName = name;
                            break;
                    }
                }
            }
            return new Person { Name = personName, Age= personAge };
        }

        public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("name",person.Name);
            writer.WriteNumber("age", person.Age);

            writer.WriteEndObject();
        }
    }
}