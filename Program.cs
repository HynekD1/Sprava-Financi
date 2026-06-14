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
                Console.WriteLine("1. Přidat transakci\n2. Vypsat historii\n3. Zobrazit celkový zůstatek\n4. Zobrazit roční graf\n5. Smazat transakce podle data\n6. Uložit a odejít");

                int volba = Overeni_intu();

                switch (volba)
                {
                    case 1:
                        //Přidat transakci
                        double castka = 0;
                        do
                        {
                            Console.Write("Zadej částku: ");
                            castka = Overeni_double();
                            if (castka < 0)
                            {
                                Console.WriteLine("Musíte zadat kladnou částku");
                            }
                        } while (castka < 0);

                        int den = 0;
                        do
                        {
                            Console.Write("Zadej den: ");
                            den = Overeni_intu();
                            if (den < 1 || den > 31)
                            {
                                Console.WriteLine("Neplatný den! Zadej 1-31.");
                            }

                        } while (den < 1 || den > 31);

                        int mesic = 0;
                        do
                        {
                            Console.Write("Zadej měsíc: ");
                            mesic = Overeni_intu();
                            if (mesic < 1 || mesic > 12)
                            {
                                Console.WriteLine("Neplatný měsíc! Zadej 1-12.");
                            }
                        } while (mesic < 1 || mesic > 12);

                        int rok = 0;
                        do
                        {
                            Console.Write("Zadej rok: ");
                            rok = Overeni_intu();
                            if (rok < 0 || rok > 2026)
                            {
                                Console.WriteLine("Neplatný rok! Zadej 1900-2026.");
                            }
                        } while (rok < 0 || rok > 2026);

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
                        Console.WriteLine("\nStisknutím klávesy Enter se vrátíš do hlavního menu...");
                        Console.ReadLine();
                        Console.Clear();

                    break;
                    case 2:
                        //Vypsat historii
                        Mojepenezenka.VypisHistorii();
                        Console.WriteLine("\nStisknutím klávesy Enter se vrátíš do hlavního menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        //Zobrazit celkový zůstatek
                        double zustatek = Mojepenezenka.SpoctiZustatek();
                        Console.WriteLine($"Aktuální zůstatek je:{zustatek} kč");
                        Console.WriteLine("\nStisknutím klávesy Enter se vrátíš do hlavního menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        //Zobrazit roční graf
                        Console.WriteLine("Zadej rok pro graf");
                        int VybranyRok = Overeni_intu();
                        rendererGrafu.VykreslyGraf(Mojepenezenka, VybranyRok);
                        Console.WriteLine("\nStisknutím klávesy Enter se vrátíš do hlavního menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        // Smazat transakce podle data
                        Console.Write("Zadej den: ");
                        int smazDen = Overeni_intu();

                        Console.Write("Zadej měsíc: ");
                        int smazMesic = Overeni_intu();

                        Console.Write("Zadej rok: ");
                        int smazRok = Overeni_intu();

                        int smazano = Mojepenezenka.SmazJednuTransakciPodleData(smazDen, smazMesic, smazRok);

                        if (smazano > 0)
                        {
                            Console.WriteLine($"Úspěšně smazáno transakcí: {smazano}");
                        }
                        else
                        {
                            Console.WriteLine("Pro toto datum nebyly nalezeny žádné transakce.");
                        }
                        Console.WriteLine("\nStisknutím klávesy Enter se vrátíš do hlavního menu...");
                        Console.ReadLine();
                        Console.Clear();
                        
                        break;
                    case 6:
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
