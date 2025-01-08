using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.Models;

namespace TamagotchiLib.Jatekmenet
{
    public class InventoryView
    {
        private readonly Pet pet;

        public InventoryView(Pet pet)
        {
            this.pet = pet;
        }

        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine($"=== {pet.Name} Inventory ===");
            if (!pet.Inventory.Any())
            {
                Console.WriteLine("Az inventory üres.");
            }
            else
            {
                for (int i = 0; i < pet.Inventory.Count; i++)
                {
                    var item = pet.Inventory[i];
                    Console.WriteLine($"{i + 1}. {item.Name} - Mennyiség: {item.Quantity}");
                }
            }
            Console.WriteLine("\nNyomj egy gombot a visszalépéshez.");
            Console.ReadKey();
        }
    }
}
