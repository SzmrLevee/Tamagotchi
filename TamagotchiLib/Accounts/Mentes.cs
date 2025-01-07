using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TamagotchiLib.Accounts
{
    public class Mentes
    {
        public static string valasztottFiokNev { get; set; } // A választott fiók neve

        private static readonly string FilePath;

        static Mentes()
        {
            // Dinamikusan meghatározzuk a projekt gyökérkönyvtárát
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
            var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
            FilePath = Path.Combine(classLibDirectory, "settings.txt");

            // Ellenőrizzük, hogy a fájl létezik-e, ha nem, akkor hozzuk létre és írjunk bele egy fejlécet
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath)); // Győződjünk meg arról, hogy a mappa létezik
                using (StreamWriter sw = new StreamWriter(FilePath, false))
                {
                    sw.WriteLine("Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;LetrehozasIdeje");
                }
            }

            // Beállítjuk a valasztottFiokNev változót az első fiók nevére
            string[] fiokok = File.ReadAllLines(FilePath);
            if (fiokok.Length > 1)
            {
                valasztottFiokNev = fiokok[1].Split(';')[0];
            }
            else
            {
                valasztottFiokNev = null;
            }
        }

        public void Fomenu()
        {
            while (true)
            {
                Console.Clear();
                string prompt = "Fiókkezelés\n";
                string[] opciok = { "Új fiók", "Fiókjaim", "Fiók törlése", "Összes fiók törlése", "Vissza a főmenübe" };
                Menu menu = new Menu(prompt, opciok);
                int Megjelolt = menu.Futas();
                Jatekmenet jatekmenet = new Jatekmenet();

                switch (Megjelolt)
                {
                    case 0:
                        UjFiok();
                        break;
                    case 1:
                        Fiokjaim();
                        break;
                    case 2:
                        FiokTorles();
                        break;
                    case 3:
                        AdminJelszoBelekerese(); // Admin jelszó kérése az összes fiók törléséhez
                        break;
                    case 4:
                        // Ha van már legalább egy fiók és nem választott ki másik fiókot,
                        // akkor a kilépéskor automatikusan a fiókok közül az első lesz kiválasztva
                        if (FiokokSzama() > 0 && string.IsNullOrEmpty(valasztottFiokNev))
                        {
                            string[] fiokok = File.ReadAllLines(FilePath);
                            valasztottFiokNev = fiokok[1].Split(';')[0]; // Az első fiók neve lesz a választott
                        }

                        if (!string.IsNullOrEmpty(valasztottFiokNev))
                        {
                            jatekmenet.MainFuttatasa();
                        }
                        else
                        {
                            Console.WriteLine("Legalább egy fiók létrehozása szükséges.");
                            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Console.WriteLine("Érvénytelen opció, próbáld újra.");
                        break;
                }
            }
        }

        private void AdminJelszoBelekerese()
        {
            Console.Clear();
            string adminJelszo = GetPassword();

            if (adminJelszo == "asdasdasd")
            {
                OsszesFiokTorles(); // Csak az admin jelszó megadása esetén történik az összes fiók törlése
            }
            else
            {
                Console.WriteLine("Hibás admin jelszó.");
                Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
                Console.ReadKey();
            }
        }

        private int FiokokSzama()
        {
            if (File.Exists(FilePath))
            {
                string[] fiokok = File.ReadAllLines(FilePath);
                return fiokok.Length - 1; // Kivonjuk az 1-et, hogy ne számoljuk bele a fejlécet
            }
            return 0;
        }

        private string GetPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            Console.Write("Add meg a jelszót (legalább 1 karakter): ");
            do
            {
                key = Console.ReadKey(true);

                // Ha a felhasználó lenyomta a Backspace-t, akkor töröljük az utolsó karaktert a jelszóból
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1]; // Töröljük az utolsó karaktert
                    CursorLeft--;
                    Console.Write(" ");
                    CursorLeft--;
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Új sorba lépünk a jelszó bevitele után

            return password;
        }

        private void UjFiok()
        {
            Console.Clear();
            string nev = "";
            string jelszo = "";

            // Addig kérjük be az új fiók nevét, amíg legalább egy karaktert nem ad meg a felhasználó
            while (string.IsNullOrEmpty(nev))
            {
                Console.Clear();
                Console.Write("Add meg az új fiók nevét (legalább 1 karakter), vagy nyomj Enter-t a visszalépéshez: ");
                nev = Console.ReadLine()?.Trim(); // A beolvasott szöveget trimmeljük, hogy eltávolítsuk a felesleges szóközöket

                if (string.IsNullOrWhiteSpace(nev))
                {
                    // Ha csak Enter-t ütött, akkor reseteljük
                    return;
                }
            }

            // Ellenőrizzük, hogy már létezik-e ilyen nevű fiók
            if (FiokNevLetezik(nev))
            {
                Console.WriteLine("Ezzel a névvel már létezik fiók.");
                Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            // Addig kérjük be az új fiók jelszavát, amíg legalább egy karaktert nem ad meg a felhasználó
            while (string.IsNullOrEmpty(jelszo))
            {
                Console.Clear();
                jelszo = GetPassword(); // Jelszó beolvasása

                if (string.IsNullOrWhiteSpace(jelszo))
                {
                    // Ha csak Enter-t ütött, akkor reseteljük
                    return;
                }
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    string currentTime = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss"); // Aktuális idő formázása
                    sw.WriteLine($"{nev};{jelszo};0;0;0;0;0;100;{currentTime}"); // Fiók hozzáadása a létrehozási idővel, alapértelmezett Hp: 100
                }
                Console.WriteLine("Az új fiók sikeresen létrehozva:");
                valasztottFiokNev = nev; // Az újonnan létrehozott fiók lesz a választott
                Console.WriteLine(valasztottFiokNev);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hiba történt a fiók mentése közben: {e.Message}");
            }

            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            Console.ReadKey();
        }



        private bool FiokNevLetezik(string nev)
        {
            if (File.Exists(FilePath))
            {
                string[] fiokok = File.ReadAllLines(FilePath);
                for (int i = 1; i < fiokok.Length; i++) // Kezdjük az 1-es indexnél, hogy kihagyjuk a fejlécet
                {
                    string fiokNev = fiokok[i].Split(';')[0];
                    if (fiokNev.Equals(nev, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Fiokjaim()
        {
            Console.Clear();
            string[] fiokok = File.ReadAllLines(FilePath);
            if (fiokok.Length <= 1)
            {
                Console.WriteLine("Nincsenek fiókok.");
                Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            int valasztottIndex;
            while (true)
            {
                Console.WriteLine("Fiókok:");
                for (int i = 1; i < fiokok.Length; i++)
                {
                    // Megjelenítjük csak a nevet és a létrehozási időt
                    string[] adatok = fiokok[i].Split(';');
                    Console.WriteLine($"{i}. Név: {adatok[0]}, Létrehozás ideje: {adatok[adatok.Length - 1]}");
                }
                Console.Write("Add meg a választott fiók sorszámát (1-től kezdve), vagy nyomj Enter-t a visszalépéshez: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    // Ha csak Enter-t ütött, akkor reseteljük
                    return;
                }
                else if (int.TryParse(input, out valasztottIndex))
                {
                    if (valasztottIndex >= 1 && valasztottIndex < fiokok.Length)
                    {
                        break; // Kilépünk a ciklusból ha a szám érvényes
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen sorszámú fiók.");
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Érvénytelen bemenet.");
                    Console.Clear();
                }
            }

            // Jelszó kérése a fiók kiválasztásához
            string jelszo = GetPassword();
            string[] valasztottFiokAdatok = fiokok[valasztottIndex].Split(';');

            // Ellenőrizzük, hogy a megadott jelszó egyezik-e a kiválasztott fiókhoz tartozó jelszóval
            if (jelszo == valasztottFiokAdatok[1])
            {
                valasztottFiokNev = valasztottFiokAdatok[0]; // A választott fiók neve lesz a választottFiokNev
                Console.WriteLine($"A kiválasztott fiók: {valasztottFiokNev}");
            }
            else
            {
                Console.WriteLine("Hibás jelszó.");
                Console.ReadKey();
            }

            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        private void FiokTorles()
        {
            Console.Clear();
            string[] fiokok = File.ReadAllLines(FilePath);
            if (fiokok.Length <= 1)
            {
                Console.WriteLine("Nincsenek fiókok a törléshez.");
                Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
                Console.ReadKey();
                return;
            }

            int fiokSzam;
            while (true)
            {
                Console.WriteLine("Fiókok:");
                for (int i = 1; i < fiokok.Length; i++)
                {
                    // Megjelenítjük csak a nevet és a létrehozási időt
                    string[] adatok = fiokok[i].Split(';');
                    Console.WriteLine($"{i}. Név: {adatok[0]}, Létrehozás ideje: {adatok[adatok.Length - 1]}");
                }
                Console.Write("Add meg a törlendő fiók sorszámát (1-től kezdve), vagy nyomj Enter-t a visszalépéshez: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    // Ha csak Enter-t ütött, akkor reseteljük
                    return;
                }
                else if (int.TryParse(input, out fiokSzam))
                {
                    if (fiokSzam >= 1 && fiokSzam < fiokok.Length)
                    {
                        break; // Kilépünk a ciklusból ha a szám érvényes
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen sorszámú fiók.");
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Érvénytelen bemenet.");
                    Console.Clear();
                }
            }

            // Jelszó kérése a fiók törléséhez
            string jelszo = GetPassword();
            string[] valasztottFiokAdatok = fiokok[fiokSzam].Split(';');

            // Ellenőrizzük, hogy a megadott jelszó egyezik-e a kiválasztott fiókhoz tartozó jelszóval
            if (jelszo == valasztottFiokAdatok[1])
            {
                Console.WriteLine($"Biztosan törölni szeretnéd a(z) {valasztottFiokAdatok[0]} nevű fiókot? (i/n)");
                string valasz = Console.ReadLine()?.ToLower();

                if (valasz == "i")
                {
                    List<string> fiokLista = new List<string>(fiokok);
                    fiokLista.RemoveAt(fiokSzam);

                    try
                    {
                        File.WriteAllLines(FilePath, fiokLista);
                        valasztottFiokNev = null;
                        Console.WriteLine("A fiók sikeresen törölve.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Hiba történt a fiók törlése közben: {e.Message}");
                    }
                }
                else if (valasz == "n")
                {
                    Console.WriteLine("A törlés megszakítva.");
                }
                else
                {
                    Console.WriteLine("Érvénytelen válasz.");
                }
            }
            else
            {
                Console.WriteLine("Hibás jelszó.");
                Console.ReadKey();
            }

            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            Console.ReadKey();
        }

        private void OsszesFiokTorles()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Biztosan törölni szeretnéd az összes fiókot? (i/n)");
                string valasz = Console.ReadLine()?.ToLower();

                if (valasz == "i")
                {
                    File.WriteAllLines(FilePath, new[] { "Nev;Jelszo;Pisztoly;Ak;Kotszer;Koktel;Joint;Hp;LetrehozasIdeje" });
                    valasztottFiokNev = null;
                    Console.WriteLine("Minden fiók sikeresen törölve.");
                }
                else if (valasz == "n")
                {
                    Console.WriteLine("Az összes fiók törlése megszakítva.");
                }
                else
                {
                    Console.WriteLine("Érvénytelen válasz.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hiba történt az összes fiók törlése közben: {e.Message}");
            }

            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz.");
            Console.ReadKey();
        }
    }
}
