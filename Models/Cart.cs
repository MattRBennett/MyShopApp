using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public string CartItems { get; set; } = string.Empty;
    }
}
