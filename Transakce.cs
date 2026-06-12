using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikace_pro_správu_osobnich_financi_a_rozpoctu
{
    internal class Transakce
    {
        public double Castka { get; set; }
        public int den;
        public int mesic;
        public int rok;
        public string Popis { get; set; }
        public string Kategorie { get; set; }
        public bool JePrijem { get; set; }

        public int Rok
        {
            get
            {
                return rok;
            }
            set
            {
                if (value >= 0 && value <= 2026) 
                {
                    rok = value;
                }
            }
        }
        public int Mesic
        {
            get
            {
                return mesic;
            }
            set
            {
                if (value >= 1 && value <= 12)
                {
                    mesic = value;
                }
            }
        }

        public int Den
        {
            get
            {
                return den;
            }
            set
            {
                if (value >=1 && value <= 31)
                {
                    den = value;
                }

            }
             
           
        }

        public Transakce(double castka, int den, int mesic, int rok, string popis, string kategorie, bool jeprijem)
        {
            this.Castka = castka;
            this.Den = den;
            this.Mesic = mesic;
            this.Rok = rok;
            this.Popis = popis;
            this.Kategorie = kategorie;
            this.JePrijem = jeprijem;
        }

        public void VypisDetail()
        {
            if (JePrijem)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Cena: {Castka}\nDatum:{Den} {Mesic} {Rok}\nPopis:{Popis}\nKategorie:{Kategorie}");
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Cena: {Castka}\nDatum:{Den} {Mesic} {Rok}\nPopis:{Popis}\nKategorie:{Kategorie}");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}   
