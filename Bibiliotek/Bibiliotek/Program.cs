using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bibiliotek
{
    class Bok
    {
        private string titel;
        private string författare;

        public Bok(string t, string f)
        {
            titel = t;
            författare = f;
        }

        public string Titel
        {
            get
            {
                return titel;

            }
            set
            {
                titel = value;
            }
        }

        public string Författare
        {
            get
            {
                return författare;
            }
            set
            {
                författare = value;
            }
        }

        public void SkrivUt()
        {
            Console.WriteLine(titel + " " + författare);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Bok> minBok = new List<Bok>();
            string tempFörfattare, tempTitel, temppos;
            int pos;
            bool status = true;
            minBok.Clear();
            StreamReader läsfil = new StreamReader("minBok.txt");
            string s;
            while ((s = läsfil.ReadLine()) != null)
            {
                string[] bokdata = s.Split(',');
                tempTitel = bokdata[0];
                tempFörfattare = bokdata[1];
                minBok.Add(new Bok(tempTitel, tempFörfattare));
            }
            läsfil.Close();
            while (status == true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till biblioteksprogrammet!");
                Console.WriteLine("T söka på Titel.");
                Console.WriteLine("F söka på Författare.");
                Console.WriteLine("L Låna bok.");
                Console.WriteLine("Å återlämna bok.");
                Console.WriteLine("N lägga in Ny bok.");
                Console.WriteLine("B ta Bort bok.");
                Console.WriteLine("A lista alla böcker.");
                Console.WriteLine("S Sluta.");

                char x;
                Console.WriteLine();
                Console.WriteLine("Vad vill du göra?");
                x = char.Parse(Console.ReadLine());

                if (x == 'T' || x == 't')
                {
                    Console.WriteLine("Vilken bok vill du ha?");
                    Console.ReadKey();
                }
                else if (x == 'F' || x == 'f')
                {
                    Console.WriteLine("Vilken författare letar du efter?");
                    Console.ReadKey();
                }
                else if (x == 'L' || x == 'l')
                {
                    Console.WriteLine("Vilken bok vill du låna?");
                    Console.ReadKey();
                }
                else if (x == 'Å' || x == 'å')
                {
                    Console.WriteLine("Vilken bok vill du återlämna?");
                    Console.ReadKey();
                }
                else if (x == 'N' || x == 'n')
                {
                    Console.WriteLine("Vilken bok vill du lägga till?");
                    {
                        Console.Write("Namn på Bok:");
                        Console.ReadKey();
                        tempTitel = Console.ReadLine();
                        Console.Write("Författare:");
                        tempFörfattare = Console.ReadLine();
                        minBok.Add(new Bok(tempTitel, tempFörfattare));
                        Console.WriteLine("Boken har lagts till!");
                        Console.ReadKey();
                    }
                }
                else if (x == 'B' || x == 'b')
                {
                    Console.WriteLine("Vilken bok vill du ta bort?");
                    {
                        Console.WriteLine("Ange position på vilken Bok du vill ta bort:");
                        for (int i = 0; i < minBok.Count; i++)
                        {
                            Console.Write((i + 1) + ":");
                            minBok[i].SkrivUt();
                        }
                        temppos = Console.ReadLine();
                        pos = int.Parse(temppos);
                        minBok.RemoveAt(pos - 1);
                        Console.WriteLine("Boken har tagits bort!");
                        Console.ReadKey();
                    }
                }
                else if (x == 'A' || x == 'a')
                {
                    Console.WriteLine("Här kommer listan på alla böcker");
                    for (int i = 0; i < minBok.Count; i++)
                    {
                        minBok[i].SkrivUt();
                    }
                    Console.ReadKey();
                }
                else if (x == 'S' || x == 's')
                {
                    Console.WriteLine("Bibilioteket stänger av");
                    status = false;
                }
            }
            Console.WriteLine("Filen minBok.txt har nu sparats");
            StreamWriter skrivfil = new StreamWriter("minBok.txt");
            for (int i = 0; i < minBok.Count; i++)
                skrivfil.WriteLine(minBok[i].Titel + "," + minBok[i].Författare);
            skrivfil.Close();
            Console.ReadKey();
        }
    }
}