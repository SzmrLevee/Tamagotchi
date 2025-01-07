using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiLib.Animations
{
    public class AsciiAnimation
    {
        public static void RunAnimation(string animationName, int delayMilliseconds = 50)
        {
            string imagePath = @$"C:\Users\Levente\Desktop\Tamagotchi\TamagotchiLib\Animations\{animationName}.png";
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Hiba: Nem található a(z) {imagePath} fájl.");
                return;
            }

            using Bitmap spriteSheet = new Bitmap(imagePath);
            int frameHeight = spriteSheet.Height;
            int numberOfFrames = spriteSheet.Width / frameHeight;  // Képkockaszám automatikus kiszámítása
            int frameWidth = spriteSheet.Width / numberOfFrames;

            List<string[]> asciiFrames = new List<string[]>();
            for (int i = 0; i < numberOfFrames; i++)
            {
                Rectangle frameRect = new Rectangle(i * frameWidth, 0, frameWidth, frameHeight);
                using Bitmap frame = spriteSheet.Clone(frameRect, spriteSheet.PixelFormat);
                asciiFrames.Add(ConvertToAscii(frame, frameWidth * 2).Split('\n'));
            }

            ShowAnimation(asciiFrames, delayMilliseconds);
        }

        private static void ShowAnimation(List<string[]> asciiFrames, int delayMilliseconds)
        {
            while (!Console.KeyAvailable)
            {
                int consoleWidth = Console.WindowWidth;
                int consoleHeight = Console.WindowHeight;

                foreach (var asciiFrame in asciiFrames)
                {
                    int frameHeight = asciiFrame.Length;
                    int frameWidth = asciiFrame.Max(line => line.Length);

                    // Ellenőrizzük, hogy a konzol elég nagy-e az animációhoz
                    if (frameWidth > consoleWidth || frameHeight > consoleHeight)
                    {
                        Console.Clear();
                        Console.WriteLine("A konzolméretet növelni kell, hogy a Tamagotchi elférjen.");
                        continue;
                    }

                    int top = Math.Clamp(consoleHeight / 2 - frameHeight / 2, 0, consoleHeight - 1);
                    int left = Math.Clamp(consoleWidth / 2 - frameWidth / 2, 0, consoleWidth - 1);

                    ClearCurrentFrame(consoleHeight, consoleWidth);  // Az előző képkockák törlése

                    for (int y = 0; y < frameHeight; y++)
                    {
                        int line = top + y;
                        if (line >= Console.WindowHeight || line < 0) continue;

                        Console.SetCursorPosition(left, line);
                        string frameLine = asciiFrame[y].PadRight(frameWidth);
                        Console.Write(frameLine.Substring(0, Math.Min(frameLine.Length, consoleWidth)));
                    }

                    Thread.Sleep(delayMilliseconds);
                }
            }
        }

        private static string ConvertToAscii(Bitmap image, int maxWidth)
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

                    if (pixelColor.A == 0)  // Átlátszó háttér kezelése
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

        private static void ClearCurrentFrame(int consoleHeight, int consoleWidth)
        {
            for (int i = 0; i < consoleHeight; i++)
            {
                if (i >= Console.WindowHeight) break;
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', consoleWidth));
            }
        }
    }
}
