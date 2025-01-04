using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiLib.Menu
{
    public class ShopMenu
    {
        private readonly GameManager gameManager;

        public ShopMenu(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("=== Item Shop ===");
            Console.WriteLine("(Egyelőre nincs elérhető item.)");
            Console.WriteLine("Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }
    }
}
