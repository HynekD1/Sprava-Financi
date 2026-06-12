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
        public Dictionary<string, double> LimityKategorii = new Dictionary<string, double>();

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
