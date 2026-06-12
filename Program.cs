using System.Diagnostics.Tracing;

namespace Aplikace_pro_správu_osobnich_financi_a_rozpoctu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Penezenka Mojepenezenka = new Penezenka();
            SouborovyManager spravceSouboru = new SouborovyManager();
            GrafRender rendererGrafu = new GrafRender();
            string nazevSouboru = "finance.txt";
            Mojepenezenka.Historie = spravceSouboru.NactiData(nazevSouboru);

            
            while (true)
            {
                
                Console.WriteLine("Aplikace pro finanční rozpočet");
                Console.WriteLine($"Načteno transakcí z historie: {Mojepenezenka.Historie.Count}\n");
                Console.WriteLine("1. Přidat transakci, \n2. Vypsat historii, \n3. Zobrazit celkový zůstatek, \n4. Zobrazit roční graf, \n5. Uložit a odejít");

                int volba = Overeni_intu();

                switch (volba)
                {
                    case 1:
                        //Přidat transakci
                        Console.Write("Zadej částku: ");
                        double castka = Overeni_double();

                        Console.Write("Zadej den: ");
                        int den = Overeni_intu();

                        Console.Write("Zadej měsíc: ");
                        int mesic = Overeni_intu();

                        Console.Write("Zadej rok: ");
                        int rok = Overeni_intu();

                        Console.Write("Zadej popis: ");
                        string popis = Console.ReadLine();

                        Console.Write("Zadej kategorii: ");
                        string kategorie = Console.ReadLine();

                        Console.Write("Je to příjem? (1 = ano / 0 = ne): ");
                        int typ = Overeni_intu();
                        bool jePrijem = (typ == 1);

                        Transakce NovaTransakce = new Transakce(castka, den, mesic, rok, popis, kategorie, jePrijem);

                        Mojepenezenka.PridatTransakci(NovaTransakce);
                        Console.WriteLine("Transakce prijata");
                        Console.ReadLine();
                        Console.Clear();

                        break;
                    case 2:
                        //Vypsat historii
                        Mojepenezenka.VypisHistorii();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        //Zobrazit celkový zůstatek
                        double zustatek = Mojepenezenka.SpoctiZustatek();
                        Console.WriteLine($"Aktuální zůstatek je:{zustatek} kč");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        //Zobrazit roční graf
                        Console.WriteLine("Zadej rok pro graf");
                        int VybranyRok = Overeni_intu();
                        rendererGrafu.VykreslyGraf(Mojepenezenka, VybranyRok);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        //Uložit a odejít
                        spravceSouboru.UlozData(nazevSouboru, Mojepenezenka.Historie);
                        Console.WriteLine("Data byla uložena");
                        
                        return;
                    default:
                        Console.WriteLine("Chyba při vstupu");
                        break;

                }



            }

            
        }

        static int Overeni_intu()
        {
            int cislo;
            while (!int.TryParse(Console.ReadLine(), out cislo))
            {
                Console.Write("Neplatné číslo, zadej znovu: ");
            }
            return cislo;
        }
        static double Overeni_double()
        {
            double cislo;
            while (!double.TryParse(Console.ReadLine(), out cislo)) 
            {
                Console.Write("Neplatná částka, zadej znovu: ");
            }
            return cislo;
        }
    }
}
