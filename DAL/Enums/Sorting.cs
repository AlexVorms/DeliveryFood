using System.Text.Json.Serialization;

namespace WebApplication2.DAL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        NameAsc = 0,
        NameDesc = 3,
        PriceAsc = 1,
        PriceDesc = 4,
        RatingAsc = 2,
        RatingDesc = 5
    }
}
