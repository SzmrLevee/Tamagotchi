using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiLib.Models;

namespace TamagotchiLib.MiniGames
{
    public class MathChallengeGame
    {
        private readonly Random random = new Random();

        public void Play(Pet pet)
        {
            int num1 = random.Next(10, 100);
            int num2 = random.Next(1, 10);
            string[] operations = { "+", "-", "*", "/" };
            string operation = operations[random.Next(operations.Length)];

            double correctAnswer = operation switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => Math.Round((double)num1 / num2, 2),
                _ => 0
            };

            Console.WriteLine($"Mennyi {num1} {operation} {num2}?");
            string userAnswer = Console.ReadLine();

            if (double.TryParse(userAnswer, out double userNumericAnswer) && Math.Abs(userNumericAnswer - correctAnswer) < 0.01)
            {
                Console.WriteLine("Helyes válasz!");
                pet.Mood = Math.Min(100, pet.Mood + 15);
                pet.Money += 30;
            }
            else
            {
                Console.WriteLine("Helytelen válasz.");
                Console.WriteLine($"A helyes válasz: {correctAnswer}");
                pet.Mood = Math.Min(100, pet.Mood + 5);
            }
        }
    }
}
