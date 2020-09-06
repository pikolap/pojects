using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turnaj
{
    class Postava
    {
        public string Jmeno;
        public int UtokZblizka = 0;
        public int Uhybani = 0;
        public int UtokZdalky = 0;
        public int Obrana = 0;
        public int Presnost = 0;
        public int Zivoty;



        public void vypisAtribut()
        {
            Console.WriteLine(Jmeno);
            Console.WriteLine("-----------------------");
            Console.WriteLine("Přesnost:"+Presnost);
            Console.WriteLine("Obrana:"+Obrana);
            Console.WriteLine("Útok zblízka:"+UtokZblizka);
            Console.WriteLine("Útok na dálku:"+UtokZdalky);
            Console.WriteLine("Uhybání:"+Uhybani);

        }
       
        public void zmenaDovednost(int zmena)
        {
            UtokZdalky += zmena;
            UtokZblizka += zmena;
            Uhybani += zmena;
            Obrana += zmena;
            Presnost += zmena;
        }
        public void nastavPostavu(string jmeno, int zivoty, int utokZblizka, int uhybani, int obrana, int presnost, int utokZdalky)
        {
            jmeno = Jmeno;
            zivoty = Zivoty;
            utokZblizka = UtokZblizka;
            utokZdalky = UtokZdalky;
            uhybani = Uhybani;
            obrana = Obrana;
            presnost = Presnost;
        }
        public Postava(string jmeno, int zivoty, int utokZblizka, int uhybani, int obrana, int presnost, int utokZdalky)
        {
            Jmeno = jmeno;
            Zivoty = zivoty;
            UtokZblizka = utokZblizka;
            UtokZdalky = utokZdalky;
            Uhybani = uhybani;
            Obrana = obrana;
            Presnost = presnost;
        }
        public int VypoctiUtok (int typUtoku) 
            {
                switch (typUtoku) 
                {
                    case 1: 
                        return  UtokZblizka + Presnost / 3;
                    case 2:
                        return UtokZdalky + Presnost /2;
                    default: return 0;
                }
            }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Postava nepritel1Postava = new Postava("Kostik",60,5,5,5,5,5);
            Postava nepritel2Postava = new Postava("Pavouk",40,15,5,5,5,15);
            Postava nepritel3Postava = new Postava("Skorpion",50,5,0,10,5,20);
            Postava nepritel4Postava = new Postava("Obr",100,15,0,15,10,15);
            Postava[] nepratele = new Postava[4];
            nepratele[0] = nepritel1Postava;
            nepratele[1] = nepritel2Postava;
            nepratele[2] = nepritel3Postava;
            nepratele[3] = nepritel4Postava;
            Postava hracovaPostava = new Postava("",100,0,0,0,0,0);
            Console.WriteLine("Vítej, zadej prosím jméno tvé postavy: ");
            hracovaPostava.Jmeno = Console.ReadLine();
            Console.WriteLine("Vytvořte postavu rozdělením 35 bodů mezi jednotlivé atributy");
            int body = 35;
            while (body > 0)
            {
                Console.Clear();
                hracovaPostava.vypisAtribut();
                Console.WriteLine("Zbývá rozdělit " + body + " bodů");
                Console.WriteLine("P pro přesnost");
                Console.WriteLine("O pro obranu");
                Console.WriteLine("Z pro útok zblízka");
                Console.WriteLine("D pro útok na dálku");
                Console.WriteLine("U pro uhýbání");
                switch (Console.ReadLine().ToUpper())
                {
                    case "P":
                        hracovaPostava.Presnost += 5;
                        body -= 5;
                        break;
                    case "O":
                        hracovaPostava.Obrana += 5;
                        body -= 5;
                        break;
                    case "Z":
                        hracovaPostava.UtokZblizka += 5;
                        body -= 5;
                        break;
                    case "D":
                        hracovaPostava.UtokZdalky += 5;
                        body -= 5;
                        break;
                    case "U":
                        hracovaPostava.Uhybani += 5;
                        body -= 5;
                        break;
                    default: Console.WriteLine("Špatně zadané písmeno");
                        break;
                }
            }
            Random nahodnaCisla = new Random();
            Console.WriteLine("Chcete zkusit štěstí? 'A'=ano 'N'=ne");
            if (Console.ReadLine().ToUpper() == "A")
            {
                hracovaPostava.zmenaDovednost(nahodnaCisla.Next(-2, 2));
                hracovaPostava.vypisAtribut();
                Console.ReadKey(false);
                Console.Clear();
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("Proti vám nastoupí: ");
            foreach (Postava jednotlivyNepritel in nepratele)
            {
                Console.WriteLine(jednotlivyNepritel.Jmeno + ": " + jednotlivyNepritel.Zivoty + " životů");
            }

            Console.WriteLine("Turnaj začíná!");
            Console.WriteLine("-----------------------");
            Console.ReadKey(false);
            Console.Clear();
            for (int I = 0; I < nepratele.Length; I++)
            {
                while (hracovaPostava.Zivoty > 0 && nepratele[I].Zivoty > 0)
                {
                    hracovaPostava.vypisAtribut();
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("PROTI");
                    nepratele[I].vypisAtribut();
                    string zadanyUtok;
                    Console.WriteLine("-----------------------");
                    do
                    {
                        Console.WriteLine("Útok zblízka = 'Z'");
                        Console.WriteLine("Útok zdálky = 'D'");
                        zadanyUtok = Console.ReadLine().ToUpper();

                    } while (zadanyUtok != "D" && zadanyUtok != "Z");

                    int silaUtoku = 0;
                    int obrana = 0;

                    switch (zadanyUtok)
                    {
                        case "Z":
                            silaUtoku = hracovaPostava.VypoctiUtok(1);
                            obrana = nepratele[I].Obrana;
                            break;
                        case "D":
                            silaUtoku = hracovaPostava.VypoctiUtok(2);
                            obrana = nepratele[I].Uhybani;
                            break;
                    }

                    if (obrana < silaUtoku)
                    {
                        Console.WriteLine("Způsobené poškození = " + (silaUtoku - obrana));
                        nepratele[I].Zivoty -= (silaUtoku - obrana);
                        Console.WriteLine("Nepřiteli zbývá " + nepratele[I].Zivoty + " životů");
                        Console.ReadKey(false);
                        Console.WriteLine("-----------------------");
                    }
                    else {
                        Console.WriteLine("Útok byl odražen");
                        Console.ReadKey(false);
                        Console.WriteLine("-----------------------");
                    }

                    if (nepratele[I].Zivoty <= 0)
                    {
                        Console.WriteLine(hracovaPostava.Jmeno + " porazil " + nepratele[I].Jmeno);
                        break;
                    }

                    int útokNepritele = nahodnaCisla.Next(1, 3);
                    silaUtoku = nepratele[I].VypoctiUtok(útokNepritele);

                    switch (útokNepritele)
                    {
                        case 1:
                            Console.WriteLine(nepratele[I].Jmeno + " útočí zblízka!");
                            obrana = hracovaPostava.Obrana;
                            break;
                        case 2:
                            Console.WriteLine(nepratele[I].Jmeno + " útočí zdálky!");
                            obrana =hracovaPostava.Uhybani;
                            break;
                    }   
                    
                    if (obrana < silaUtoku)
                    {
                        Console.WriteLine("Způsobené poškození = " + (silaUtoku - obrana));
                        hracovaPostava.Zivoty -= (silaUtoku - obrana);
                        Console.WriteLine("Zbývá životů: " + hracovaPostava.Zivoty);
                        Console.ReadKey(false);
                        Console.Clear();
                        if (hracovaPostava.Zivoty <= 0)
                        {
                            Console.WriteLine(nepratele[I].Jmeno + " porazil " + hracovaPostava.Jmeno);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Útok byl odražen");
                        Console.ReadKey(false);
                        Console.Clear();
                    }
                }
                if (hracovaPostava.Zivoty > 0)
                {
                    Console.WriteLine("Vyhrál jsi!");
                    Console.ReadKey(false);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Prohrál jsi!");
                    Console.ReadKey(false);
                    break;
                }

            }
            if (hracovaPostava.Zivoty > 0)
                {
                    Console.WriteLine("Vyhrál jsi celý turnaj!");
                    Console.ReadKey(false);
                }
        }
    }
}
