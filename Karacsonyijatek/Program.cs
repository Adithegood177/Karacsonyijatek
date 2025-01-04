namespace KaracsonyiAjandek
{
    internal class Program
    {
        static decimal teljesKoltsegvetes;
        static decimal marElkoltott = 0;
        static List<string[]> ajandekLista = new();

        static void Main(string[] args)
        {
            Console.WriteLine("1. Ajándék hozzáadása\n" +
                              "2. Ajándék módosítása\n" +
                              "3. Ajándék eltávolítása\n" +
                              "4. Ajándékok listázása\n" +
                              "5. Költségvetés ellenőrzése\n" +
                              "6. Statisztikák megtekintése\n" +
                              "7. Kilépés\n");

            while (true)
            {
                Console.Write("Válassz egy opciót: ");
                string opcio = Console.ReadLine();

                switch (opcio)
                {
                    case "1":
                        Console.Write("Ajándék neve: ");
                        string nev = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(nev))
                        {
                            Console.WriteLine("Az ajándék neve nem lehet üres.");
                            break;
                        }

                        Console.Write("Ajándék ára (Ft): ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal ar) || ar <= 0)
                        {
                            Console.WriteLine("Hibás ár. Pozitív számot kell megadni.");
                            break;
                        }

                        Console.Write("Ajándék kategóriája: ");
                        string kategoria = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(kategoria))
                        {
                            Console.WriteLine("A kategória nem lehet üres.");
                            break;
                        }

                        ajandekLista.Add(new string[] { nev, ar.ToString(), kategoria });
                        marElkoltott += ar;
                        Console.WriteLine("Ajándék sikeresen hozzáadva!");
                        break;

                    case "2":
                        Console.WriteLine("Ajándékok listája:");
                        for (int i = 0; i < ajandekLista.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {ajandekLista[i][0]} - {ajandekLista[i][1]} Ft ({ajandekLista[i][2]})");
                        }

                        Console.Write("Add meg a módosítandó ajándék sorszámát: ");
                        if (int.TryParse(Console.ReadLine(), out int modositandoIndex) && modositandoIndex > 0 && modositandoIndex <= ajandekLista.Count)
                        {
                            string[] modositandoAjandek = ajandekLista[modositandoIndex - 1];

                            Console.Write("Új név (Enter, ha nem módosítod): ");
                            string ujNev = Console.ReadLine()?.Trim();
                            if (!string.IsNullOrWhiteSpace(ujNev))
                            {
                                modositandoAjandek[0] = ujNev;
                            }

                            Console.Write("Új ár (Ft) (Enter, ha nem módosítod): ");
                            string ujArInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(ujArInput) && decimal.TryParse(ujArInput, out decimal ujAr) && ujAr > 0)
                            {
                                marElkoltott = marElkoltott - decimal.Parse(modositandoAjandek[1]) + ujAr;
                                modositandoAjandek[1] = ujAr.ToString();
                            }

                            Console.Write("Új kategória (Enter, ha nem módosítod): ");
                            string ujKategoria = Console.ReadLine()?.Trim();
                            if (!string.IsNullOrWhiteSpace(ujKategoria))
                            {
                                modositandoAjandek[2] = ujKategoria;
                            }

                            Console.WriteLine("Ajándék sikeresen módosítva!");
                        }
                        else
                        {
                            Console.WriteLine("Érvénytelen sorszám.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Ajándékok listája:");
                        for (int i = 0; i < ajandekLista.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {ajandekLista[i][0]} - {ajandekLista[i][1]} Ft ({ajandekLista[i][2]})");
                        }

                        Console.Write("Add meg az eltávolítandó ajándék sorszámát: ");
                        if (int.TryParse(Console.ReadLine(), out int g) && g > 0 && g <= ajandekLista.Count)
                        {
                            marElkoltott -= decimal.Parse(ajandekLista[g - 1][1]);
                            ajandekLista.RemoveAt(g - 1);
                            Console.WriteLine("Ajándék sikeresen eltávolítva!");
                        }
                        else
                        {
                            Console.WriteLine("Érvénytelen sorszám.");
                        }
                        break;

                    case "4":
                        if (ajandekLista.Count == 0)
                        {
                            Console.WriteLine("Nincsenek ajándékok a listában.");
                        }
                        else
                        {
                            Console.WriteLine("Ajándékok listája:");
                            for (int i = 0; i < ajandekLista.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {ajandekLista[i][0]} - {ajandekLista[i][1]} Ft ({ajandekLista[i][2]})");
                            }
                        }
                        break;

                    case "5":
                        Console.WriteLine($"Eddig elköltött összeg: {marElkoltott} Ft");
                        Console.WriteLine($"Hátralévő költségvetés: {teljesKoltsegvetes - marElkoltott} Ft");
                        if (marElkoltott > teljesKoltsegvetes)
                        {
                            Console.WriteLine("Figyelem: Túllépted a költségvetést!");
                        }
                        break;

                    case "6":
                        if (ajandekLista.Count == 0)
                        {
                            Console.WriteLine("Nincsenek ajándékok a statisztikákhoz.");
                        }
                        else
                        {
                            Console.WriteLine($"Ajándékok száma: {ajandekLista.Count}");
                            Console.WriteLine($"Összérték: {marElkoltott} Ft");
                            var legdragabbAjandek = ajandekLista.OrderByDescending(g => decimal.Parse(g[1])).First();
                            var legolcsobbAjandek = ajandekLista.OrderBy(g => decimal.Parse(g[1])).First();
                            Console.WriteLine($"Legdrágább ajándék: {legdragabbAjandek[0]} ({legdragabbAjandek[1]} Ft)");
                            Console.WriteLine($"Legolcsóbb ajándék: {legolcsobbAjandek[0]} ({legolcsobbAjandek[1]} Ft)");
                        }
                        break;

                    case "7":
                        Console.WriteLine("Boldog karácsonyt!");
                        return;

                    default:
                        Console.WriteLine("Érvénytelen opció. Próbáld újra!");
                        break;
                }
            }
        }
    }
}
