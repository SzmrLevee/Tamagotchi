using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.Utils;

namespace TamagotchiLib.Menu
{
    public class ShopMenu
    {
        private readonly GameManager gameManager;
        private readonly string[] shopItems =
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
            "11. Mega étel (500 pénz)"
        };

        public ShopMenu(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Display()
        {
            bool exitShop = false;
            int selectedOption = 0;

            string prompt = "=== Bolt ===";
            Console.Clear();
            Console.WriteLine(CenterText(prompt));

            DisplayShopMenu(selectedOption);

            while (!exitShop)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedOption = (selectedOption == 0) ? shopItems.Length - 1 : selectedOption - 1;
                    DisplayShopMenu(selectedOption);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOption = (selectedOption == shopItems.Length - 1) ? 0 : selectedOption + 1;
                    DisplayShopMenu(selectedOption);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    ExecutePurchase(selectedOption);
                    exitShop = true;
                }
            }
        }

        private void DisplayShopMenu(int selectedOption)
        {
            Console.SetCursorPosition(0, 2);  // A menüpontokat közvetlenül a bolt neve alá rajzoljuk
            for (int i = 0; i < shopItems.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                string centeredOption = CenterText(shopItems[i]); // Középre igazítjuk a menüpontot
                Console.WriteLine(centeredOption);        

                Console.ResetColor();
            }
        }

        // Vásárlás végrehajtása
        private void ExecutePurchase(int selectedOption)
        {
            string itemName = "";
            int itemPrice = 0;

            switch (selectedOption)
            {
                case 0:
                    itemName = "Étel";
                    itemPrice = 100;
                    break;
                case 1:
                    itemName = "Ital";
                    itemPrice = 50;
                    break;
                case 2:
                    itemName = "Gyógyszer";
                    itemPrice = 150;
                    break;
                case 3:
                    itemName = "Csemege";
                    itemPrice = 120;
                    break;
                case 4:
                    itemName = "Tápláló étel";
                    itemPrice = 200;
                    break;
                case 5:
                    itemName = "Egészség javító ital";
                    itemPrice = 90;
                    break;
                case 6:
                    itemName = "Mágikus édesség";
                    itemPrice = 250;
                    break;
                case 7:
                    itemName = "Speciális gyógyszer";
                    itemPrice = 300;
                    break;
                case 8:
                    itemName = "Játékos cukorka";
                    itemPrice = 80;
                    break;
                case 9:
                    itemName = "Gyógyító fű";
                    itemPrice = 170;
                    break;
                case 10:
                    itemName = "Mega étel";
                    itemPrice = 500;
                    break;
                default:
                    break;
            }

            // Vásárlás logikája
            gameManager.BuyItem(itemName, itemPrice);

            MenuUtils.DisplayPromptOnce(gameManager.Prompt);
        }

        // Középre igazító segédfüggvény
        static string CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2; // Kiszámoljuk a szükséges szóközöket
            return new string(' ', padding) + text; // A szükséges szóközökkel hozzáfűzzük a szöveget
        }
    }
}
