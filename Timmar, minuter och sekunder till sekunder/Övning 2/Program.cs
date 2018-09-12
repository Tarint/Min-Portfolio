using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Övning_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int t1, t2, t3, summa;
            Console.WriteLine("Timmar: ");
            t1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Minuter: ");
            t2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Sekunder: ");
            t3 = Convert.ToInt32(Console.ReadLine());

            summa = (t1 * 3600) + (t2 * 60) + t3;

            Console.Write("Summan är " + summa + " sekunder ");
            Console.ReadKey();
        }
    }
}
