namespace TamagotchiTeszt;
using TamagotchiLib.Utils;
using TamagotchiLib.Models;

[TestClass]
public sealed class Tesztek
{
    [TestClass]
    public class GameManagerTests
    {
        [TestMethod]
        public void Test_LoadPet_CreatesPetIfNoneExists()
        {
            GameManager manager = new GameManager();

            manager.LoadPet();

            Assert.IsNotNull(manager.CurrentPet, "CurrentPet should not be null after loading.");
        }

        [TestMethod]
        public void Test_SavePet_SavesCurrentState()
        {
            GameManager manager = new GameManager();
            manager.LoadPet();

            manager.CurrentPet.Name = "TestPet";
            manager.SavePet();

            GameManager newManager = new GameManager();
            newManager.LoadPet();

            Assert.AreEqual("TestPet", newManager.CurrentPet.Name, "Saved pet state should persist.");
        }
    }
    [TestClass]
    public class PetTests
    {
        [TestMethod]
        public void Test_Play_IncreasesMood()
        {
            Pet pet = new Pet("TestPet");
            int initialMood = pet.Mood;

            pet.Play();

            Assert.IsTrue(pet.Mood > initialMood, "Mood should increase after playing.");
        }

        [TestMethod]
        public void Test_Sleep_DecreasesTiredness()
        {
            Pet pet = new Pet("TestPet") { Tiredness = 50 };

            pet.Sleep();

            Assert.IsTrue(pet.Tiredness < 50, "Tiredness should decrease after sleeping.");
        }

        [TestMethod]
        public void Test_Hunger_IncreasesOverTime()
        {
            Pet pet = new Pet("TestPet") { Hunger = 0 };

            pet.Hunger += 10;

            Assert.AreEqual(10, pet.Hunger, "Hunger should increase over time.");
        }
    }
    
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void Test_AddItem_IncreasesInventory()
        {
            Pet pet = new Pet("TestPet");
            int initialItemCount = pet.Inventory.Count;

            pet.Inventory.Add(new Item("Food", 1));

            Assert.AreEqual(initialItemCount + 1, pet.Inventory.Count, "Inventory count should increase after adding an item.");
        }

        [TestMethod]
        public void Test_UseItem_DecreasesQuantity()
        {
            Pet pet = new Pet("TestPet");
            Item item = new Item("Food", 5);
            pet.Inventory.Add(item);

            item.Quantity--;

            Assert.AreEqual(4, item.Quantity, "Item quantity should decrease after use.");
        }
    }
}