using System;
using TamagotchiLib.Models;

namespace TamagotchiLib.Utils
{
    public class Interakciok
    {
        private readonly Pet _pet;

        public Interakciok(Pet pet)
        {
            _pet = pet;
        }

        public void Etet()
        {
            if (_pet.Hunger > 0)
            {
                _pet.Hunger = Math.Max(0, _pet.Hunger - 20);
                _pet.Mood = Math.Min(100, _pet.Mood + 10);
                Console.WriteLine($"{_pet.Name} evett és kevésbé éhes.");
            }
            else
            {
                Console.WriteLine($"{_pet.Name} nem éhes most.");
            }
        }

        public void Simogat()
        {
            _pet.Mood = Math.Min(100, _pet.Mood + 15);
            Console.WriteLine($"{_pet.Name} élvezte a simogatást, és boldogabb lett!");
        }

        public void Setal()
        {
            if (_pet.Tiredness < 80)
            {
                _pet.Tiredness = Math.Min(100, _pet.Tiredness + 15);
                _pet.Mood = Math.Min(100, _pet.Mood + 10);
                Console.WriteLine($"{_pet.Name} jókedvűen sétált, de kicsit elfáradt.");
            }
            else
            {
                Console.WriteLine($"{_pet.Name} túl fáradt a sétához.");
            }
        }

        public void Jatek()
        {
            if (_pet.Tiredness < 90 && _pet.Hunger < 80)
            {
                _pet.Mood = Math.Min(100, _pet.Mood + 20);
                _pet.Tiredness = Math.Min(100, _pet.Tiredness + 20);
                _pet.Hunger = Math.Min(100, _pet.Hunger + 10);
                Console.WriteLine($"{_pet.Name} játszott, és remekül érezte magát!");
            }
            else
            {
                Console.WriteLine($"{_pet.Name} túl fáradt vagy éhes a játékhoz.");
            }
        }

        public void Gyogyitas()
        {
            if (_pet.Health < 100)
            {
                _pet.Health = Math.Min(100, _pet.Health + 30);
                Console.WriteLine($"{_pet.Name} meggyógyult, és jobban érzi magát.");
            }
            else
            {
                Console.WriteLine($"{_pet.Name} teljesen egészséges, nincs szükség kezelésre.");
            }
        }
    }
}
