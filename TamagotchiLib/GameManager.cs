using TamagotchiLib.MiniGames;
using TamagotchiLib.Models;
using TamagotchiLib.Utils;

namespace TamagotchiLib
{
    public class GameManager
    {
        public Pet CurrentPet { get; private set; }
        private readonly FileManager fileManager = new FileManager("Data/pet_data.json");

        public GameManager()
        {
            LoadPet();
        }

        public void LoadPet()
        {
            CurrentPet = fileManager.LoadData<Pet>() ?? new Pet("Kisállatka");
        }

        public void SavePet()
        {
            fileManager.SaveData(CurrentPet);
        }

        public void PlayMinigame(int gameChoice)
        {
            switch (gameChoice)
            {
                case 1:
                    new ColorGuessGame().Play(CurrentPet);
                    break;
                case 2:
                    new MathChallengeGame().Play(CurrentPet);
                    break;
                default:
                    Console.WriteLine("Érvénytelen játékválasztás.");
                    break;
            }
            SavePet();
        }

        public void DisplayStatus()
        {
            Console.WriteLine(CurrentPet.ToString());
        }
    }
}
