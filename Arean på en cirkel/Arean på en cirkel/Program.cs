using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arean_på_en_cirkel
{
    class Program
    {
        static void Main(string[] args)
        {
            double R, summa; 
            Console.WriteLine("Skriv cirkelns radie: ");
            R = double.Parse(Console.ReadLine());
            if (R <= 0)
            {
                Console.WriteLine("Arean är för liten för att beräknas");
            } 
            else
            {
                summa = (R * R) * Math.PI;
                Console.WriteLine("Arean på cirkeln är: " + Math.Round(summa, 2) + "cm^2");
                Console.ReadKey();
            }
        }
    }
}
