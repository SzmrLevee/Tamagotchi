using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiLib.Models
{
    public class Item
    {
        public string Name { get; set; }  // Az item neve
        public int Quantity { get; set; } // Mennyisége

        public Item(string name, int quantity = 1)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
