using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikace_pro_správu_osobnich_financi_a_rozpoctu
{
    internal class SouborovyManager
    {
        public void UlozData(string cestaKSouboru, List<Transakce> historie)
        {
            using(StreamWriter sw = new StreamWriter(cestaKSouboru))
            {
                foreach (var t in historie)
                {
                
                    string radek = $"{t.Castka};{t.Den};{t.Mesic};{t.Rok};{t.Popis};{t.Kategorie};{t.JePrijem}";
                    sw.WriteLine(radek);
                }
            }
        }

        public List<Transakce> NactiData (string cestaKSouboru)
        {
            List<Transakce> nactenaHistorie = new List<Transakce>();

            if (!File.Exists(cestaKSouboru)) 
            { 
                return nactenaHistorie;
            }

            
            using (StreamReader sr = new StreamReader(cestaKSouboru))
            {
                string radek;
                while ((radek = sr.ReadLine()) != null)
                {
                    string[] casti = radek.Split(';');
                    double castka = double.Parse(casti[0]);
                    int den = int.Parse(casti[1]);
                    int mesic = int.Parse(casti[2]);
                    int rok = int.Parse(casti[3]);
                    string popis = casti[4];
                    string kategorie = casti[5];
                    bool jePrijem = bool.Parse(casti[6]);

                    Transakce t = new Transakce(castka, den, mesic, rok, popis, kategorie, jePrijem);

                    nactenaHistorie.Add(t);
                }
            }
            
            return nactenaHistorie;
        }
    }
}
