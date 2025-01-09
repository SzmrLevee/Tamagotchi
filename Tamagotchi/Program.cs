using System.Text;
using TamagotchiLib.Menu;
using TamagotchiLib.Accounts;
using TamagotchiLib.Models;
using TamagotchiLib.Jatekmenet;
using TamagotchiLib.Animations;
using TamagotchiLib.Utils;

//TODO: adatok visszatöltése, több macska megjelenítése

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.GetEncoding(437);

Console.WriteLine("Melyik animációt szeretnéd futtatni?:");
string animationName = Console.ReadLine()!;

AsciiAnimation.RunAnimation(animationName);


//// Program elindítása, kezdő GameManager példányosítása
//GameManager gameManager = new GameManager();
//gameManager.LoadPet();

//// `MainMenu` és `ShopMenu` példányosítása `GameManager` paraméterrel
//MainMenu mainMenu = new MainMenu(gameManager);
//ShopMenu shopMenu = new ShopMenu(gameManager);

//// Időmúlás és interakciók inicializálása
//IdoMulas idoMulas = new IdoMulas(gameManager.CurrentPet);
//Interakciok interakciok = new Interakciok(gameManager.CurrentPet);

//DisplayPromptOnce(gameManager.Prompt);

//// Segédfüggvény a menuPrompt egyszeri megjelenítéséhez
//static void DisplayPromptOnce(string prompt)
//{
//    Console.Clear(); // Konzol törlése az induláskor
//    foreach (var line in prompt.Split('\n'))
//    {
//        Console.WriteLine(CenterText(line)); // Középre igazított menuPrompt kiírása
//    }
//}

//bool exit = false;
//int selectedOption = 0;
//string[] options = { "Játék", "Bolt", "Kisállat állapota", "Interakciók", "Kilépés" };

//while (!exit)
//{
//    // Megjelenítjük a menüt
//    DisplayMenu(selectedOption, options);

//    // Felhasználói bemenet kezelése
//    var key = Console.ReadKey(true);

//    if (key.Key == ConsoleKey.UpArrow) // Nyíl felfelé
//    {
//        selectedOption = (selectedOption == 0) ? options.Length - 1 : selectedOption - 1;
//    }
//    else if (key.Key == ConsoleKey.DownArrow) // Nyíl lefelé
//    {
//        selectedOption = (selectedOption == options.Length - 1) ? 0 : selectedOption + 1;
//    }
//    else if (key.Key == ConsoleKey.Enter) // Enter gomb megnyomása
//    {
//        switch (selectedOption)
//        {
//            case 0:  // "Játék"
//                JatekmenetView gameView = new JatekmenetView(gameManager.CurrentPet);
//                gameView.MainFuttatasa();  // `Show` helyett `MainFuttatasa()`
//                break;
//            case 1:  // "Bolt"
//                ClearMenuArea();
//                shopMenu.Display();  // Bolt megjelenítése
//                break;
//            case 2:  // "Kisállat állapota"
//                ClearMenuArea();
//                DisplayPetStatus(gameManager);
//                break;
//            case 3:  // "Interakciók"
//                DisplayInteractions(interakciok);
//                break;
//            case 4:  // "Kilépés"
//                exit = true;
//                break;
//        }
//    }
//}

//// Menüpont terület törlése
//static void ClearMenuArea()
//{
//    Console.Clear(); // Eltávolítja a menü tartalmát, menuPrompt nem jelenik meg újra
//}

//// Kisállat státuszának megjelenítése
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

//// Középre igazító segédfüggvény
//static string CenterText(string text)
//{
//    int windowWidth = Console.WindowWidth;
//    int padding = (windowWidth - text.Length) / 2; // Kiszámoljuk a szükséges szóközöket
//    return new string(' ', Math.Max(padding, 0)) + text; // A szükséges szóközökkel hozzáfűzzük a szöveget
//}

//// Menüpontok megjelenítése
//static void DisplayMenu(int selectedOption, string[] options)
//{
//    int verticalPaddingMenu = (Console.WindowHeight - options.Length) / 2;

//    for (int i = 0; i < options.Length; i++)
//    {
//        Console.SetCursorPosition(0, verticalPaddingMenu + i);

//        if (i == selectedOption)
//        {
//            Console.ForegroundColor = ConsoleColor.Red; // Kiemelt menüpont színe
//        }
//        else
//        {
//            Console.ForegroundColor = ConsoleColor.White; // A többi menüpont normál színnel
//        }

//        string centeredOption = CenterText(options[i]); // Középre igazítjuk a menüpontot
//        Console.WriteLine(centeredOption.PadRight(Console.WindowWidth)); // Kiíratjuk a középre illesztett menüpontot

//        Console.ResetColor();  // Szín visszaállítása
//    }
//}

//// Időmúlás leállítása a program kilépésekor
//idoMulas.Stop();

//MenuUtils.ResetPrompt();
//MenuUtils.DisplayPromptOnce(gameManager.Prompt);

//// Interakciók menü megjelenítése
//static void DisplayInteractions(Interakciok interakciok)
//{
//    Console.Clear();
//    string[] interactionOptions = { "Etetés", "Simogatás", "Séta", "Játék", "Gyógyítás", "Vissza" };
//    int selectedInteraction = 0;

//    while (true)
//    {
//        Console.Clear();
//        DisplayMenu(selectedInteraction, interactionOptions);

//        var key = Console.ReadKey(true);
//        if (key.Key == ConsoleKey.UpArrow)
//            selectedInteraction = (selectedInteraction == 0) ? interactionOptions.Length - 1 : selectedInteraction - 1;
//        else if (key.Key == ConsoleKey.DownArrow)
//            selectedInteraction = (selectedInteraction == interactionOptions.Length - 1) ? 0 : selectedInteraction + 1;
//        else if (key.Key == ConsoleKey.Enter)
//        {
//            if (selectedInteraction == interactionOptions.Length - 1)
//                break;

//            switch (selectedInteraction)
//            {
//                case 0:
//                    interakciok.Etet();
//                    break;
//                case 1:
//                    interakciok.Simogat();
//                    break;
//                case 2:
//                    interakciok.Setal();
//                    break;
//                case 3:
//                    interakciok.Jatek();
//                    break;
//                case 4:
//                    interakciok.Gyogyitas();
//                    break;
//            }

//            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
//            Console.ReadKey();
//        }
//    }
//}