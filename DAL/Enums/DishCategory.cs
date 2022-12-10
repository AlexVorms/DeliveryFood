using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace WebApplication2.DAL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DishCategory
    {
        WOK,
        Pizza,
        Soup,
        Dessert,
        Drink
    }
}
