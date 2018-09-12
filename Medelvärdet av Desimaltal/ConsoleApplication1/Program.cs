using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            double tal1, tal2, tal3, tal4, summa;
            Console.WriteLine("Skriv in första desimaltalet: ");

            tal1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in andra desimaltalet: ");

            tal2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in tredje desimaltalet: ");
            tal3 = double.Parse(Console.ReadLine());
            if (tal1 <= 0 || tal2 <= 0 || tal3 <= 0)
                {
                Console.WriteLine("Error. Microsoft Visual Studios.exe have stoped working.");
                Console.ReadKey();
                }
            else if(tal1 > 0 || tal2 > 0 || tal3 > 0)
                {
                tal4 = tal1 + tal2 + tal3;
                summa = tal4 / 3;
                Console.WriteLine("Medelvärdet på talen är:" + summa);
                Console.ReadKey();
                }
        }
    }
}
