using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.Utils;

namespace TamagotchiLib.Menu
{
    public class MainMenu
    {
        private readonly GameManager gameManager;

        public MainMenu(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("=== Minijátékok ===");
            Console.WriteLine("1. Találd ki a színt (Piros vagy Kék)");
            Console.WriteLine("2. Számolós játék");
            Console.Write("Választás: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                gameManager.PlayMinigame(choice);
            }
            else
            {
                Console.WriteLine("Érvénytelen bemenet.");
            }
            Console.WriteLine("Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }
    }
}
