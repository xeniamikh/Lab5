using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BooksCatalog;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    [Description("Название")]
    Title,
    [Description("Автор")]
    Author,
    [Description("ISBN")]
    ISBN,
    [Description("Ключевые слова")]
    Keyword
}