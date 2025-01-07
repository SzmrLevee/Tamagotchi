using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiLib.Menu
{
    public class MenuUtils
    {
        private static bool promptDisplayed = false;

        public static void DisplayPromptOnce(string prompt)
        {
            Console.Clear(); // Töröljük a konzolt
            foreach (var line in prompt.Split('\n'))
            {
                Console.WriteLine(CenterText(line)); // Középre igazított sorokat írunk ki
            }
        }

        private static string CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2;
            return new string(' ', padding) + text;
        }

        public static void ResetPrompt()
        {
            promptDisplayed = false; // Reseteli a prompt állapotát
        }
    }
}