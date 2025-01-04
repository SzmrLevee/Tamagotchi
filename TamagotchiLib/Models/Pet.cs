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
        public int Health { get; private set; } = MAX_HEALTH;
        public int Hunger { get; private set; } = 0;
        public int Tiredness { get; private set; } = 0;
        public int Mood { get; set; } = MAX_HEALTH;
        public int Money { get; set; } = 0;

        [JsonIgnore]
        public bool IsAlive => Health > 0;

        [JsonConstructor]
        public Pet(string name, int age = 0, int health = MAX_HEALTH, int hunger = 0, int tiredness = 0, int mood = MAX_HEALTH, int money = 0)
        {
            Name = name;
            Age = age;
            Health = health;
            Hunger = hunger;
            Tiredness = tiredness;
            Mood = mood;
            Money = money;
        }

        public void Eat()
        {
            if (Hunger > 0)
            {
                Hunger = Math.Max(MIN_VALUE, Hunger - 30);
                Mood = Math.Min(MAX_HEALTH, Mood + 10);
                Console.WriteLine($"{Name} evett és kevésbé éhes.");
            }
            else
            {
                Console.WriteLine($"{Name} nem éhes.");
            }
        }

        public void Sleep()
        {
            Tiredness = Math.Max(MIN_VALUE, Tiredness - 40);
            Health = Math.Min(MAX_HEALTH, Health + 10);
            Console.WriteLine($"{Name} aludt és kipihentebb.");
        }

        public void Play()
        {
            if (Tiredness < MAX_HEALTH)
            {
                Mood = Math.Min(MAX_HEALTH, Mood + 20);
                Tiredness = Math.Min(MAX_HEALTH, Tiredness + 20);
                Hunger = Math.Min(MAX_HEALTH, Hunger + 10);
                Console.WriteLine($"{Name} jól érezte magát a játék során!");
            }
            else
            {
                Console.WriteLine($"{Name} túl fáradt a játékhoz.");
            }
        }

        public void AgeUp()
        {
            Age++;
            Hunger = Math.Min(MAX_HEALTH, Hunger + 5);
            Tiredness = Math.Min(MAX_HEALTH, Tiredness + 5);

            if (Hunger >= MAX_HEALTH || Tiredness >= MAX_HEALTH)
            {
                Health = Math.Max(MIN_VALUE, Health - 20);
                Console.WriteLine($"{Name} egészségi állapota romlik az éhség vagy fáradtság miatt.");
            }

            if (!IsAlive)
            {
                Console.WriteLine($"{Name} sajnos elpusztult.");
            }
        }

        public override string ToString()
        {
            if (!IsAlive) return $"{Name} meghalt";
            return $"Név: {Name}\nKor: {Age}\nÉlet: {Health}/100\nÉhség: {Hunger}/100\nFáradtság: {Tiredness}/100\nHangulat: {Mood}/100\nPénz: {Money}";
        }
    }
}
