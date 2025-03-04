class Program
{
    static void DrawTable(List<int> accepted, List<int> rejected)
    {
        Console.WriteLine("   0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15");
        Console.WriteLine("----------------------------------------------------------------");

        int sidebar = 0;
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
            else if (rejected.Contains(i))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.Write($"{i, 4}");
            Console.ResetColor();
        }
        Console.WriteLine(" | " + sidebar);
        Console.WriteLine($"\n\nGesamtanzahl gültiger Werte: {accepted.Count()} von 256 \n");
    }

    static void Main()
    {
        List<int> accepted = new List<int>();
        List<int> rejected = new List<int>();

        bool reset = true;
        bool exit = false;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ACL Wildcard Tester ===\n");
            Console.WriteLine("Legende:");
            Console.WriteLine("✅ Grün: Akzeptierte Zahlen");
            Console.WriteLine("❌ Rot: Explizit abgelehnte Zahlen");
            Console.WriteLine(
                "⚠️ Gelb: Implizit abgelehnte Zahlen (nicht explizit erlaubt oder abgelehnt)\n"
            );
            Console.WriteLine("x für exit oder r für reset bei jedem input");

            DrawTable(accepted, rejected);

            if (reset)
            {
                accepted.Clear();
                rejected.Clear();
                reset = false;
                continue;
            }

            bool DenyMode = false;
            while (true && reset == false)
            {
                Console.Write("Modus (Permit(p)/Deny(d)): ");
                string modestring = Console.ReadLine().Trim().ToLower();
                if (modestring == "p" || modestring == "d")
                {
                    DenyMode = modestring == "d";
                    break;
                }
                else if (modestring == "r")
                {
                    reset = true;
                    continue;
                }
                else if (modestring == "x")
                {
                    reset = true;
                    exit = true;
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Ungültige Eingabe. Bitte 'p' für Permit oder 'd' für Deny eingeben."
                    );
                }
            }

            int num = 0;
            while (true && reset == false)
            {
                Console.Write("Startzahl (0-255): ");
                string numstring = Console.ReadLine();
                if (int.TryParse(numstring, out num) && num >= 0 && num <= 255)
                {
                    break;
                }
                else if (numstring == "r")
                {
                    reset = true;
                    continue;
                }
                else if (numstring == "x")
                {
                    reset = true;
                    exit = true;
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Ungültige Eingabe. Bitte eine Zahl zwischen 0 und 255 eingeben."
                    );
                }
            }

            int wildcard = 0;
            while (true && reset == false)
            {
                Console.Write("Wildcard (0-255): ");
                string numstring = Console.ReadLine();
                if (int.TryParse(numstring, out wildcard) && wildcard >= 0 && wildcard <= 255)
                {
                    break;
                }
                else if (numstring == "r")
                {
                    reset = true;
                    continue;
                }
                else if (numstring == "x")
                {
                    reset = true;
                    exit = true;
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Ungültige Eingabe. Bitte eine Zahl zwischen 0 und 255 eingeben."
                    );
                }
            }
            if (exit)
            {
                break;
            }
            if (reset)
            {
                continue;
            }
            byte byte1 = (byte)num;
            byte byteWildcard = (byte)wildcard;

            for (int i = 0; i <= 255; i++)
            {
                byte current = (byte)i;
                if ((current & ~byteWildcard) == (byte1 & ~byteWildcard))
                {
                    if (DenyMode)
                    {
                        if (!accepted.Contains(i))
                            rejected.Add(i);
                    }
                    else
                    {
                        if (!rejected.Contains(i))
                            accepted.Add(i);
                    }
                }
            }
        }
    }
}
