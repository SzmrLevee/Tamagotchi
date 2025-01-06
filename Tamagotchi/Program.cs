using System;
using System.Drawing;
using System.Text;
using TamagotchiLib;
using TamagotchiLib.Animations;
using TamagotchiLib.Menu;

//string prompt = @"

//Köszöntelek a Tamagotchi szimulátorunkban!
//Használd a nyilakat a választáshoz és nyomd meg az entert!

//";

//string[] options =
//{
//    "Minijátékok",
//    "Bolt",
//    "Kisállat állapota megtekintése",
//    "Kilépés"
//};

//GameManager gameManager = new GameManager();
//Menu menu = new Menu(prompt, options);
//MainMenu mainMenu = new MainMenu(gameManager);
//ShopMenu shopMenu = new ShopMenu(gameManager);

//bool exit = false;
//while (!exit)
//{
//    int selectedOption = menu.Run();

//    switch (selectedOption)
//    {
//        case 0: // Minijátékok

//            mainMenu.Display();
//            break;
//        case 1: // Bolt

//            shopMenu.Display();
//            break;
//        case 2: // Kisállat állapota
//            Console.Clear();
//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.WriteLine("\n=== Kisállat Státusz ===\n");
//            Console.ResetColor();
//            gameManager.DisplayStatus();
//            Console.WriteLine("\nNyomj egy gombot a folytatáshoz.");
//            Console.ReadKey();
//            break;
//        case 3: // Kilépés
//            gameManager.SavePet();
//            exit = true;
//            break;
//    }
//}

// Code Page 437 használata
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.GetEncoding(437);

Console.WriteLine("Melyik fusson le?: ");
var fajl = Console.ReadLine();

string imagePath = @$"C:\Users\Levente\Desktop\Tamagotchi\TamagotchiLib\Animations\{fajl}.png";

int delayMilliseconds = 50;

using Bitmap spriteSheet = new Bitmap(imagePath);

// Képkockaszám automatikus kiszámítása
int frameHeight = spriteSheet.Height;
int numberOfFrames = spriteSheet.Width / frameHeight;
int frameWidth = spriteSheet.Width / numberOfFrames;

List<string[]> asciiFrames = new List<string[]>();

for (int i = 0; i < numberOfFrames; i++)
{
    Rectangle frameRect = new Rectangle(i * frameWidth, 0, frameWidth, frameHeight);
    using Bitmap frame = spriteSheet.Clone(frameRect, spriteSheet.PixelFormat);
    asciiFrames.Add(ConvertToAscii(frame, frameWidth * 2).Split('\n'));
}

ShowAnimation(asciiFrames, delayMilliseconds);

static void ShowAnimation(List<string[]> asciiFrames, int delayMilliseconds)
{
    while (!Console.KeyAvailable)
    {
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;

        int minWidth = 2;
        int minHeight = 2;

        Console.Clear();  // Törli az előző nyomokat

        foreach (var asciiFrame in asciiFrames)
        {
            int frameHeight = asciiFrame.Length;
            int frameWidth = asciiFrame.Max(line => line.Length);

            if (frameWidth > consoleWidth || frameHeight > consoleHeight)
            {
                Console.Clear();
                Console.WriteLine("A konzolméretet növelni kell, hogy a Tamagotchi elférjen.");
                break;
            }

            int top = Math.Clamp(consoleHeight / 2 - frameHeight / 2, 0, consoleHeight - 1);
            int left = Math.Clamp(consoleWidth / 2 - frameWidth / 2, 0, consoleWidth - 1);

            ClearCurrentFrame(consoleHeight, consoleWidth);  // Az egész képernyőt törli

            for (int y = 0; y < frameHeight; y++)
            {
                int line = top + y;
                if (line >= Console.WindowHeight || line < 0) continue;  // Ne írjon a konzol határain kívül

                Console.SetCursorPosition(left, line);
                string frameLine = asciiFrame[y].PadRight(frameWidth);
                Console.Write(frameLine.Substring(0, Math.Min(frameLine.Length, consoleWidth)));  // Levágja a sort, ha túl hosszú
            }

            Thread.Sleep(delayMilliseconds);
        }
    }
}

static string ConvertToAscii(Bitmap image, int maxWidth)
{
    StringBuilder asciiArt = new StringBuilder();
    double aspectRatio = (double)image.Height / image.Width;
    int newWidth = Math.Min(maxWidth, image.Width);
    int newHeight = (int)(newWidth * aspectRatio);

    using Bitmap resizedImage = new Bitmap(image, newWidth, newHeight);

    for (int y = 0; y < resizedImage.Height; y++)
    {
        for (int x = 0; x < resizedImage.Width; x++)
        {
            Color pixelColor = resizedImage.GetPixel(x, y);

            if (pixelColor.A == 0)
            {
                asciiArt.Append(" ");
            }
            else
            {
                int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                char asciiChar = grayScale > 200 ? '.' :
                                 grayScale > 150 ? '*' :
                                 grayScale > 100 ? '+' :
                                 grayScale > 50 ? '#' : '@';
                asciiArt.Append(asciiChar);
            }
        }
        asciiArt.AppendLine();
    }

    return asciiArt.ToString();
}

static void ClearCurrentFrame(int consoleHeight, int consoleWidth)
{
    for (int i = 0; i < consoleHeight; i++)
    {
        if (i >= Console.WindowHeight) break;  // Ha a sor túlmutat a konzolmagasságon, kilépünk
        Console.SetCursorPosition(0, i);
        Console.Write(new string(' ', consoleWidth));
    }
}
