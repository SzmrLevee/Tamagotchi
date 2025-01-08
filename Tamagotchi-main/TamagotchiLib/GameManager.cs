using TamagotchiLib.MiniGames;
using TamagotchiLib.Models;
using TamagotchiLib.Utils;

namespace TamagotchiLib
{
    public class GameManager
    {
        public string Prompt { get; private set; }

        public GameManager()
        {
            Prompt = @"
 _______         __  __            _____   ____  _______  _____  _    _  _____ 
|__   __| /\    |  \/  |    /\    / ____| / __ \|__   __|/ ____|| |  | ||_   _|
    | |   /  \   | \  / |   /  \  | |  __ | |  | |  | |  | |     | |__| |  | |  
    | |  / /\ \  | |\/| |  / /\ \ | | |_ || |  | |  | |  | |     |  __  |  | |  
    | | / ____ \ | |  | | / ____ \| |__| || |__| |  | |  | |____ | |  | | _| |_ 
    |_|/_/    \_\|_|  |_|/_/    \_\\_____| \____/   |_|   \_____||_|  |_||_____|
";


        }

        public int PlayerMoney { get; set; } = 500; // Kezdeti pénzösszeg

        public void BuyItem(string itemName, int itemPrice)
        {
            if (PlayerMoney >= itemPrice)
            {
                PlayerMoney -= itemPrice;
                Console.WriteLine($"Vásároltál egy {itemName}-t! Kiadott pénz: {itemPrice} Pénz, Maradó: {PlayerMoney}");
                // További logika hozzáadása, például az adott termék hatása a Tamagotchi-ra
            }
            else
            {
                Console.WriteLine("Nincs elég pénzed a vásárláshoz!");
            }

            Console.WriteLine("Nyomj egy gombot a folytatáshoz.");
            Console.ReadKey();
            Console.Clear();
        }

        public Pet CurrentPet { get; private set; }
        private readonly FileManager fileManager = new FileManager("Data/pet_data.json");


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
