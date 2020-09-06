using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lod
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programBezi = true;
            bool souboj = true;
            while (programBezi)
            {
                int volba;
                ZobrazeniNabidky nabidka = new ZobrazeniNabidky();
                volba = nabidka.ZobrazNabidku();
                switch (volba)
                {
                    case 0:
                        Mapa svet = new Mapa();
                        Pocitac pc = new Pocitac();
                        svet.NastavMapu();
                        pc.NastavMapu();
                        int beh;
                        int behPc;
                        while (souboj)
                        {
                            svet.VykresliMapu();
                            pc.VykresliMapu();
                            svet.ZpracovaniPohybu();
                            pc.ZkrontrolujPole();
                            beh = svet.ZkontorujHru();
                            behPc = pc.ZkontorujHru();
                            if (beh == 0||behPc == 0)
                            {
                                souboj = false;
                            }
                            Console.ReadKey();
                        }
                        break;

                    case 1:
                        Console.Clear();
                        Console.WriteLine("Zničte nepřatelské lodě pomocí šipek a klávesy Enter. \nEsc = Předčasný konec hry");
                        Console.ReadKey();
                        break;

                    case 2:
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
            int velikostNabidky = 3;
            int vyberPolozky = 0;
            bool vyberDokoncen = false;
            string[] polozkyNabidky = new string[velikostNabidky];
            polozkyNabidky[0] = "Hra";
            polozkyNabidky[1] = "Jak hrát?";
            polozkyNabidky[2] = "Konec";

            Console.Clear();
            while (!vyberDokoncen)
            {
                for (int I = 0; I < velikostNabidky; I++)
                {
                    Console.SetCursorPosition(30, 10 + I);
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
    class Mapa
    {
        const int sirka = 10;
        const int vyska = 10;
        int lode = 0;
        int[,] mapa = new int[sirka, vyska];
        int pocetPokusu = 0;
        public void NastavMapu()
        {
            bool lodNeNastavena = true;
            int letLod = 4;
            int nadbytek = 0;
            Random nahodnaCisla = new Random();

            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska; y++)
                {
                    mapa[x, y] = 0;
                }
            }

            // letadlová loď
            int nx = nahodnaCisla.Next(0, sirka);
            int ny = nahodnaCisla.Next(0, vyska);
            int rotace = nahodnaCisla.Next(0, 4);

            switch (rotace)
            {
                case 0:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (nx + i <= 7)
                        {
                            mapa[nx + i, ny] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx - nadbytek, ny] = 1;
                        }
                    }
                    break;
                case 1:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (ny + i <= 7)
                        {
                            mapa[nx, ny + i] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx, ny - nadbytek] = 1;
                        }
                    }
                    break;
                case 2:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (nx - i >= 0)
                        {
                            mapa[nx - i, ny] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx + nadbytek, ny] = 1;
                        }
                    }
                    break;
                case 3:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (ny - i >= 0)
                        {
                            mapa[nx, ny - i] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx, ny + nadbytek] = 1;
                        }
                    }
                    break;
            }
            // torpedoborec typ 2
            int torpedoborec = 3;
            while(lodNeNastavena){
            nx = nahodnaCisla.Next(2, sirka - 2);
            ny = nahodnaCisla.Next(2, vyska - 2);
            rotace = nahodnaCisla.Next(0, 4);
            if (UmisteniLode(nx, ny, 2))
            {
                lodNeNastavena = false;
                switch (rotace)
                {
                    case 0:
                        mapa[nx, ny] = 1;
                        for (int i = 1; i < torpedoborec; i++)
                        {
                            if (nx + i <= 7)
                            {
                                mapa[nx + i, ny] = 1;
                            }
                            else
                            {
                                nadbytek++;
                                mapa[nx - nadbytek, ny] = 1;
                            }
                        }
                        break;
                    case 1:
                        mapa[nx, ny] = 1;
                        for (int i = 1; i < torpedoborec; i++)
                        {
                            if (ny + i <= 7)
                            {
                                mapa[nx, ny + i] = 1;
                            }
                            else
                            {
                                nadbytek++;
                                mapa[nx, ny - nadbytek] = 1;
                            }
                        }
                        break;
                    case 2:
                        mapa[nx, ny] = 1;
                        for (int i = 1; i < torpedoborec; i++)
                        {
                            if (nx - i >= 0)
                            {
                                mapa[nx - i, ny] = 1;
                            }
                            else
                            {
                                nadbytek++;
                                mapa[nx + nadbytek, ny] = 1;
                            }
                        }
                        break;
                    case 3:
                        mapa[nx, ny] = 1;
                        for (int i = 1; i < torpedoborec; i++)
                        {
                            if (ny - i >= 0)
                            {
                                mapa[nx, ny - i] = 1;
                            }
                            else
                            {
                                nadbytek++;
                                mapa[nx, ny + nadbytek] = 1;
                            }
                        }
                        break;
                }
            }
            }

        // křižník typ 1
        lodNeNastavena=true;
            while(lodNeNastavena){
            nx = nahodnaCisla.Next(2, sirka - 2);
            ny = nahodnaCisla.Next(2, vyska - 2);
            if (UmisteniLode(nx,ny,1))
            {
                lodNeNastavena=false;
                mapa[nx, ny] = 1;
                mapa[nx + 1, ny] = 1;
                mapa[nx - 1, ny] = 1;
                mapa[nx, ny + 1] = 1;
                mapa[nx, ny - 1] = 1;
            }
            }
        // dělový člun typ 0

            lodNeNastavena = true;
            while(lodNeNastavena){
            nx = nahodnaCisla.Next(1, sirka - 1);
            ny = nahodnaCisla.Next(1, vyska - 1);
            if (UmisteniLode(nx, ny, 0))
            {
                lodNeNastavena = false;
                mapa[nx, ny] = 1;
            }
            }

                      
            Console.SetCursorPosition(15, 0);
            Console.WriteLine("Zbývá zasahnout: 13");
            Console.SetCursorPosition(15, 1);
            Console.WriteLine("Počet pokusů: 0");
        }
        public void VykresliMapu()
        {
            Console.SetCursorPosition(15, 2);
            Console.WriteLine("             ");
            Console.SetCursorPosition(15, 3);
            Console.WriteLine("             ");
            lode = 0;
            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska; y++)
                {
                    if (mapa[x, y] == 0 || mapa[x,y] == 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('#');
                        Console.ResetColor();
                        if (mapa[x, y] == 1)
                        {
                            lode++;
                        }
                    }

                    else if (mapa[x, y] == 2)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine('@');
                        Console.ResetColor();
                    }
                    else if (mapa[x, y] == 3)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine('?');
                        Console.ResetColor();
                    }
                    else if (mapa[x, y] == 4)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine('X');
                        Console.ResetColor();
                    }
                }
            }
        }

        public void ZkrontrolujPole(int x, int y)
        {
            pocetPokusu++;
            if (mapa[x, y] == 1)
            {
                Console.SetCursorPosition(15, 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Zasah");
                Console.ResetColor();
                mapa[x, y] = 2;
            }
            else if (mapa[x, y] == 2 || mapa[x,y] == 4)
            {
                Console.SetCursorPosition(15, 2);
                Console.WriteLine("Již vypáleno");
            }
            else
            {
                Console.SetCursorPosition(15, 2);
                Console.WriteLine("Vedle");
                mapa[x, y] = 4;
            }
            Console.SetCursorPosition(15, 0);
            Console.WriteLine("Zbývá zasahnout: " + lode);
            Console.SetCursorPosition(15, 1);
            Console.WriteLine("Počet pokusů: " + pocetPokusu);
            if (lode < 10)
            {
                Console.SetCursorPosition(33, 0);
                Console.WriteLine(" ");
            }
        }

        public int ZkontorujHru()
        {
            if (lode == 0)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Zničili jste nepřítele!\nVýsledek byl zapsán do souboru 'zaznam.txt'.");
                using (StreamWriter sw = new StreamWriter(@"zaznam.txt", true))
                {
                    sw.WriteLine("Zničili jste nepřítele na "+pocetPokusu+" pokusů.\n");
                    sw.Flush();
                }
                return 0;
            }
            else 
            {
                return 1;
            }
        }
        public void ZpracovaniPohybu()
        {
            int x = 4;
            int y = 4;
            int pamet = mapa[x, y];
            bool vyber= true;
            while (vyber)
            {
                Console.SetCursorPosition(15, 3);
                ConsoleKeyInfo stisknutaKlavesa = Console.ReadKey(true);
                switch (stisknutaKlavesa.Key)
                {
                    case ConsoleKey.DownArrow:
                        mapa[x, y] = pamet;
                        y++;
                        if (y == vyska)
                        {
                            y = 0;
                        }
                        pamet = mapa[x, y];
                        mapa[x, y] = 3;
                        VykresliMapu();
                        break;
                    case ConsoleKey.UpArrow:
                        mapa[x, y] = pamet;
                        y--;
                        if (y < 0)
                        {
                            y = vyska-1;
                        }
                        pamet = mapa[x, y];
                        mapa[x, y] = 3;
                        VykresliMapu();
                        break;
                    case ConsoleKey.LeftArrow:
                        mapa[x, y] = pamet;
                        x--;
                        if (x < 0)
                        {
                            x = sirka-1;
                        }
                        pamet = mapa[x, y];
                        mapa[x, y] = 3;
                        VykresliMapu();
                        break;
                    case ConsoleKey.RightArrow:
                        mapa[x, y] = pamet;
                        x++;
                        if (x == sirka)
                        {
                            x = 0;
                        }
                        pamet = mapa[x, y];
                        mapa[x, y] = 3;
                        VykresliMapu();
                        break;
                    case ConsoleKey.Enter:
                        vyber = false;
                        mapa[x, y] = pamet;
                        ZkrontrolujPole(x, y);
                        break;
                    case ConsoleKey.Escape:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
        public bool UmisteniLode(int nx, int ny,int typ)
        {
            switch (typ)
            {
                case 0:
                    if (mapa[nx, ny] == 0 && mapa[nx + 1, ny] == 0 && mapa[nx - 1, ny] == 0 && mapa[nx, ny + 1] == 0 && mapa[nx, ny - 1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 1:

                    if (mapa[nx, ny] == 0 && mapa[nx + 1, ny] == 0 && mapa[nx - 1, ny] == 0 && mapa[nx, ny + 1] == 0 && mapa[nx, ny - 1] == 0
                        && mapa[nx + 2, ny] == 0 && mapa[nx - 2, ny] == 0 && mapa[nx, ny + 2] == 0 && mapa[nx, ny - 2] == 0
                && mapa[nx - 1, ny - 1] == 0 && mapa[nx + 1, ny + 1] == 0 && mapa[nx + 1, ny - 1] == 0 && mapa[nx - 1, ny + 1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:

                    if (mapa[nx, ny] == 0 && mapa[nx + 1, ny] == 0 && mapa[nx - 1, ny] == 0 && mapa[nx + 2, ny] == 0 && mapa[nx - 2, ny] == 0
                && mapa[nx, ny + 1] == 0 && mapa[nx, ny - 1] == 0 && mapa[nx, ny + 2] == 0 && mapa[nx, ny - 2] == 0
                && mapa[nx + 1, ny + 1] == 0 && mapa[nx + 1, ny - 1] == 0 && mapa[nx + 1, ny + 1] == 0 && mapa[nx - 1, ny - 1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }
    }
    class Pocitac
    {
        int px=1;
        int py=1;
        bool neZvoleno = true;
        int pocitadlo = 0;
        const int sirka = 10;
        const int vyska = 10;
        const int posun = 38;
        int lode = 0;
        int[,] mapa = new int[sirka, vyska];
        public void NastavMapu()
        {
            int letLod = 4;
            int nadbytek = 0;
            bool lodNeNastavena = true;
            Random nahodnaCisla = new Random();

            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska; y++)
                {
                    mapa[x, y] = 0;
                }
            }

            // letadlová loď
            int nx = nahodnaCisla.Next(0, sirka);
            int ny = nahodnaCisla.Next(0, vyska);
            int rotace = nahodnaCisla.Next(0, 4);

            switch (rotace)
            {
                case 0:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (nx + i <= 7)
                        {
                            mapa[nx + i, ny] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx - nadbytek, ny] = 1;
                        }
                    }
                    break;
                case 1:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (ny + i <= 7)
                        {
                            mapa[nx, ny + i] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx, ny - nadbytek] = 1;
                        }
                    }
                    break;
                case 2:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (nx - i >= 0)
                        {
                            mapa[nx - i, ny] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx + nadbytek, ny] = 1;
                        }
                    }
                    break;
                case 3:
                    mapa[nx, ny] = 1;
                    for (int i = 1; i < letLod; i++)
                    {
                        if (ny - i >= 0)
                        {
                            mapa[nx, ny - i] = 1;
                        }
                        else
                        {
                            nadbytek++;
                            mapa[nx, ny + nadbytek] = 1;
                        }
                    }
                    break;
            }
            int torpedoborec = 3;
            while (lodNeNastavena)
            {
                nx = nahodnaCisla.Next(2, sirka - 2);
                ny = nahodnaCisla.Next(2, vyska - 2);
                rotace = nahodnaCisla.Next(0, 4);
                if (UmisteniLode(nx, ny, 2))
                {
                    lodNeNastavena = false;
                    switch (rotace)
                    {
                        case 0:
                            mapa[nx, ny] = 1;
                            for (int i = 1; i < torpedoborec; i++)
                            {
                                if (nx + i <= 7)
                                {
                                    mapa[nx + i, ny] = 1;
                                }
                                else
                                {
                                    nadbytek++;
                                    mapa[nx - nadbytek, ny] = 1;
                                }
                            }
                            break;
                        case 1:
                            mapa[nx, ny] = 1;
                            for (int i = 1; i < torpedoborec; i++)
                            {
                                if (ny + i <= 7)
                                {
                                    mapa[nx, ny + i] = 1;
                                }
                                else
                                {
                                    nadbytek++;
                                    mapa[nx, ny - nadbytek] = 1;
                                }
                            }
                            break;
                        case 2:
                            mapa[nx, ny] = 1;
                            for (int i = 1; i < torpedoborec; i++)
                            {
                                if (nx - i >= 0)
                                {
                                    mapa[nx - i, ny] = 1;
                                }
                                else
                                {
                                    nadbytek++;
                                    mapa[nx + nadbytek, ny] = 1;
                                }
                            }
                            break;
                        case 3:
                            mapa[nx, ny] = 1;
                            for (int i = 1; i < torpedoborec; i++)
                            {
                                if (ny - i >= 0)
                                {
                                    mapa[nx, ny - i] = 1;
                                }
                                else
                                {
                                    nadbytek++;
                                    mapa[nx, ny + nadbytek] = 1;
                                }
                            }
                            break;
                    }
                }
            }

            // křižník typ 1
            lodNeNastavena = true;
            while (lodNeNastavena)
            {
                nx = nahodnaCisla.Next(2, sirka - 2);
                ny = nahodnaCisla.Next(2, vyska - 2);
                if (UmisteniLode(nx, ny, 1))
                {
                    lodNeNastavena = false;
                    mapa[nx, ny] = 1;
                    mapa[nx + 1, ny] = 1;
                    mapa[nx - 1, ny] = 1;
                    mapa[nx, ny + 1] = 1;
                    mapa[nx, ny - 1] = 1;
                }
            }
            // dělový člun typ 0

            lodNeNastavena = true;
            while (lodNeNastavena)
            {
                nx = nahodnaCisla.Next(1, sirka - 1);
                ny = nahodnaCisla.Next(1, vyska - 1);
                if (UmisteniLode(nx, ny, 0))
                {
                    lodNeNastavena = false;
                    mapa[nx, ny] = 1;
                }
            }
        }
        public void VykresliMapu()
        {
            lode = 0;
            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska; y++)
                {
                    if (mapa[x, y] == 0)
                    {
                        Console.SetCursorPosition(x+posun, y);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('#');
                        Console.ResetColor();
                    }

                    else if (mapa[x, y] == 2)
                    {
                        Console.SetCursorPosition(x+posun, y);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine('@');
                        Console.ResetColor();
                    }
                    else if (mapa[x, y] == 4)
                    {
                        Console.SetCursorPosition(x+posun, y);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine('X');
                        Console.ResetColor();
                    }
                    else if (mapa[x, y] == 1)
                    {
                        lode++;
                        Console.SetCursorPosition(x+posun, y);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine('&');
                        Console.ResetColor();
                    }
                }
            }
        }
        public void ZkrontrolujPole()
        {
            Random nahodnaCisla = new Random();
            int x=5;
            int y=5;
            bool novePole = false;
            
            while (!novePole)
            {
                if (pocitadlo == 0)
                {

                        x = nahodnaCisla.Next(0, sirka);
                        y = nahodnaCisla.Next(0, vyska);
                }
                else
                {
                    neZvoleno = true;
                    while (neZvoleno)
                    {
                        switch (pocitadlo)
                        {
                            case 1:
                                  y = py + 1;
                                  x = px;
                                if (y < vyska)
                                {
                                    neZvoleno = false;
                                }
                                pocitadlo--;
                                break;
                            case 2:
                                  y = py - 1;
                                  x = px;
                                if (y >= 0)
                                {
                                    neZvoleno = false;
                                }
                                pocitadlo--;
                                break;
                            case 3:
                                y = py;
                                  x = px - 1;
                                if (x >= 0)
                                {
                                    neZvoleno = false;
                                }
                                pocitadlo--;
                                break;
                            case 4:
                                x = px + 1;
                                y = py;
                                if (x < sirka)
                                {
                                    neZvoleno = false;
                                }
                                pocitadlo--;
                                break;
                        }
                    }
                }

                
                if (mapa[x, y] == 1)
                
                {
                    px = x;
                    py = y;
                    Console.SetCursorPosition(15, 3);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zasahli nás!");
                    Console.ResetColor();
                    mapa[x, y] = 2;
                    novePole = true;
                    pocitadlo = 4;

                }
                if (mapa[x, y] == 0)
                {
                    Console.SetCursorPosition(15, 3);
                    Console.WriteLine("Netrefili se!");
                    mapa[x, y] = 4;
                    novePole = true;
                }
            }
        }
        public int ZkontorujHru()
        {
            if (lode == 0)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Zničil vás nepřítel!");
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public bool UmisteniLode(int nx, int ny, int typ)
        {
            switch (typ)
            {
                case 0:
                    if (mapa[nx, ny] == 0 && mapa[nx + 1, ny] == 0 && mapa[nx - 1, ny] == 0 && mapa[nx, ny + 1] == 0 && mapa[nx, ny - 1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 1:

                    if (mapa[nx, ny] == 0 && mapa[nx + 1, ny] == 0 && mapa[nx - 1, ny] == 0 && mapa[nx, ny + 1] == 0 && mapa[nx, ny - 1] == 0
                        && mapa[nx + 2, ny] == 0 && mapa[nx - 2, ny] == 0 && mapa[nx, ny + 2] == 0 && mapa[nx, ny - 2] == 0
                && mapa[nx - 1, ny - 1] == 0 && mapa[nx + 1, ny + 1] == 0 && mapa[nx + 1, ny - 1] == 0 && mapa[nx - 1, ny + 1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:

                    if (mapa[nx, ny] == 0 && mapa[nx + 1, ny] == 0 && mapa[nx - 1, ny] == 0 && mapa[nx + 2, ny] == 0 && mapa[nx - 2, ny] == 0
                && mapa[nx, ny + 1] == 0 && mapa[nx, ny - 1] == 0 && mapa[nx, ny + 2] == 0 && mapa[nx, ny - 2] == 0
                && mapa[nx + 1, ny + 1] == 0 && mapa[nx + 1, ny - 1] == 0 && mapa[nx + 1, ny + 1] == 0 && mapa[nx - 1, ny - 1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;
        }
    }
}
