using System;
using System.IO;
using TamagotchiLib.Models;

namespace TamagotchiLib.Utils
{
    public class RandomInterakciok
    {
        private string projectDirectory;

        public RandomInterakciok()
        {
            projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }

        // Véletlenszerű interakció a kisállattal
        public void RandomInteraction(Pet pet)
        {
            Random rand = new Random();
            int eventChoice = rand.Next(1, 6); // 1-5 között választunk egy eseményt

            switch (eventChoice)
            {
                case 1:
                    GenerateIllness(pet);
                    break;
                case 2:
                    GenerateInjury(pet);
                    break;
                case 3:
                    pet.Mood = Math.Min(100, pet.Mood + 10);
                    Console.WriteLine($"{pet.Name} jobb kedvre derült!");
                    break;
                case 4:
                    pet.Mood = Math.Max(0, pet.Mood - 15);
                    Console.WriteLine($"{pet.Name} nagyon szomorú.");
                    break;
                case 5:
                    Console.WriteLine($"{pet.Name} pihent, de unja magát.");
                    DisplaySittingImage();
                    break;
                default:
                    break;
            }
        }

        // Betegség generálása
        private void GenerateIllness(Pet pet)
        {
            Random rand = new Random();
            string[] illnesses = { "megfázás", "hasmenés", "fejfájás" };
            string illness = illnesses[rand.Next(illnesses.Length)];

            pet.Health = Math.Max(0, pet.Health - 20);
            pet.Mood = Math.Max(0, pet.Mood - 10);

            Console.WriteLine($"{pet.Name} megbetegedett és most {illness}-ban szenved.");
            DisplaySittingImage();
        }

        // Sérülés vagy zúzódás generálása
        private void GenerateInjury(Pet pet)
        {
            Random rand = new Random();
            string[] injuries = { "törés", "zúzódás", "horzsolás" };
            string injury = injuries[rand.Next(injuries.Length)];

            pet.Health = Math.Max(0, pet.Health - 15);
            pet.Mood = Math.Max(0, pet.Mood - 5);

            Console.WriteLine($"{pet.Name} megsérült és most {injury}-t szenved.");
            DisplaySittingImage();
        }

        // Az ülő animációs kép megjelenítése
        private void DisplaySittingImage()
        {
            string imagePath = GetAnimationsPath("Sitting");

            if (File.Exists(imagePath))
            {
                // Ezt lehetőség szerint grafikai könyvtárral jelenítsük meg a képet
                Console.WriteLine($"[Kép: {imagePath}]");  // A konzolon szöveges megjelenítés
            }
            else
            {
                Console.WriteLine("Nincs elérhető ülő animációs kép.");
            }
        }

        // Az Animations mappához vezető teljes út
        public string GetAnimationsPath(string fileName)
        {
            // Az Animations mappához vezető teljes út
            string animationsPath = Path.Combine(projectDirectory, "TamagotchiLib", "Animations", $"{fileName}.png");
            return animationsPath;
        }
    }
}
