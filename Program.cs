using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ACL Wildcard Tester ===\n");

            int num,
                wildcard;

            Console.Write("Startzahl  : ");
            num = Convert.ToInt32(Console.ReadLine());

            Console.Write("Wildcard   : ");
            wildcard = Convert.ToInt32(Console.ReadLine());

            int sidebar = 0;
            byte byte1 = (byte)num;
            byte byteWildcard = (byte)wildcard;

            List<int> accepted = new List<int>();
            List<int> rejected = new List<int>();

            for (int i = 0; i <= 255; i++)
            {
                byte current = (byte)i;
                if ((current & ~byteWildcard) == (byte1 & ~byteWildcard))
                {
                    accepted.Add(current);
                }
                else
                {
                    rejected.Add(current);
                }
            }

            Console.WriteLine("\n✅ Akzeptierte Zahlen (Grün) / ❌ Abgelehnte Zahlen (Rot):");

            Console.WriteLine("   0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15");
            Console.WriteLine("----------------------------------------------------------------");

            for (int i = 0; i <= 255; i++)
            {
                if (i % 16 == 0 && i != 0)
                {
                    Console.WriteLine(" | " + sidebar);
                    sidebar += 16;
                }

                if (accepted.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write($"{i, 4}");
                Console.ResetColor();
            }
            Console.WriteLine(" | " + sidebar);
            Console.WriteLine($"\n\nGesamtanzahl gültiger Werte: {accepted.Count} von 256\n");

            // Ask user if they want to continue or exit
            Console.Write("Neue Eingabe? (J/N): ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input != "j" && input != "y")
            {
                Console.WriteLine("Programm beendet.");
                break;
            }
        }
    }
}
