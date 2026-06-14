using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikace_pro_správu_osobnich_financi_a_rozpoctu
{
    internal class GrafRender
    {
        public void VykreslyGraf(Penezenka penezenka, int rok)
        {
            int[] kosticky = new int[12];
            for (int i = 1; i <= 12; i++)
            {
                double prijmy = penezenka.SpoctiMesicniPrijmy(i, rok);
                double vydaje = penezenka.SpoctiMesicniVydaje(i, rok);

                double suma = prijmy - vydaje;

                int pocetKosticek = (int)(suma / 1000);
                kosticky[i - 1] = pocetKosticek;
            }

            
            for (int vyska = 10; vyska > 0; vyska--)
            {
                for (int m = 0; m < 12; m++)
                {
                    if (kosticky[m] >= vyska)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" ■ ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }

            
            Console.WriteLine("___---___---___---___---___---___---");

            
            for (int hloubka = 1; hloubka <= 5; hloubka++)
            {
                for (int m = 0; m < 12; m++)
                {
                    
                    if (kosticky[m] < 0 && (kosticky[m] * -1) >= hloubka)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" ■ ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("■ = 1000kč");
            Console.WriteLine("Jeden měsíc: ___ / ---");
        }
    }
}
