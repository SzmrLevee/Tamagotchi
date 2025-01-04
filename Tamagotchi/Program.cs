using TamagotchiLib;
using TamagotchiLib.Menu;

string prompt = @"

Köszöntelek a Tamagotchi szimulátorunkban!
Használd a nyilakat a választáshoz és nyomd meg az entert!

";

string[] options =
{
    "Minijátékok",
    "Bolt",
    "Kisállat állapota megtekintése",
    "Kilépés"
};

GameManager gameManager = new GameManager();
Menu menu = new Menu(prompt, options);
MainMenu mainMenu = new MainMenu(gameManager);
ShopMenu shopMenu = new ShopMenu(gameManager);

bool exit = false;
while (!exit)
{
    int selectedOption = menu.Run();

    switch (selectedOption)
    {
        case 0: // Minijátékok
            
            mainMenu.Display();
            break;
        case 1: // Bolt
            
            shopMenu.Display();
            break;
        case 2: // Kisállat állapota
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n=== Kisállat Státusz ===\n");
            Console.ResetColor();
            gameManager.DisplayStatus();
            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
            break;
        case 3: // Kilépés
            gameManager.SavePet();
            exit = true;
            break;
    }
}