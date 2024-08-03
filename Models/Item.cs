using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = "New Item Name";

        public string Description { get; set; } = "New Item Description";

        public decimal Price { get; set; } = decimal.Zero;

        public ItemCategory ItemsCategory { get; set; } = ItemCategory.Unasssigned;

        public async Task<List<Item>> GetItems()
        {
            List<Item> items = new();
            var itemsindb = await App.Service.GetAllItems();

            items.AddRange(itemsindb);
            return items;
        }

        public async Task<Item> GetItemItemID(int ID)
        {
            Item item = await App.Service.GetItemByItemID(ID);
            return item;
        }

        public string GetCategoryNameByID(int CategoryID)
        {
            string Name = string.Empty;
            switch (CategoryID)
            {
                case 1:
                    Name = "Unassigned";
                    break;
                case 2:
                    Name = "Electronics";
                    break;
                case 3:
                    Name = "Home Appliances";
                    break;
                case 4:
                    Name = "Fashion";
                    break;
                case 5:
                    Name = "Health & Beauty";
                    break;
                case 6:
                    Name = "Sports & Outdoors";
                    break;
                case 7:
                    Name = "Books & Media";
                    break;
                case 8:
                    Name = "Toys & Games";
                    break;
                case 9:
                    Name = "Home & Furniture";
                    break;
                case 10:
                    Name = "Groceries";
                    break;
                case 11:
                    Name = "Automotive";
                    break;
                default:
                    Name = "Unassigned";
                    break;
            }
            return Name;
        }
    }

}
