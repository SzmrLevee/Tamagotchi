using System.Text;
using TamagotchiLib.Menu;
using TamagotchiLib.Accounts;
using TamagotchiLib.Models;
using TamagotchiLib.Jatekmenet;
using TamagotchiLib.Animations;
using TamagotchiLib;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.GetEncoding(437);

// Program elindítása, kezdő GameManager példányosítása
GameManager gameManager = new GameManager();
gameManager.LoadPet();

bool exit = false;
int selectedOption = 0;
string[] options = { "Játék", "Bolt", "Kisállat állapota", "Kilépés" };

while (!exit)
{
    Console.Clear();
    DisplayMenu("=== Tamagotchi Főmenü ===", options, selectedOption);

    var key = Console.ReadKey(true);
    if (key.Key == ConsoleKey.UpArrow)
        selectedOption = (selectedOption == 0) ? options.Length - 1 : selectedOption - 1;
    else if (key.Key == ConsoleKey.DownArrow)
        selectedOption = (selectedOption == options.Length - 1) ? 0 : selectedOption + 1;
    else if (key.Key == ConsoleKey.Enter)
    {
        switch (selectedOption)
        {
            case 0:  // "Játék"
                if (string.IsNullOrEmpty(Mentes.valasztottFiokNev))
                {
                    Mentes mentes = new Mentes();
                    mentes.Fomenu();  // Ha nincs bejelentkezve, megjelenik a Fiókkezelés
                }
                else
                {
                    new JatekmenetView(gameManager.CurrentPet).MainFuttatasa();  // A Játékmenet megjelenítése
                }
                break;
            case 1:  // "Bolt"
                Console.Clear();
                Console.WriteLine("\n=== Bolt Funkció ===\n");
                Console.WriteLine("A bolt megjelenítése folyamatban...");
                Console.WriteLine("Nyomj meg egy gombot a visszalépéshez.");
                Console.ReadKey();
                break;
            case 2:  // "Kisállat állapota"
                Console.Clear();
                DisplayPetStatus(gameManager);
                break;
            case 3:  // "Kilépés"
                gameManager.SavePet();  // Állat adatainak mentése kilépéskor
                exit = true;
                break;
        }
    }
}

MenuUtils.ResetPrompt();
MenuUtils.DisplayPromptOnce(gameManager.Prompt);

// Kisállat státuszának megjelenítése
static void DisplayPetStatus(GameManager gameManager)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\n=== Kisállat Állapota ===\n");
    Console.ResetColor();
    Console.WriteLine(gameManager.CurrentPet.ToString());
    Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
    Console.ReadKey();
}

// Menük megjelenítése középre igazítva
static void DisplayMenu(string title, string[] options, int selectedOption)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(CenterText(title));
    Console.ResetColor();

    int verticalPadding = (Console.WindowHeight - options.Length) / 2;

    for (int i = 0; i < verticalPadding; i++)
        Console.WriteLine();  // Függőleges margó

    for (int i = 0; i < options.Length; i++)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        string formattedOption = (i == selectedOption) ? $"> {options[i]} <" : $"  {options[i]}  ";
        Console.ForegroundColor = (i == selectedOption) ? ConsoleColor.Red : ConsoleColor.White;
        Console.WriteLine(CenterText(formattedOption));
    }
    Console.ResetColor();
}

// Szöveg középre igazítása
static string CenterText(string text)
{
    int windowWidth = Console.WindowWidth;
    int padding = (windowWidth - text.Length) / 2;
    return new string(' ', Math.Max(padding, 0)) + text;
}

//string menuPrompt = @"


//  _______         __  __            _____   ____  _______  _____  _    _  _____ 
// |__   __| /\    |  \/  |    /\    / ____| / __ \|__   __|/ ____|| |  | ||_   _|
//    | |   /  \   | \  / |   /  \  | |  __ | |  | |  | |  | |     | |__| |  | |  
//    | |  / /\ \  | |\/| |  / /\ \ | | |_ || |  | |  | |  | |     |  __  |  | |  
//    | | / ____ \ | |  | | / ____ \| |__| || |__| |  | |  | |____ | |  | | _| |_ 
//    |_|/_/    \_\|_|  |_|/_/    \_\\_____| \____/   |_|   \_____||_|  |_||_____|


//";

//// Kiírjuk a promptot egyszer
//DisplayPromptOnce(menuPrompt);

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
//            case 0: // Játék mód
//                JatekmenetView gameView = new JatekmenetView(gameManager.CurrentPet);
//                gameView.Show();
//                break;
//            case 1: // Minijátékok
//                ClearMenuArea();
//                mainMenu.Display();
//                break;
//            case 2: // Bolt
//                ClearMenuArea();
//                shopMenu.Display();
//                break;
//            case 3: // Kisállat állapota
//                ClearMenuArea();
//                DisplayPetStatus(gameManager);
//                break;
//            case 4: // Kilépés
//                gameManager.SavePet();
//                exit = true;
//                break;
//        }
//    }
//}

//// Segédfüggvény a menuPrompt egyszeri megjelenítéséhez
//static void DisplayPromptOnce(string prompt)
//{
//    Console.Clear(); // Konzol törlése az induláskor
//    foreach (var line in prompt.Split('\n'))
//    {
//        Console.WriteLine(CenterText(line)); // Középre igazított menuPrompt kiírása
//    }
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



//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

//Console.WriteLine("Melyik animációt szeretnéd futtatni?:");
//string animationName = Console.ReadLine() ?? "Idle";

//AsciiAnimation.RunAnimation(animationName);
