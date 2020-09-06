using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnC
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programBezi = true;
            while (programBezi)
            {
                int volba;
            zacatek:
                ZobrazeniNabidky nabidka = new ZobrazeniNabidky();
                volba = nabidka.ZobrazNabidku();
                switch (volba)
                {
                    case 0:
                        CSifry csifra = new CSifry();
                        volba = nabidka.ZobrazDruhouNabidku();
                        switch (volba)
                        {
                            case 0:
                                csifra.Sifrovani();
                                break;
                            case 1:
                                csifra.SKlicem();
                                break;
                            case 2:
                                csifra.BezKlice();
                                break;
                            case 3:
                                goto zacatek;
                        }
                        break;

                    case 1:
                        VSifry vsifra = new VSifry();
                        volba = nabidka.ZobrazDruhouNabidku();
                        switch (volba)
                        {
                            case 0:
                                vsifra.Sifrovani();
                                break;
                            case 1:
                                vsifra.SKlicem();
                                break;
                            case 2:
                                vsifra.BezKlice();
                                break;
                            case 3:
                                goto zacatek;
                        }
                        break;

                    case 2:
                        Morse msifra = new Morse();
                        volba = nabidka.ZobrazMorseovuNabidku();
                        switch (volba)
                        {
                            case 0:
                                msifra.Text();
                                break;
                            case 1:
                                msifra.Morseovka();
                                break;
                            case 2:
                                goto zacatek;
                        }
                        break;

                    case 3:
                        Mob mobsifra = new Mob();
                        mobsifra.Text();
                        break;
                    case 4:
                        programBezi = false;
                        break;
                }
            }
        }
    }
    class ZobrazeniNabidky
    {
        public int ZobrazNabidku()
        {
            int velikostNabidky = 5;
            int vyberPolozky = 0;
            bool vyberDokoncen = false;
            string[] polozkyNabidky = new string[velikostNabidky];
            polozkyNabidky[0] = "Ceaserova šifra";
            polozkyNabidky[1] = "Vigenerova šifra";
            polozkyNabidky[2] = "Morseovka";
            polozkyNabidky[velikostNabidky-2] = "Mobilní šifra";
            polozkyNabidky[velikostNabidky-1] = "Konec";

            while (!vyberDokoncen)
            {
                for (int I = 0; I < velikostNabidky; I++)
                {
                    if (vyberPolozky == I)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(polozkyNabidky[I]);
                    Console.ResetColor();
                }
                ConsoleKeyInfo stisknutaKlavesa = Console.ReadKey(true);
                if (stisknutaKlavesa.Key == ConsoleKey.DownArrow)
                {
                    vyberPolozky++;
                    if (vyberPolozky == polozkyNabidky.Length)
                    {
                        vyberPolozky = 0;
                    }
                }
                else if (stisknutaKlavesa.Key == ConsoleKey.UpArrow)
                {
                    vyberPolozky--;
                    if (vyberPolozky < 0)
                    {
                        vyberPolozky = polozkyNabidky.Length - 1;
                    }
                }
                else if (stisknutaKlavesa.Key == ConsoleKey.Enter)
                {
                    vyberDokoncen = true;
                }
                Console.Clear();
            }
            return vyberPolozky;
        }
        public int ZobrazDruhouNabidku()
        {
            int velikostNabidky = 4;
            int vyberPolozky = 0;
            bool vyberDokoncen = false;
            string[] polozkyNabidky = new string[velikostNabidky];
            polozkyNabidky[0] = "Šifrovat";
            polozkyNabidky[1] = "Dešifrovat s klíčem";
            polozkyNabidky[2] = "Dešifrovat bez klíče";
            polozkyNabidky[3] = "Zpět";
            while (!vyberDokoncen)
            {

                for (int I = 0; I < velikostNabidky; I++)
                {
                    if (vyberPolozky == I)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(polozkyNabidky[I]);
                    Console.ResetColor();
                }
                ConsoleKeyInfo stisknutaKlavesa = Console.ReadKey(true);
                if (stisknutaKlavesa.Key == ConsoleKey.DownArrow)
                {
                    vyberPolozky++;
                    if (vyberPolozky == polozkyNabidky.Length)
                    {
                        vyberPolozky = 0;
                    }

                }
                else if (stisknutaKlavesa.Key == ConsoleKey.UpArrow)
                {
                    vyberPolozky--;
                    if (vyberPolozky < 0)
                    {
                        vyberPolozky = polozkyNabidky.Length - 1;
                    }
                }
                else if (stisknutaKlavesa.Key == ConsoleKey.Enter)
                {
                    vyberDokoncen = true;
                }
                Console.Clear();
            }
            return vyberPolozky;
        }
        public int ZobrazTretiNabidku()
        {
            int velikostNabidky = 2;
            int vyberPolozky = 0;
            bool vyberDokoncen = false;
            string[] polozkyNabidky = new string[velikostNabidky];
            polozkyNabidky[0] = "Znám délku klíče";
            polozkyNabidky[1] = "Neznám délku klíče";
            while (!vyberDokoncen)
            {

                for (int I = 0; I < velikostNabidky; I++)
                {
                    if (vyberPolozky == I)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(polozkyNabidky[I]);
                    Console.ResetColor();
                }
                ConsoleKeyInfo stisknutaKlavesa = Console.ReadKey(true);
                if (stisknutaKlavesa.Key == ConsoleKey.DownArrow)
                {
                    vyberPolozky++;
                    if (vyberPolozky == polozkyNabidky.Length)
                    {
                        vyberPolozky = 0;
                    }

                }
                else if (stisknutaKlavesa.Key == ConsoleKey.UpArrow)
                {
                    vyberPolozky--;
                    if (vyberPolozky < 0)
                    {
                        vyberPolozky = polozkyNabidky.Length - 1;
                    }
                }
                else if (stisknutaKlavesa.Key == ConsoleKey.Enter)
                {
                    vyberDokoncen = true;
                }
                Console.Clear();
            }
            return vyberPolozky;
        }

        public int ZobrazMorseovuNabidku()
        {
            int velikostNabidky = 3;
            int vyberPolozky = 0;
            bool vyberDokoncen = false;
            string[] polozkyNabidky = new string[velikostNabidky];
            polozkyNabidky[0] = "Zadat text";
            polozkyNabidky[1] = "Zadat morseovku";
            polozkyNabidky[2] = "Zpět";
            while (!vyberDokoncen)
            {

                for (int I = 0; I < velikostNabidky; I++)
                {
                    if (vyberPolozky == I)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(polozkyNabidky[I]);
                    Console.ResetColor();
                }
                ConsoleKeyInfo stisknutaKlavesa = Console.ReadKey(true);
                if (stisknutaKlavesa.Key == ConsoleKey.DownArrow)
                {
                    vyberPolozky++;
                    if (vyberPolozky == polozkyNabidky.Length)
                    {
                        vyberPolozky = 0;
                    }

                }
                else if (stisknutaKlavesa.Key == ConsoleKey.UpArrow)
                {
                    vyberPolozky--;
                    if (vyberPolozky < 0)
                    {
                        vyberPolozky = polozkyNabidky.Length - 1;
                    }
                }
                else if (stisknutaKlavesa.Key == ConsoleKey.Enter)
                {
                    vyberDokoncen = true;
                }
                Console.Clear();
            }
            return vyberPolozky;
        }
    }
    class CSifry
    {
        public void SKlicem()
        {
            string sifra;
            string text = "Text: ";
            char pismeno;
            int delkaSifry;
            int posun;
            int cisloZnaku;
            int klic;
            Console.WriteLine("Zadejte sifru: ");
            sifra = Console.ReadLine();
            Console.WriteLine("Zadejte klic (1-25): ");
            klic = Convert.ToInt16(Console.ReadLine());
            delkaSifry = sifra.Length;
            char[] poleSifra = new char[delkaSifry];

            for (int I = 0; I < delkaSifry; I++)
            {
                poleSifra[I] = sifra[I];
                cisloZnaku = sifra[I];
                if (poleSifra[I] == ' ')
                {
                    posun = 32;
                }
                else
                {
                    posun = cisloZnaku + klic;
                }

                if (posun > 122)
                {
                    posun = posun - 26;
                }
                pismeno = Convert.ToChar(posun);
                text += pismeno;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text.ToUpperInvariant());
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        public void BezKlice()
        {
            string vsifra;
            string sifra;
            string puvodniText = " pozice: ";
            string text = " pozice: ";
            char pismeno;
            int delkaSifry;
            int posun;
            int cisloZnaku;
            Console.WriteLine("Zadejte sifru: ");
            vsifra = Console.ReadLine();
            sifra = vsifra.ToLowerInvariant();
            Console.WriteLine(sifra);
            delkaSifry = sifra.Length;
            char[] poleSifra = new char[delkaSifry];
            for (int X = 0; X < 26; X++)
            {
                text = puvodniText;
                for (int I = 0; I < delkaSifry; I++)
                {
                    poleSifra[I] = sifra[I];
                    cisloZnaku = sifra[I];
                    if (poleSifra[I] == ' ')
                    {
                        posun = 32;
                    }
                    else
                    {
                        posun = cisloZnaku + X;
                    }

                    if (posun > 122)
                    {
                        posun = posun - 26;
                    }
                    pismeno = Convert.ToChar(posun);
                    text += pismeno;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(X + text.ToUpperInvariant());
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        public void Sifrovani()
        {
            string sifra;
            string text = "Sifra: ";
            char pismeno;
            int delkaSifry;
            int posun;
            int cisloZnaku;
            int klic;
            Console.WriteLine("Zadejte text: ");
            sifra = Console.ReadLine();
            Console.WriteLine("Zadejte klic (1-25): ");
            klic = Convert.ToInt16(Console.ReadLine());
            delkaSifry = sifra.Length;
            char[] poleSifra = new char[delkaSifry];

            for (int I = 0; I < delkaSifry; I++)
            {
                poleSifra[I] = sifra[I];
                cisloZnaku = sifra[I];
                if (poleSifra[I] == ' ')
                {
                    posun = 32;
                }
                else
                {
                    posun = cisloZnaku + klic;
                }

                if (posun > 122)
                {
                    posun = posun - 26;
                }
                pismeno = Convert.ToChar(posun);
                text += pismeno;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text.ToUpperInvariant());
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

    }
    class VSifry
    {
        public void SKlicem()
        {
            string pismeno;
            string klic;
            string dzaver = "Text je: ";
            int cisloP;
            int cisloS;
            int vypocet;
            int posun;
            int mezera;
            char vysledek;
            string sifra;
            int delkaKlice;
            int delkaSifry;

            Console.Clear();
            Console.WriteLine("Zadejte zašifrovaný text: ");
            sifra = Console.ReadLine();
            Console.WriteLine("Zadejte klic (nesmí obsahovat mezery)");
            klic = Console.ReadLine();

            delkaSifry = sifra.Length;
            delkaKlice = klic.Length;

            while (delkaSifry > delkaKlice)
            {
                klic = klic + klic;
                delkaKlice = klic.Length;
            }
            mezera = -1;
            char[] dsifraKlic = new char[delkaKlice];
            char[] dsifrovanePismeno = new char[delkaSifry];
            for (int i = 0; i < delkaSifry; i++)
            {
                mezera++;
                dsifrovanePismeno[i] = sifra[i];
                dsifraKlic[mezera] = klic[mezera];
                if (dsifrovanePismeno[i] == ' ')
                {
                    Console.WriteLine("mezera");
                    mezera -= 1;
                    continue;

                }
                Console.WriteLine(dsifrovanePismeno[i] + " - " + dsifraKlic[mezera]);
                cisloP = dsifrovanePismeno[i];
                cisloS = dsifraKlic[mezera];
                Console.WriteLine(cisloP + " - " + cisloS);
                posun = cisloS - 97;
                vypocet = cisloP - posun;
                if (vypocet < 97)
                {
                    vypocet = vypocet + 26;
                }
                vysledek = Convert.ToChar(vypocet);
                Console.WriteLine(vysledek);
                pismeno = Convert.ToString(vysledek);
                dzaver = dzaver + pismeno;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(dzaver.ToUpperInvariant());
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        public void BezKlice()
        {
            int volba;
            int delkaKlice;
            int delkaSifry;
            string sifra;
            ZobrazeniNabidky nabidka = new ZobrazeniNabidky();
            Console.Clear();
            volba = nabidka.ZobrazTretiNabidku();
            switch (volba)
            {
                case 0:
                    Console.WriteLine("Zadejte delku klíče: ");
                    delkaKlice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Zadejte šifru: ");
                    sifra = Console.ReadLine();

                    delkaSifry = sifra.Length;

                    
                    break;
                case 1:
                    break;
            }

        }

        public void Sifrovani()
        {
            string pismeno;
            string klic;
            string text;
            string zaver = "Sifra je: ";
            int cisloP;
            int cisloS;
            int vypocet;
            int posun;
            int mezera;
            char vysledek;
            int delkaTextu;
            int delkaKlice;

            Console.Clear();
            Console.WriteLine("Zadejte otevřený text (může obsahovat mezery)");
            text = Console.ReadLine();
            Console.WriteLine("Zadejte klic (nesmí obsahovat mezery)");
            klic = Console.ReadLine();

            delkaTextu = text.Length;
            delkaKlice = klic.Length;

            while (delkaTextu > delkaKlice)
            {
                klic = klic + klic;
                delkaKlice = klic.Length;
            }

            char[] sifraKlic = new char[delkaKlice];
            char[] sifrovanePismeno = new char[delkaTextu];
            mezera = -1;
            for (int i = 0; i < delkaTextu; i++)
            {
                mezera++;
                sifrovanePismeno[i] = text[i];
                sifraKlic[mezera] = klic[mezera];
                if (sifrovanePismeno[i] == ' ')
                {
                    Console.WriteLine("mezera");
                    mezera -= 1;
                    continue;

                }
                Console.WriteLine(sifrovanePismeno[i] + " - " + sifraKlic[mezera]);
                cisloP = sifrovanePismeno[i];
                cisloS = sifraKlic[mezera];
                Console.WriteLine(cisloP + " - " + cisloS);
                posun = cisloS - 97;
                vypocet = posun + cisloP;
                if (vypocet > 122)
                {
                    vypocet = vypocet - 26;
                }
                vysledek = Convert.ToChar(vypocet);
                Console.WriteLine(vysledek);
                pismeno = Convert.ToString(vysledek);
                zaver = zaver + pismeno;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(zaver.ToUpperInvariant());
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }
    }
    class Morse
    {
        public void Text()
        {
            char pismeno;
            string morseovka = "Morseovka: ";
            string morseovkaPismeno;
            Console.WriteLine("Zadejte text: ");
            string text = Console.ReadLine();
            int delkaTextu = text.Length;
            for (int i = 0; i < delkaTextu; i++)
            {
                pismeno = text[i];
                switch (pismeno)
                {
                    case 'a':
                        morseovkaPismeno = ".- ";
                        morseovka = morseovka + morseovkaPismeno;
                        break;
                    case 'b':
                        morseovkaPismeno = "-... ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'c':
                        morseovkaPismeno = "-.-. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'd':
                        morseovkaPismeno = "-.. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'e':
                        morseovkaPismeno = ". ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'f':
                        morseovkaPismeno = "..-. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'g':
                        morseovkaPismeno = "--. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'h':
                        morseovkaPismeno = ".... ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'i':
                        morseovkaPismeno = ".. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'j':
                        morseovkaPismeno = ".--- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'k':
                        morseovkaPismeno = "-.- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'l':
                        morseovkaPismeno = ".-.. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'm':
                        morseovkaPismeno = "-- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'n':
                        morseovkaPismeno = "-. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'o':
                        morseovkaPismeno = "--- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'p':
                        morseovkaPismeno = ".--. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'q':
                        morseovkaPismeno = "--.- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'r':
                        morseovkaPismeno = ".-. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 's':
                        morseovkaPismeno = "... ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 't':
                        morseovkaPismeno = "- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'u':
                        morseovkaPismeno = "..- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'v':
                        morseovkaPismeno = "...- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'w':
                        morseovkaPismeno = ".-- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'x':
                        morseovkaPismeno = "-..- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'y':
                        morseovkaPismeno = "-.-- ";
                        morseovka += morseovkaPismeno;
                        break;
                    case 'z':
                        morseovkaPismeno = "--.. ";
                        morseovka += morseovkaPismeno;
                        break;
                    case ' ':
                        morseovkaPismeno = "  ";
                        morseovka += morseovkaPismeno;
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(morseovka.ToUpperInvariant());
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
        public void Morseovka()
        {
            Console.WriteLine("Zadejte morseovku (jednotlivé pismena oddělujte mezerou): ");
            string morseovka = Console.ReadLine();
            string text = "Text: ";

            string abecedniZnaky = "abcdefghijklmnopqrstuvwxyz0123456789";
            string[] morseovyZnaky = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....",
            "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-",
            "...-", ".--", "-..-", "-.--", "--..","-----",".----","..---","...--","....-",".....","-....",
            "--...","---..","----."
            };


            string[] znaky = morseovka.Split(' ');

            foreach (string morseuvZnak in znaky)
            {
                char abecedniZnak = '?';
                int index = Array.IndexOf(morseovyZnaky, morseuvZnak);
                if (index >= 0)
                    abecedniZnak = abecedniZnaky[index];
                text += abecedniZnak;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text.ToUpperInvariant());
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
    }
    class Mob
{
    public void Text()
    {
        char pismeno;
        bool mezera;
        string vysledek = "Text: ";
        string posloupnost = "";
        Console.WriteLine("Zadejte cislo: ");
        string cislo = Console.ReadLine();
        int delkaTextu = cislo.Length;
        for (int i = 0; i < delkaTextu; i++)
        {
            posloupnost = "";
                Console.WriteLine("reset");
                pismeno = cislo[i];
                while (pismeno != ' ')
                {
                    posloupnost += pismeno;
                    Console.WriteLine("pismeno je " + pismeno);
                    Console.WriteLine("celek to je " + posloupnost);
                    if(i < delkaTextu-1)
                    {
                        i++;
                        pismeno = cislo[i];
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("finalni celek to je " + posloupnost);
                switch (posloupnost)
            {
                    case "0":
                        vysledek += " ";
                        break;
                    case "1":
                        vysledek += "_";
                        break;
                    case "2":
                        vysledek += "a";
                        break;
                    case "22":
                        vysledek += "b";
                        break;
                    case "222":
                        vysledek += "c";
                        break;
                    case "3":
                        vysledek += "d";
                        break;
                    case "33":
                        vysledek += "e";
                        break;
                    case "333":
                        vysledek += "f";
                        break;
                    case "4":
                        vysledek += "g";
                        break;
                    case "44":
                        vysledek += "h";
                        break;
                    case "444":
                        vysledek += "i";
                        break;
                    case "5":
                        vysledek += "j";
                        break;
                    case "55":
                        vysledek += "k";
                        break;
                    case "555":
                        vysledek += "l";
                        break;
                    case "6":
                        vysledek += "m";
                        break;
                    case "66":
                        vysledek += "n";
                        break;
                    case "666":
                        vysledek += "o";
                        break;
                    case "7":
                        vysledek += "p";
                        break;
                    case "77":
                        vysledek += "q";
                        break;
                    case "777":
                        vysledek += "r";
                        break;
                    case "7777":
                        vysledek += "s";
                        break;
                    case "8":
                        vysledek += "t";
                        break;
                    case "88":
                        vysledek += "u";
                        break;
                    case "888":
                        vysledek += "v";
                        break;
                    case "9":
                        vysledek += "w";
                        break;
                    case "99":
                        vysledek += "x";
                        break;
                    case "999":
                        vysledek += "y";
                        break;
                    case "9999":
                        vysledek += "z";
                        break;
                    default:
                        
                        break;
                }
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(vysledek.ToUpperInvariant());
        Console.ResetColor();
        Console.ReadKey();
        Console.Clear();
    }
}
}
