using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.Models;

namespace TamagotchiLib.MiniGames
{
    public class ColorGuessGame
    {
        private readonly Random random = new Random();

        public void Play(Pet pet)
        {
            Console.WriteLine("Találd ki, hogy piros vagy kék lesz a szín! Írd be: 'piros' vagy 'kék'.");
            string userChoice = Console.ReadLine()?.ToLower();

            string[] colors = { "piros", "kék" };
            string randomColor = colors[random.Next(colors.Length)];

            Console.WriteLine($"A választott szín: {randomColor}");

            if (userChoice == randomColor)
            {
                Console.WriteLine("Gratulálok! Eltaláltad.");
                pet.Mood = Math.Min(100, pet.Mood + 10);
                pet.Money += 20;
            }
            else
            {
                Console.WriteLine("Nem találtad el. Próbáld újra!");
                pet.Mood = Math.Min(100, pet.Mood + 5);
            }
        }
    }
}
