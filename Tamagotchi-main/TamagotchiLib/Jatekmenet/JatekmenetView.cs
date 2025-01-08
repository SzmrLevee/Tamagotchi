using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TamagotchiLib.Accounts;
using TamagotchiLib.Animations;
using TamagotchiLib.Menu;
using TamagotchiLib.Models;

namespace TamagotchiLib.Jatekmenet
{
    public class JatekmenetView
    {
        private readonly Pet pet;

        public JatekmenetView(Pet pet)
        {
            this.pet = pet;
        }

        public void MainFuttatasa()
        {
            Console.Clear();
            Console.WriteLine($"Üdvözöllek, {pet.Name}!");
            Console.WriteLine("1. Játszik");
            Console.WriteLine("2. Alszik");
            Console.WriteLine("3. Inventory");
            Console.WriteLine("4. Vissza");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1)
                {
                    pet.Play();
                }
                else if (key == ConsoleKey.D2)
                {
                    pet.Sleep();
                }
                else if (key == ConsoleKey.D3)
                {
                    InventoryView inventoryView = new InventoryView(pet);
                    inventoryView.ShowInventory();
                }
                else if (key == ConsoleKey.D4)
                {
                    break;
                }
            }
        }
    }
}
