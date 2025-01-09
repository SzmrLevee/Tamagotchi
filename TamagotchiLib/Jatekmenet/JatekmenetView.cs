using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TamagotchiLib.Accounts;
using TamagotchiLib.Animations;
using TamagotchiLib.Menu;
using TamagotchiLib.Models;
using TamagotchiLib.Utils;

namespace TamagotchiLib.Jatekmenet
{
    public class JatekmenetView
    {
        private readonly Pet pet;
        private bool isRunning = true;
        private readonly string[] menuOptions = { "Játszik", "Alszik", "Inventory", "Vissza" };

        public JatekmenetView(Pet pet)
        {
            this.pet = pet;
            if (pet == null)
            {
                throw new ArgumentNullException(nameof(pet), "A kisállat példány nem lehet null!");
            }
        }
        public void MainFuttatasa()
        {
            Console.Clear();

            // Indítsuk az animációt külön szálon.

            if (pet == null)
            {
                Console.WriteLine("A kisállat példánya nincs inicializálva!");
                return;
            }


            Thread animationThread = new Thread(() => RunAnimationLoop());
            if (animationThread == null)
            {
                Console.WriteLine("Nem sikerült elindítani az animációs szálat.");
            }

            animationThread.Start();

            while (isRunning)
            {
                if (!IsInGameView())
                {
                    ClearStatusArea();
                    ClearMenuArea();
                    DisplayStatus();
                    DisplayMenu();
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    HandleMenuSelection(key);
                }

                Thread.Sleep(100);
            }

            animationThread.Join();
        }

        private bool IsInGameView()
        {
            return Console.Title == "Fiókkezelés";
        }

        private void RunAnimationLoop()
        {
            string[] animations = { "Idle", "Walk" };
            Random random = new Random();

            while (isRunning)
            {
                string currentAnimation = animations[random.Next(animations.Length)];

                if (pet.Tiredness > 80)
                    currentAnimation = "Licking";
                else if (pet.Mood < 40)
                    currentAnimation = "Sitting";

                Console.SetCursorPosition(0, 3);
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 3);
                AsciiAnimation.RunAnimation(currentAnimation, 50);

                Thread.Sleep(2000);
            }
        }

        private void ClearStatusArea()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
        }

        private void DisplayStatus()
        {
            if (pet == null)
            {
                Console.WriteLine("A kisállat nincs betöltve.");
                return;
            }

            if (IsInGameView()) return; // Ne írja ki az állapotot fiókkezelésnél
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(pet.ToString().PadRight(Console.WindowWidth));
            Console.ResetColor();
        }

        private void ClearMenuArea()
        {
            int menuStartLine = Console.WindowHeight - menuOptions.Length - 4;
            Console.SetCursorPosition(0, menuStartLine);
            for (int i = 0; i < menuOptions.Length + 5; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, menuStartLine);
        }

        private void DisplayMenu()
        {
            if (IsInGameView()) return; // Ne írja ki a menüt fiókkezelésnél
            int menuStartLine = Console.WindowHeight - menuOptions.Length - 3;
            Console.SetCursorPosition(0, menuStartLine);
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine("--- Választható opciók ---");

            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.SetCursorPosition(0, menuStartLine + i + 2);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, menuStartLine + i + 2);
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Használd a számbillentyűket a választáshoz. Nyomd meg az ESC-t a főmenübe való visszatéréshez.".PadRight(Console.WindowWidth));
        }

        private void HandleMenuSelection(ConsoleKey key)
        {
            GameManager gameManager = new GameManager();
            switch (key)
            {
                case ConsoleKey.D1:
                    if (gameManager.CurrentPet == null)
                    {
                        Console.WriteLine("Nincs kisállat betöltve. Kérlek, tölts be egy kisállatot.");
                    }
                    else
                    {
                        new JatekmenetView(gameManager.CurrentPet).MainFuttatasa();
                    }
                    break;

                case ConsoleKey.D2:
                    ClearStatusArea();
                    ClearMenuArea();
                    DisplayStatus();
                    Console.SetCursorPosition(0, 3);
                    AsciiAnimation.RunAnimation("Laying", 50);
                    AsciiAnimation.RunAnimation("Sleeping", 100);
                    pet.Sleep();
                    break;
                case ConsoleKey.D3:
                    InventoryView inventoryView = new InventoryView(pet);
                    inventoryView.ShowInventory();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.Escape:
                    isRunning = false;
                    Console.Clear();
                    Console.WriteLine("Visszatérés a főmenübe...");
                    break;
                default:
                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                    Console.WriteLine("Érvénytelen opció. Próbáld újra.".PadRight(Console.WindowWidth));
                    break;
            }
        }
    }
}
