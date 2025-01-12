using TamagotchiLib.Accounts;
using TamagotchiLib.Jatekmenet;
using TamagotchiLib.Menu;
using TamagotchiLib.MiniGames;
using TamagotchiLib.Models;

namespace TamagotchiLib.Utils
{
    public class GameManager
    {
        public GameManager()
        {
            randomInterakciok = new RandomInterakciok();
        }

        public string Prompt = @"

 ________         __  __            _____   ____  _______  _____  _    _  _____ 
    | |    /\    |  \/  |    /\    / ____| / __ \|__   __|/ ____|| |  | ||_   _|
    | |   /  \   | \  / |   /  \  | |  __ | |  | |  | |  | |     | |__| |  | |  
    | |  / /\ \  | |\/| |  / /\ \ | | |_ || |  | |  | |  | |     |  __  |  | |  
    | | / ____ \ | |  | | / ____ \| |__| || |__| |  | |  | |____ | |  | | _| |_ 
    |_|/_/    \_\|_|  |_|/_/    \_\\_____| \____/   |_|   \_____||_|  |_||_____|
        ";

        public int PlayerMoney { get; set; } = 500; // Kezdeti pénzösszeg

        public void JatekMenu()
        {
            Console.Clear();
            DisplayPromptOnce(Prompt);

            bool exit = false;
            string[] options = { "Játék", "Bolt", "Inventory", "Kisállat állapota", "Fiókkezelés", "Kilépés" };
            int selectedOption = 0;

            while (!exit)
            {
                DisplayMenu(selectedOption, options);

                // Felhasználói bemenet kezelése
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow) // Nyíl felfelé
                {
                    selectedOption = (selectedOption == 0) ? options.Length - 1 : selectedOption - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow) // Nyíl lefelé
                {
                    selectedOption = (selectedOption == options.Length - 1) ? 0 : selectedOption + 1;
                }
                else if (key.Key == ConsoleKey.Enter) // Enter gomb megnyomása
                {
                    // Ellenőrizzük, hogy be van-e jelentkezve és van-e kisállat betöltve
                    if (string.IsNullOrEmpty(Mentes.valasztottFiokNev) || CurrentPet == null && selectedOption != 4 && selectedOption != 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Nem vagy bejelentkezve, vagy nincs kisállat betöltve. Átirányítás a fiókkezeléshez...");
                        Console.ReadKey();
                        Mentes mentes = new Mentes();
                        mentes.Fomenu();  // Átirányítás a fiókkezelés menübe
                        LoadPet();  // Újra betölti a kisállatot, ha sikeres bejelentkezés történt
                        continue;  // Újra betölti a JatekMenu-t
                    }

                    switch (selectedOption)
                    {
                        case 0:  // "Játék"
                            Console.Clear();
                            if (CurrentPet == null)
                            {
                                Console.WriteLine("Hiba: Nincs kisállat betöltve! Bejelentkezés szükséges.");
                                Console.ReadKey();
                                break;
                            }
                            new JatekmenetView(CurrentPet).MainFuttatasa();
                            break;
                        case 1:  // "Bolt"
                            Console.Clear();
                            new ShopMenu(this).Display();
                            break;
                        case 2:  // "Inventory"
                            Console.Clear();
                            DisplayInventory(); // Inventory megjelenítés metódus
                            Console.Clear();
                            break;
                        case 3:  // "Kisállat állapota"
                            Console.Clear();
                            DisplayStatus();
                            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 4:  // "Fiókkezelés"
                            Console.Clear();
                            Mentes mentes = new Mentes();
                            mentes.Fomenu();
                            break;
                        case 5:  // "Kilépés"
                            exit = true;
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        // Középre igazító segédfüggvény
        public static string CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2; // Kiszámoljuk a szükséges szóközöket
            return new string(' ', Math.Max(padding, 0)) + text; // A szükséges szóközökkel hozzáfűzzük a szöveget
        }

        // Segédfüggvény a menuPrompt egyszeri megjelenítéséhez
        void DisplayPromptOnce(string prompt)
        {
            Console.Clear(); // Konzol törlése az induláskor
            foreach (var line in prompt.Split('\n'))
            {
                Console.WriteLine(CenterText(line)); // Középre igazított menuPrompt kiírása
            }
        }

        public void BuyItem(string itemName, int itemPrice)
        {
            if (PlayerMoney >= itemPrice)
            {
                PlayerMoney -= itemPrice;

                // Inventory frissítése fájlban
                Mentes mentes = new Mentes();
                mentes.UpdateInventory(Mentes.valasztottFiokNev, itemName, 1);

                Console.WriteLine($"Vásároltál egy {itemName}-t! Kiadott pénz: {itemPrice} Pénz, Maradó: {PlayerMoney}");
            }
            else
            {
                Console.WriteLine("Nincs elég pénzed a vásárláshoz!");
            }

            Console.WriteLine("Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
            Console.Clear();
        }

        public Pet CurrentPet { get; private set; }
        private readonly FileManager fileManager = new FileManager("Data/pet_data.json");
        private readonly RandomInterakciok randomInterakciok;


        public void LoadPet()
        {
            if (string.IsNullOrEmpty(Mentes.valasztottFiokNev))
            {
                Console.WriteLine("Nem érhető el fiókhoz tartozó kisállat. Alapértelmezett állat betöltése...");
                CurrentPet = new Pet("Alapértelmezett Kisállat");
            }
            else
            {
                Console.WriteLine($"Kisállat betöltése a(z) {Mentes.valasztottFiokNev} fiókból...");
                CurrentPet = new Pet(Mentes.valasztottFiokNev);  // Ezt módosíthatod, hogy fiókadatokból töltsön
            }
        }



        public void SavePet()
        {
            fileManager.SaveData(CurrentPet);
        }

        public void PlayMinigame(int gameChoice)
        {
            switch (gameChoice)
            {
                case 1:
                    new ColorGuessGame().Play(CurrentPet);
                    break;
                case 2:
                    new MathChallengeGame().Play(CurrentPet);
                    break;
                default:
                    Console.WriteLine("Érvénytelen játékválasztás.");
                    break;
            }
            SavePet();
        }

        static void DisplayMenu(int selectedOption, string[] options)
        {
            int verticalPaddingMenu = (Console.WindowHeight - options.Length) / 2;

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(0, verticalPaddingMenu + i);

                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Kiemelt menüpont színe
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White; // A többi menüpont normál színnel
                }

                string centeredOption = CenterText(options[i]); // Középre igazítjuk a menüpontot
                Console.WriteLine(centeredOption.PadRight(Console.WindowWidth)); // Kiíratjuk a középre illesztett menüpontot

                Console.ResetColor();  // Szín visszaállítása
            }
        }

        //static void DisplayPetStatus(GameManager gameManager)
        //{
        //    Console.Clear();
        //    Console.ForegroundColor = ConsoleColor.Blue;
        //    Console.WriteLine("\n=== Kisállat Státusz ===\n");
        //    Console.ResetColor();
        //    gameManager.DisplayStatus();
        //    Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
        //    Console.ReadKey();
        //}

        public void DisplayStatus()
        {
            Console.WriteLine(CurrentPet.ToString());
        }

        public void RandomEvent()
        {
            randomInterakciok.RandomInteraction(CurrentPet);  // Véletlenszerű interakció kiváltása
        }

        public void DisplayInventory()
        {
            Console.Clear();
            Console.WriteLine("=== Inventory ===\n");

            Mentes mentes = new Mentes();
            string[] inventoryItems = mentes.GetInventory(Mentes.valasztottFiokNev);

            if (inventoryItems == null || inventoryItems.Length == 0)
            {
                Console.WriteLine("Az inventory üres.");
            }
            else
            {
                foreach (var item in inventoryItems)
                {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        
    }
}
