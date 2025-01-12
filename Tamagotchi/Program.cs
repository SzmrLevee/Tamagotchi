using System;
using TamagotchiLib.Menu;
using TamagotchiLib.Accounts;
using TamagotchiLib.Models;
using TamagotchiLib.Jatekmenet;
using TamagotchiLib.Animations;
using TamagotchiLib.Utils;

GameManager gameManager = new GameManager();
Mentes mentes = new Mentes(); // Fiókkezelés példányosítása
IdoMulas idoMulas = null; // Időzítő referencia

while (true)
{
    // Képernyő törlése minden iteráció elején
    Console.Clear();

    // TAMAGOTCHI felirat megjelenítése
    Console.WriteLine("=== TAMAGOTCHI JÁTÉK ===");
    Console.WriteLine();

    // Ellenőrzés: Be van-e jelentkezve a felhasználó
    if (string.IsNullOrEmpty(Mentes.valasztottFiokNev) || gameManager.CurrentPet == null)
    {
        Console.WriteLine("Nem vagy bejelentkezve, vagy nincs kisállat betöltve.");
        Console.WriteLine("Fiókkezelés menü megnyitása...");
        mentes.Fomenu(); // Fiókkezelő menü megnyitása
        gameManager.LoadPet(); // Kisállat betöltése
        continue;
    }

    // Indítsuk el az időmúlást, ha még nincs elindítva
    if (idoMulas == null)
    {
        idoMulas = new IdoMulas(gameManager.CurrentPet);
    }

    // Főmenü megjelenítése
    Console.WriteLine("1. Játszani a kisállattal");
    Console.WriteLine("2. Bolt megnyitása");
    Console.WriteLine("3. Inventory megtekintése");
    Console.WriteLine("4. Kisállat állapota megtekintése");
    Console.WriteLine("5. Fiókkezelés");
    Console.WriteLine("6. Kilépés");
    Console.Write("Válassz egy opciót: ");

    // Felhasználói bemenet feldolgozása
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        switch (choice)
        {
            case 1: // Játszani a kisállattal
                new JatekmenetView(gameManager.CurrentPet).MainFuttatasa();
                break;

            case 2: // Bolt megnyitása
                new ShopMenu(gameManager).Display(); // Bolt menü megnyitása
                break;

            case 3: // Inventory megtekintése
                ShowInventory(mentes); // Inventory megjelenítése
                break;

            case 4: // Kisállat állapota megtekintése
                Console.Clear();
                Console.WriteLine("=== Kisállat Állapota ===");
                Console.WriteLine(gameManager.CurrentPet.ToString());
                Console.WriteLine("\nNyomj egy gombot a visszalépéshez.");
                Console.ReadKey();
                break;

            case 5: // Fiókkezelés
                Console.Clear();
                mentes.Fomenu(); // Fiókkezelő menü
                break;

            case 6: // Kilépés
                Console.WriteLine("Kilépés...");
                idoMulas?.Stop(); // Időmúlás leállítása
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Érvénytelen választás! Próbáld újra.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Érvénytelen bemenet! Próbáld újra.");
    }
}

// Inventory megjelenítésének metódusa
void ShowInventory(Mentes mentes)
{
    Console.Clear();
    Console.WriteLine("=== Inventory ===\n");

    // Inventory lekérése
    string[] inventoryItems = mentes.GetInventory(Mentes.valasztottFiokNev);

    if (inventoryItems == null || inventoryItems.Length == 0)
    {
        Console.WriteLine("Az inventory üres.");
    }
    else
    {
        foreach (var item in inventoryItems)
        {
            Console.WriteLine(item);
        }
    }

    Console.WriteLine("\nNyomj egy gombot a visszalépéshez.");
    Console.ReadKey();
}
