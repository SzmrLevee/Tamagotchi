using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiLib.Accounts
{
    public class FiokKezeles
    {
        private static readonly string FilePath;

        static FiokKezeles()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.Parent.FullName;
            FilePath = Path.Combine(projectDirectory, "settings.txt");
        }

        private string[] GetFiokSorok()
        {
            return File.ReadAllLines(FilePath);
        }

        private void UpdateFiokSorok(string[] fiokSorok)
        {
            File.WriteAllLines(FilePath, fiokSorok);
        }

        private int GetFiokIndex(string nev)
        {
            string[] fiokok = GetFiokSorok();
            for (int i = 1; i < fiokok.Length; i++)
            {
                if (fiokok[i].Split(';')[0] == nev)
                {
                    return i;
                }
            }
            return -1;
        }

        private void ModifyFiokData(string nev, int index, int value)
        {
            int fiokIndex = GetFiokIndex(nev);
            if (fiokIndex == -1) return;

            string[] fiokok = GetFiokSorok();
            string[] adatok = fiokok[fiokIndex].Split(';');
            adatok[index] = (int.Parse(adatok[index]) + value).ToString();
            fiokok[fiokIndex] = string.Join(";", adatok);
            UpdateFiokSorok(fiokok);
        }

        public void Novelem(string nev, string itemName, int amount)
        {
            int index = GetItemIndex(itemName);
            if (index != -1)
            {
                ModifyFiokData(nev, index, amount);
            }
        }

        private int GetItemIndex(string itemName)
        {
            string[] items = { "Pisztoly", "Ak", "Kotszer", "Koktel", "Joint", "Hp" };
            return Array.IndexOf(items, itemName);
        }

    }
}
