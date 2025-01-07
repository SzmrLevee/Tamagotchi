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
            var classLibDirectory = Path.Combine(projectDirectory, "ClassLib");
            FilePath = Path.Combine(classLibDirectory, "settings.txt");
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

        public void NovelemPisztoly(string nev) => ModifyFiokData(nev, 2, 1);
        public void CsokkentemPisztoly(string nev) => ModifyFiokData(nev, 2, -1);
        public void NovelemAk(string nev) => ModifyFiokData(nev, 3, 1);
        public void CsokkentemAk(string nev) => ModifyFiokData(nev, 3, -1);
        public void NovelemKotszer(string nev) => ModifyFiokData(nev, 4, 1);
        public void CsokkentemKotszer(string nev) => ModifyFiokData(nev, 4, -1);
        public void NovelemKoktel(string nev) => ModifyFiokData(nev, 5, 1);
        public void CsokkentemKoktel(string nev) => ModifyFiokData(nev, 5, -1);
        public void NovelemJoint(string nev) => ModifyFiokData(nev, 6, 1);
        public void CsokkentemJoint(string nev) => ModifyFiokData(nev, 6, -1);
        public void NovelemHp(string nev) => ModifyFiokData(nev, 7, 1);
        public void CsokkentemHp(string nev) => ModifyFiokData(nev, 7, -1);
    }
}
