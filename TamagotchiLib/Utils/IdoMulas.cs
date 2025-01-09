using System;
using System.Timers;
using TamagotchiLib.Models;

namespace TamagotchiLib.Utils
{
    public class IdoMulas
    {
        private readonly System.Timers.Timer _timer;
        private readonly Pet _pet;

        public IdoMulas(Pet pet, int intervalMilliseconds = 10000)
        {
            _pet = pet;

            // Időzítő beállítása
            _timer = new System.Timers.Timer(intervalMilliseconds);
            _timer.Elapsed += (sender, args) => FrissitTulajdonsagok();
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void FrissitTulajdonsagok()
        {
            if (_pet == null)
                return;

            // Éhség növekedése
            _pet.Hunger = Math.Min(100, _pet.Hunger + 5);

            // Fáradtság növekedése
            _pet.Tiredness = Math.Min(100, _pet.Tiredness + 3);

            // Hangulat csökkenése, ha az éhség vagy fáradtság magas
            if (_pet.Hunger > 70 || _pet.Tiredness > 70)
            {
                _pet.Mood = Math.Max(0, _pet.Mood - 5);
            }

            // Élet csökkenése, ha az éhség vagy fáradtság kritikus
            if (_pet.Hunger >= 100 || _pet.Tiredness >= 100)
            {
                _pet.Health = Math.Max(0, _pet.Health - 10);
            }
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
