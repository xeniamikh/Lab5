
using System.Text.Json.Serialization;

namespace BooksCatalog
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SavingType
    {
        JSon = 1,
        Xml = 2,
        SqLite = 3
    }
}
