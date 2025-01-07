using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.MiniGames;

namespace TamagotchiLib.Menu
{
    public class MiniGamesMenu
    {
        private readonly GameManager gameManager;
        private readonly string[] minigameOptions =
        {
            "Találd ki a színt (Piros vagy Kék)",
            "Számolós játék",
            "Vissza a főmenübe"
        };

        public MiniGamesMenu(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Display()
        {
            bool exitMinigame = false;
            int selectedOption = 0;

            while (!exitMinigame)
            {
                Console.Clear();
                string prompt = "=== Minijátékok ===";
                Console.WriteLine(CenterText(prompt));

                int verticalPaddingMenu = (Console.WindowHeight - minigameOptions.Length - 2) / 2;
                for (int i = 0; i < verticalPaddingMenu; i++)
                {
                    Console.WriteLine();
                }

                // Kiíratjuk a menüpontokat
                for (int i = 0; i < minigameOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    string centeredOption = CenterText(minigameOptions[i]);
                    Console.WriteLine(centeredOption);

                    Console.ResetColor();
                }

                GameManager gameManager = new GameManager();

                // Felhasználói bemenet kezelése
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedOption = (selectedOption == 0) ? minigameOptions.Length - 1 : selectedOption - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOption = (selectedOption == minigameOptions.Length - 1) ? 0 : selectedOption + 1;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            Console.Clear();
                            new ColorGuessGame().Play(gameManager.CurrentPet);
                            break;
                        case 1:
                            Console.Clear();
                            new MathChallengeGame().Play(gameManager.CurrentPet);
                            break;
                        case 2:
                            Console.Clear();
                            exitMinigame = true;

                            MenuUtils.DisplayPromptOnce(gameManager.Prompt);
                            break;
                    }
                }
            }
        }

        // Középre igazító segédfüggvény
        public static string CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;

            // Ha a szöveg hosszabb, mint a konzol szélessége, vágjuk le
            if (text.Length > windowWidth)
            {
                text = text.Substring(0, windowWidth - 1); // Levágjuk, hogy beleférjen
            }

            // Ellenőrizni kell a paddinget is, hogy ne legyen negatív
            int padding = Math.Max(0, (windowWidth - text.Length) / 2); // Biztosítjuk, hogy a padding ne legyen negatív
            return new string(' ', padding) + text; // Szóközök hozzáadása középre igazításhoz
        }
    }
}
