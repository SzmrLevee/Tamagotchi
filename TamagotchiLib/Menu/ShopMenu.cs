using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.Accounts;
using TamagotchiLib.Utils;

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
            bool exitShop = false; // Kontrollváltozó a bolt elhagyásához
            int selectedOption = 0;

            // Bolt menü opciók, beleértve a kilépést
            string[] shopItems =
            {
        "1. Étel (100 pénz)",
        "2. Ital (50 pénz)",
        "3. Gyógyszer (150 pénz)",
        "4. Csemege (120 pénz)",
        "5. Tápláló étel (200 pénz)",
        "6. Egészség javító ital (90 pénz)",
        "7. Mágikus édesség (250 pénz)",
        "8. Speciális gyógyszer (300 pénz)",
        "9. Játékos cukorka (80 pénz)",
        "10. Gyógyító fű (170 pénz)",
        "11. Mega étel (500 pénz)",
        "12. Kilépés a főmenübe" // Kilépési opció
    };

            while (!exitShop)
            {
                Console.Clear(); // Tisztítjuk a képernyőt a bolt újrarenderelése előtt
                Console.WriteLine("=== Bolt ===");
                Console.WriteLine();

                // Bolt menüpontok megjelenítése
                for (int i = 0; i < shopItems.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Aktív opció kiemelése
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(shopItems[i]);
                }
                Console.ResetColor();

                // Felhasználói bemenet
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow) // Fel
                {
                    selectedOption = (selectedOption == 0) ? shopItems.Length - 1 : selectedOption - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow) // Le
                {
                    selectedOption = (selectedOption == shopItems.Length - 1) ? 0 : selectedOption + 1;
                }
                else if (key.Key == ConsoleKey.Enter) // Kiválasztás
                {
                    if (selectedOption == shopItems.Length - 1) // Kilépés opció
                    {
                        exitShop = true; // Bolt elhagyása
                    }
                    else
                    {
                        ExecutePurchase(selectedOption); // Vásárlás végrehajtása
                    }
                }
                else if (key.Key == ConsoleKey.Escape) // ESC kilépés
                {
                    exitShop = true; // Bolt elhagyása
                }
            }

            Console.Clear(); // Bolt menüből kilépve töröljük a képernyőt
        }

        // Vásárlás végrehajtása
        private void ExecutePurchase(int selectedOption)
        {
            string itemName = "";
            int itemPrice = 0;

            switch (selectedOption)
            {
                case 0:
                    itemName = "Etel";
                    itemPrice = 100;
                    break;
                case 1:
                    itemName = "Ital";
                    itemPrice = 50;
                    break;
                case 2:
                    itemName = "Gyogyszer";
                    itemPrice = 150;
                    break;
                case 3:
                    itemName = "Csemege";
                    itemPrice = 120;
                    break;
                case 4:
                    itemName = "TaplaloEtel";
                    itemPrice = 200;
                    break;
                case 5:
                    itemName = "EJI";
                    itemPrice = 90;
                    break;
                case 6:
                    itemName = "MEdesseg";
                    itemPrice = 250;
                    break;
                case 7:
                    itemName = "SpecGyogyszer";
                    itemPrice = 300;
                    break;
                case 8:
                    itemName = "Cukorka";
                    itemPrice = 80;
                    break;
                case 9:
                    itemName = "GyogyFu";
                    itemPrice = 170;
                    break;
                case 10:
                    itemName = "MegaEtel";
                    itemPrice = 500;
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás!");
                    return;
            }

            if (gameManager.PlayerMoney >= itemPrice)
            {
                gameManager.PlayerMoney -= itemPrice;

                // Inventory frissítése fájlban
                Mentes mentes = new Mentes();
                mentes.UpdateInventory(Mentes.valasztottFiokNev, itemName, 1);

                Console.WriteLine($"Sikeresen vásároltál egy {itemName}-t {itemPrice} pénzért!");
            }
            else
            {
                Console.WriteLine("Nincs elég pénzed a vásárláshoz.");
            }

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
            Console.ReadKey(); // Várakozás a folytatáshoz
        }

    }
}
