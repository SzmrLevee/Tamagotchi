using System.Text;
using TamagotchiLib.Menu;
using TamagotchiLib.Accounts;
using TamagotchiLib.Models;
using TamagotchiLib.Jatekmenet;
using TamagotchiLib.Animations;
using TamagotchiLib.Utils;

//TODO: adatok visszatöltése, több macska megjelenítése

//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//Console.OutputEncoding = Encoding.GetEncoding(437);

//Console.WriteLine("Melyik animációt szeretnéd futtatni?:");
//string animationName = Console.ReadLine()!;

//AsciiAnimation.RunAnimation(animationName);



// Program elindítása, kezdő GameManager példányosítása
GameManager gameManager = new GameManager();

// `MainMenu` és `ShopMenu` példányosítása `GameManager` paraméterrel
MainMenu mainMenu = new MainMenu(gameManager);
ShopMenu shopMenu = new ShopMenu(gameManager);

// Időmúlás és interakciók inicializálása
IdoMulas idoMulas = new IdoMulas(gameManager.CurrentPet);
Interakciok interakciok = new Interakciok(gameManager.CurrentPet);

Mentes mentes = new Mentes();
gameManager.JatekMenu();
mentes.Fomenu();

// Menüpontok megjelenítése
static void DisplayMenu(int selectedOption, string[] options)
{
    GameManager gameManager = new GameManager();
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

        string centeredOption = GameManager.CenterText(options[i]); // Középre igazítjuk a menüpontot
        Console.WriteLine(centeredOption.PadRight(Console.WindowWidth)); // Kiíratjuk a középre illesztett menüpontot

        Console.ResetColor();  // Szín visszaállítása
    }
}

// Időmúlás leállítása a program kilépésekor
idoMulas.Stop();

MenuUtils.ResetPrompt();
MenuUtils.DisplayPromptOnce(gameManager.Prompt);