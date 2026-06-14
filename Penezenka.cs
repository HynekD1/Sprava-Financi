using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aplikace_pro_správu_osobnich_financi_a_rozpoctu
{
    internal class Penezenka
    {
        public List<Transakce> Historie = new List<Transakce>();


        public int SmazJednuTransakciPodleData(int den, int mesic, int rok)
        {
            List<int> nalezeneIndexy = new List<int>();
            for (int i = 0; i < Historie.Count; i++)
            {
                if (Historie[i].Den == den && Historie[i].Mesic == mesic && Historie[i].Rok == rok)
                {
                    nalezeneIndexy.Add(i);
                }
            }

            if (nalezeneIndexy.Count == 0)
            {
                return 0;
            }
            
            Console.WriteLine($"\nTransakce pro den: {den}.{mesic}.{rok}:");
            for (int i = 0; i < nalezeneIndexy.Count; i++)
            {
                int originalniIndex = nalezeneIndexy[i];
                Console.Write($"[{i + 1}] ");
                Historie[originalniIndex].VypisDetail();
            }
            
            Console.Write($"Zadej číslo transakce na smazání (1-{nalezeneIndexy.Count}): ");
            int volba;
            while (!int.TryParse(Console.ReadLine(), out volba) || volba < 1 || volba > nalezeneIndexy.Count)
            {
                Console.Write($"Neplatný vstup! Zadej číslo od 1 do {nalezeneIndexy.Count}: ");
            }
            
            int indexKeSmazani = nalezeneIndexy[volba - 1];
            Historie.RemoveAt(indexKeSmazani);

            return 1;
        }


        public double SpoctiMesicniPrijmy(int mesic, int rok)
        {
            double suma = 0;
            foreach (var t in Historie)
            {
                if (t.JePrijem && t.Mesic == mesic && t.Rok == rok)
                {
                    suma += t.Castka;
                }
            }

            return suma;
        }

        public double SpoctiMesicniVydaje(int mesic, int rok)
        {
            double suma = 0;
            foreach (var t in Historie)
            {
                if (!t.JePrijem && t.Mesic == mesic && t.Rok == rok)
                {
                    suma += t.Castka;
                }
            }
            return suma;
        }
       public void PridatTransakci(Transakce novaTransakce)
       {
            this.Historie.Add(novaTransakce);
       }

        public void VypisHistorii()
        {
            foreach (var t in Historie)
            {
                t.VypisDetail();
            }
        }

        public double SpoctiZustatek()
        {
            double zustatek = 0;
            foreach (var t in Historie)
            {
                if (t.JePrijem)
                {
                    zustatek += t.Castka;
                }
                else
                {
                    zustatek -= t.Castka;
                }
            }
            return zustatek;
        }

    }
}
