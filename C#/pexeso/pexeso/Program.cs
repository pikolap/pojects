using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pexeso
{
    class Program
    {
        static HerniObrazovka herniObrazovka;
        static void Main(string[] args)
        {
            Console.WriteLine("Vítejte ve hře\na = standartní hra\nc = konec");
            ConsoleKeyInfo Klavesa = Console.ReadKey(true);
            switch (Klavesa.Key)
            {
                case ConsoleKey.A:
                    herniObrazovka = new HerniObrazovka();
                    break;

                default:
                    return;
            }
            while (!herniObrazovka.vseOdhaleno())
            {
                herniObrazovka.VykresliPlochu();
                int x = -1, y = -1;
                do {
                    Console.WriteLine("zadejte souřadnice první hrací karty, každou na jeden řádek");
                    try
                    {
                        x = int.Parse(Console.ReadLine());
                        y = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Zadavejte jenom čísla");
                        Console.ResetColor();
                    }
                    catch (OverflowException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Číslo je mimo rozsah");
                        Console.ResetColor();
                    }
                    catch
                    {
                        Console.WriteLine("Nastala neočekávaná chyba");
                    }
                    if (!herniObrazovka.ValidniKarta(x, y))
                    {
                        Console.WriteLine("chybně zadané informace");

                    }
                }while(!herniObrazovka.ValidniKarta(x,y));
                herniObrazovka.OtocPrvniKartu(x, y);
                x = -1;
                y = -1;
                do
                {
                    Console.WriteLine("zadejte sořadnice druhé hrací karty, každou na jeden řádek");
                    x = int.Parse(Console.ReadLine());
                    y = int.Parse(Console.ReadLine());
                    if (!herniObrazovka.ValidniKarta(x, y))
                    {
                        Console.WriteLine("spatně zadené informce");
                    }
                } while (!herniObrazovka.ValidniKarta(x, y));
                herniObrazovka.OtocDruhouKartu(x,y);
            }
            Console.WriteLine("Vyhrál jsi!");
            Console.ReadKey();
        }
    }
    class HerniObrazovka
    {
        enum StavKarty
        {
            Skryta,Otocena,Odstranena
        }
        int OdhaleneKarty = 0, PocitadloTahu = 0;
        int rozmer = 4;
        char[,] HerniPole;
        StavKarty[,] PoleStavu;
        Char RubKarty = '#';
        Char LicKarty = 'A';
        Pozice OtocenaKarta;
        public HerniObrazovka()
        {
            HerniPole = new Char[rozmer, rozmer];
            PoleStavu = new StavKarty[rozmer, rozmer];
            RozdejKarty();
        }
        private void RozdejKarty()
        {
            ArrayList volnaPozice = new ArrayList();
            for (int y = 0; y < rozmer; y++)
            {
                for (int x = 0; x < rozmer; x++)
                {
                    Pozice pozice = new Pozice(x, y);
                    volnaPozice.Add(pozice);
                    PoleStavu[x,y] = StavKarty.Skryta;
                }
            }
            while (volnaPozice.Count >= 2)
            {
                Random generatorCisel = new Random();
                int cislo = generatorCisel.Next(volnaPozice.Count);
                Pozice prvniKarta = (Pozice)volnaPozice[cislo];
                volnaPozice.Remove(prvniKarta);
                cislo = generatorCisel.Next(volnaPozice.Count);
                Pozice druhaKarta = (Pozice)volnaPozice[cislo];
                volnaPozice.Remove(druhaKarta);
                HerniPole[prvniKarta.X, prvniKarta.Y] = LicKarty;
                HerniPole[druhaKarta.X, druhaKarta.Y] = LicKarty;
                LicKarty++;
            }
        }
        public void VykresliPlochu()
        {
            Console.Clear();
            for (int y = 0; y < rozmer; y++)
            {
                for (int x = 0; x < rozmer; x++)
                {
                    switch(PoleStavu[x,y]) 
                    {
                        case StavKarty.Skryta:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(RubKarty);
                            Console.ResetColor();
                            break;

                        case StavKarty.Odstranena:
                            Console.Write(" ");
                            break;
                        
                        case StavKarty.Otocena:
                            Console.Write(HerniPole[x,y]);
                            break;
                        
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Tahy: " + PocitadloTahu);
        }
        public bool vseOdhaleno()
        {
            return OdhaleneKarty == rozmer * rozmer;
        }
        public bool ValidniKarta(int x, int y)
        {
            return (x >= 0 && x < rozmer) && (y >= 0 && y < rozmer) && (PoleStavu[x, y] == StavKarty.Skryta);
        }
        public void OtocPrvniKartu(int x, int y)
        {
            OtocenaKarta = new Pozice(x, y);
            PoleStavu[x, y] = StavKarty.Otocena;
            PocitadloTahu++;
            VykresliPlochu();
        }
        public void OtocDruhouKartu(int x, int y)
        {
            PoleStavu[x, y] = StavKarty.Otocena;
            PocitadloTahu++;
            VykresliPlochu();
            Console.ReadKey();
            if (HerniPole[OtocenaKarta.X, OtocenaKarta.Y] == HerniPole[x, y])
            {
                PoleStavu[OtocenaKarta.X, OtocenaKarta.Y] = StavKarty.Odstranena;
                PoleStavu[x, y] = StavKarty.Odstranena;
                OdhaleneKarty += 2;
            }
            else
            {
                PoleStavu[OtocenaKarta.X, OtocenaKarta.Y] = StavKarty.Skryta;
                PoleStavu[x, y] = StavKarty.Skryta;
            }
        }
    }
    struct Pozice
    {
        private readonly int x;
        public int X
        {
            get
            {
                return x;
            }
        }
        private readonly int y;
        public int Y
        {
            get
            {
                return y;
            }
        }
        public Pozice(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
