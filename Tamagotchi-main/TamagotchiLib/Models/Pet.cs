using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TamagotchiLib.Models
{
    public class Pet
    {
        private const int MAX_HEALTH = 100;
        private const int MIN_VALUE = 0;

        public string Name { get; set; }
        public int Age { get; set; }
        public int Health { get; set; } = MAX_HEALTH;
        public int Hunger { get; set; } = 0;
        public int Tiredness { get; set; } = 0;
        public int Mood { get; set; } = MAX_HEALTH;
        public int Money { get; set; } = 0;
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Pet(string name)
        {
            Name = name;
        }

        public void Play()
        {
            if (Tiredness < MAX_HEALTH)
            {
                Mood = Math.Min(MAX_HEALTH, Mood + 20);
                Tiredness = Math.Min(MAX_HEALTH, Tiredness + 20);
                Console.WriteLine($"{Name} jól érezte magát a játék során!");
            }
            else
            {
                Console.WriteLine($"{Name} túl fáradt a játékhoz.");
            }
        }

        public void Sleep()
        {
            Tiredness = Math.Max(MIN_VALUE, Tiredness - 40);
            Health = Math.Min(MAX_HEALTH, Health + 10);
            Console.WriteLine($"{Name} aludt és kipihentebb lett.");
        }

        public override string ToString()
        {
            return $"Név: {Name}, Kor: {Age}, Élet: {Health}/100, Éhség: {Hunger}/100, Fáradtság: {Tiredness}/100, Hangulat: {Mood}/100, Pénz: {Money}";
        }
    }
}
