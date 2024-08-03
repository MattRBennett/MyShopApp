using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Models
{
    public class Cart
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; } = decimal.Zero;
    }
}
