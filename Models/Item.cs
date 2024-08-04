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
        public ItemCategory ItemsCategory { get; set; } = ItemCategory.Unassigned;

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

        public async Task AddNewItem(Item NewItem)
        {
            await App.Service.AddNewItem(NewItem);

        }
        
    }

}
