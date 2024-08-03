using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyShopApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemCategory
    {
        Unasssigned = 1,

        Electronics = 2,

        HomeAppliances = 3,

        Fashion = 4,

        HealthAndBeauty = 5,

        SportsAndOutdoors = 6,

        BooksAndMedia = 7,

        ToysAndGames = 8,

        HomeAndFurniture = 9,

        Groceries = 10,

        Automotive = 11


    }
}
