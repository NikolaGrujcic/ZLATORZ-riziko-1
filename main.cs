using System;
using System.IO;
class MainClass
{

  //Promenljive:
    static int x = 110;
    static int y = 28;
    static int brojIgraca;
    static int brojMape;
    static int kol = 0;
    static int kursorPolja = 126;
    static int gornjaGranica = 110;
    static int brojTeritorija;
    static int trenutniIgrac;

    struct Kartica 
    {
      int KomePripada; //0 - nikome, 1 = prvom igracu itd.
      int Teritorija;
      int Tip; //0 - dzoker, 1 - pesadija, 2 - konjica, 3 - avion
    }


    static char[,] PretvaracTxtMapeUMatricu(string ImeFajla)
    {

        char[,] Mapa = new char[50, 200];
        for (int i = 0; i < Mapa.GetLength(0); i++) for (int j = 0; j < Mapa.GetLength(1); j++) Mapa[i, j] = '#';
        string linijaMape;
        StreamReader CitacMape = new StreamReader(ImeFajla);
        int brojReda = 0;
        while (!CitacMape.EndOfStream)
        {
            linijaMape = CitacMape.ReadLine();
            for (int i = 0; i < Mapa.GetLength(1); i++) Mapa[brojReda, i] = linijaMape[i];
            brojReda++;
        }
        CitacMape.Close();
        return Mapa;
    }

    static void IspisMape(char[,] Mapa, int[,] TestVrednosti)
    {
        for (int i = 0; i < Mapa.GetLength(0); i++)
        {
            Console.Beep();
            for (int j = 0; j < Mapa.GetLength(1); j++)
            {

                int broj = Mapa[i, j] - 'A';
                
                if (Mapa[i, j] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else if (Mapa[i, j] == '=')
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (TestVrednosti[0, broj] == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (TestVrednosti[0, broj] == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else if (TestVrednosti[0, broj] == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (TestVrednosti[0, broj] == 3)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (TestVrednosti[0, broj] == 4)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (TestVrednosti[0, broj] == 5)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (TestVrednosti[0, broj] == 6)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.Write(Mapa[i, j]);
            }
            Console.WriteLine();
        }
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }

    static void Nacrtaj(string[] brojevi, int brKol, int pocRed, int pocKol, int sirina2)
    {
        int visina = 3;
        int sirina = 2 * brKol + 1;
        char hor = '\u2500', ver = '\u2502';
        char gLevi = '\u250C', gDesni = '\u2510';
        char dLevi = '\u2514', dDesni = '\u2518';
        char levi = '\u251C', desni = '\u2524';
        char gornji = '\u252C', donji = '\u2534';
        char centralni = '\u253C';

        Console.SetCursorPosition(pocKol, pocRed);
        int broj = 0;

        pocRed++;
        Console.SetCursorPosition(pocKol, pocRed);
        for (int i = 0; i < visina; i++)
            if (i % 2 == 1)
            {
                Console.SetCursorPosition(pocKol, pocRed + i);
            }
        pocKol += 2;
        Console.SetCursorPosition(pocKol, pocRed);
        for (int red = 0; red < visina; red++)
        {
            Console.SetCursorPosition(pocKol, pocRed++);
            for (int kol = 0; kol < sirina; kol++)
                if (red % 2 == 1)
                    if (kol % 2 == 0) Console.Write(ver);
                    else { Console.Write(brojevi[broj]); broj++; }
                else
                    if (red == 0)
                    if (kol == 0) Console.Write(gLevi);
                    else if (kol == sirina - 1) Console.Write(gDesni);
                    else if (kol % 2 == 0) Console.Write(gornji);
                    else { TablaZaBiranje(sirina2, hor); }
                else if (red == visina - 1)
                    if (kol == 0) Console.Write(dLevi);
                    else if (kol == sirina - 1) Console.Write(dDesni);
                    else if (kol % 2 == 0) Console.Write(donji);
                    else { TablaZaBiranje(sirina2, hor); }
                else
                    if (kol == 0) Console.Write(levi);
                else if (kol == sirina - 1) Console.Write(desni);
                else if (kol % 2 == 0) Console.Write(centralni);
                else { TablaZaBiranje(sirina2, hor); }
        }
    }

    static void TablaZaBiranje(int sirina, char znak)
    {
        for (int i = 0; i < sirina; i++)
        {
            Console.Write(znak);
        }
    }
    
    static void IdiDesno(int a)
    {
        if (x < kursorPolja) { x += 4; kol++; }
    }

    static void IdiLevo()
    {
        if (x > gornjaGranica) { x -= 4; kol--; }
    }

    static void IzaberiBrojIgraca(int a)
    {
        ConsoleKeyInfo cki;
        Console.SetCursorPosition(x, y);
        do
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.LeftArrow) IdiLevo();
            else if (cki.Key == ConsoleKey.RightArrow) IdiDesno(a);
            Console.SetCursorPosition(x, y);
        } while (cki.Key != ConsoleKey.Enter);
        if (x == 110) brojIgraca = 2;
        if (x == 114) brojIgraca = 3;
        if (x == 118) brojIgraca = 4;
        if (x == 122) brojIgraca = 5;
        if (x == 126) brojIgraca = 6;
    }

    static void IzaberiMapu(int a)
    {
        ConsoleKeyInfo cki;
        x = 114;
        y = 34;
        gornjaGranica = 114;
        Console.SetCursorPosition(x, y);
        do
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.LeftArrow) IdiLevo();
            else if (cki.Key == ConsoleKey.RightArrow) IdiDesno(a);
            Console.SetCursorPosition(x, y);
        } while (cki.Key != ConsoleKey.Enter);
        if (x == 114) 
        {
          brojMape = 1;
          brojTeritorija = 27;
        }
        if (x == 118) ;
        {
          brojMape = 2
          brojTeritorija = 27;
        }
        if (x == 122) 
        {
          brojMape = 3;
          //brojTeritorija = NE ZNAMO JOS;
        }
    }

    static void BiranjeIgraca()
    {
        string[] brojevi = { " 2 ", " 3 ", " 4 ", " 5 ", " 6 " };
        Nacrtaj(brojevi, 5, 26, 106, 3);
        IzaberiBrojIgraca(5);
        Console.SetCursorPosition(4, 9);
    }

    static void BiranjeMape()
    {
      string[] mape = {" 1 " , " 2 " , " 3 "};
      Nacrtaj(mape, 3, 32, 110, 3);
      kursorPolja = 122;
      IzaberiMapu(3);
      Console.SetCursorPosition(4, 9);
      Console.Clear();
    }

    static void IspisiStatusIgreNaTabli(int[,] Vrednosti, string imeMape)
    {
        int[,] PozicijeStatusa = new int[2, Vrednosti.GetLength(1)];
        int bl = 0;
        StreamReader ucitajI = new StreamReader(imeMape+ "I");
        while (!ucitajI.EndOfStream)
        {
            string[] linija = ucitajI.ReadLine().Split();
            PozicijeStatusa[0, bl] = Convert.ToInt32(linija[0]);
            PozicijeStatusa[1, bl] = Convert.ToInt32(linija[1]);
            bl++;
        }
        ucitajI.Close();
        for (int i = 0; i < PozicijeStatusa.GetLength(1); i++)
        {
            if (Vrednosti[0, i] == 0)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (Vrednosti[0, i] == 1)
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (Vrednosti[0, i] == 2)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (Vrednosti[0, i] == 3)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (Vrednosti[0, i] == 4)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (Vrednosti[0, i] == 5)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (Vrednosti[0, i] == 6)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            Console.SetCursorPosition(PozicijeStatusa[0,i],PozicijeStatusa[1,i]);
            //Console.Write(Convert.ToChar(Convert.ToInt32('A') + i - ((i / 26) * 26)) + "" + (1 + (i / 26)) + ":");
            Console.Write(i < 9 ? "0" : "" + "");
            Console.Write(i + 1 + ":");
            Console.Write("\uD83D\uDC82:" + Vrednosti[1, i] + "\U0001f40e:" + Vrednosti[2, i] + "\U0001f6e9\uFE0F:" + Vrednosti[3, i]);

        }
    }

    static void ZameniVlasnika(char[,] Mapa, string imeOsvojeneTeritorije, int BrojNovogVlasnika)//Metoda koja na pozivu zameni boju teritorije koja se osvoji
    {
        if (BrojNovogVlasnika == 0)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else if (BrojNovogVlasnika == 1)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        else if (BrojNovogVlasnika == 2)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (BrojNovogVlasnika == 3)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }
        else if (BrojNovogVlasnika == 4)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        else if (BrojNovogVlasnika == 5)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        else if (BrojNovogVlasnika == 6)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        int AsciiRed=0;
        if(imeOsvojeneTeritorije[1]=='2')AsciiRed=26;
        for(int i=0;i<Mapa.GetLength(0);i++)
        {
            for (int j = 0; j < Mapa.GetLength(1); j++)
            {
                if(Convert.ToInt32(Mapa[i,j])==Convert.ToInt32('A')+AsciiRed)
                {
                  Console.SetCursorPosition(j,i);
                  Console.Write(" ");
                }
            }
        }
    }

    static void IspisiKockuPraznu(int SirinaKordinata, int VisinaKordinata, char boja)
        {
            if (boja == 'C')
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(SirinaKordinata, VisinaKordinata + i);
                Console.Write("              ");
            }
            Console.SetCursorPosition(0,0);
        }
        static void IspisiKockuJedan(int SirinaKordinata, int VisinaKordinata)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(SirinaKordinata + 6, VisinaKordinata + 3);
            Console.Write("##");
            Console.SetCursorPosition(0,0);

        }
        static void IspisiKockuDva(int SirinaKordinata, int VisinaKordinata)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(0,0);
        }
        static void IspisiKockuTri(int SirinaKordinata, int VisinaKordinata)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 6, VisinaKordinata + 3);
            Console.Write("##");
            Console.SetCursorPosition(0,0);
        }
        static void IspisiKockuCetiri(int SirinaKordinata, int VisinaKordinata)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(0,0);
        }
        static void IspisiKockuPet(int SirinaKordinata, int VisinaKordinata)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 6, VisinaKordinata + 3);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(0,0);
        }
        static void IspisiKockuSest(int SirinaKordinata, int VisinaKordinata)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 3);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 1);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 10, VisinaKordinata + 3);
            Console.Write("##");
            Console.SetCursorPosition(SirinaKordinata + 2, VisinaKordinata + 5);
            Console.Write("##");
            Console.SetCursorPosition(0,0);
        }
        static void IspisiKocke(int[] VrednostiNaKockama)
        {
            Random rand = new Random();
            int SirinaKordinata = 210;
            int VisinaKordinata = 3;
            for (int i = 0; i < 5; i++)
            {
                IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, i > 2 ? 'B' : 'C');
            }
            for (int i = 0; i < 5; i++)
            {
              System.Threading.Thread.Sleep(500);
                char boja = i > 2 ? 'B' : 'C';
                if (VrednostiNaKockama[i] == 0)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    continue;
                }
                else
                {
                    int proslinum = -1;
                    for (int j = 0; j < 12; j++)
                    {
                        int num = rand.Next(1, 6);
                        if(proslinum==num)
                        {
                            if (num == 6) num--;
                            else if (num == 1) num++;
                            else num--;
                        }
                        proslinum = num;
                        if (num == 1)
                        {
                            IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                            IspisiKockuJedan(SirinaKordinata, VisinaKordinata + i * 8);
                        }
                        else if (num == 2)
                        {
                            IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                            IspisiKockuDva(SirinaKordinata, VisinaKordinata + i * 8);
                        }
                        else if (num == 3)
                        {
                            IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                            IspisiKockuTri(SirinaKordinata, VisinaKordinata + i * 8);
                        }
                        else if (num == 4)
                        {
                            IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i* 8, boja);
                            IspisiKockuCetiri(SirinaKordinata, VisinaKordinata + i * 8);
                        }
                        else if (num == 5)
                        {
                            IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                            IspisiKockuPet(SirinaKordinata, VisinaKordinata + i * 8);
                        }
                        else if (num == 6)
                        {
                            IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                            IspisiKockuSest(SirinaKordinata, VisinaKordinata + i * 8);
                        }
                        Console.Beep();
                        System.Threading.Thread.Sleep(100+j * 20);
                    }
                }
                if (VrednostiNaKockama[i] == 1)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    IspisiKockuJedan(SirinaKordinata, VisinaKordinata + i * 8);
                }
                else if (VrednostiNaKockama[i] == 2)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    IspisiKockuDva(SirinaKordinata, VisinaKordinata + i * 8);
                }
                else if (VrednostiNaKockama[i] == 3)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    IspisiKockuTri(SirinaKordinata, VisinaKordinata + i * 8);
                }
                else if (VrednostiNaKockama[i] == 4)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    IspisiKockuCetiri(SirinaKordinata, VisinaKordinata + i * 8);
                }
                else if (VrednostiNaKockama[i] == 5)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    IspisiKockuPet(SirinaKordinata, VisinaKordinata + i * 8);
                }
                else if (VrednostiNaKockama[i] == 6)
                {
                    IspisiKockuPraznu(SirinaKordinata, VisinaKordinata + i * 8, boja);
                    IspisiKockuSest(SirinaKordinata, VisinaKordinata + i * 8);
                }
                Console.Beep();
            }
        }

        static int[] NasumicneKocke(int BC, int BP)//BC-Broj crvenih kocki, BP-Broj plavih kocki
        {
                  Random rand = new Random();
                  int[] VrednostiNaKockama = new int[5];
                  for(int i=0;i<VrednostiNaKockama.Length;i++)VrednostiNaKockama[i] = rand.Next(1, 6);
                  if(BC<=2)VrednostiNaKockama[2]=0;
                  if(BC==1)VrednostiNaKockama[1]=0;
                  if(BP==1)VrednostiNaKockama[4]=0;
                  return VrednostiNaKockama;
        }

        static void IspisiRizikoLogo(int SirinaKordinata, int VisinaKordinata)
        {
            char[,] RizikoLogo = {
            {'A','A','A','A','A','A',' ',' ','A','A',' ','A','A','A','A','A','A','A',' ','A','A',' ','A','A',' ',' ',' ','A','A',' ',' ','A','A','A','A','A',' '},
            {'A','A',' ',' ',' ','A','A',' ','A','A',' ',' ',' ',' ',' ',' ','A','A',' ','A','A',' ','A','A',' ',' ','A','A',' ',' ','A','A',' ',' ',' ','A','A' },
            {'A','A','A','A','A','A',' ',' ','A','A',' ',' ',' ','A','A','A',' ',' ',' ','A','A',' ','A','A','A','A','A',' ',' ',' ','A','A',' ',' ',' ','A','A' },
            {'A','A',' ',' ',' ','A','A',' ','A','A',' ','A','A',' ',' ',' ',' ',' ',' ','A','A',' ','A','A',' ',' ','A','A',' ',' ','A','A',' ',' ',' ','A','A'},
            {'A','A',' ',' ',' ','A','A',' ','A','A',' ','A','A','A','A','A','A','A',' ','A','A',' ','A','A',' ',' ',' ','A','A',' ',' ','A','A','A','A','A', ' ' }
            };
            for(int i=0;i<RizikoLogo.GetLength(0);i++)
            {
                Console.SetCursorPosition(SirinaKordinata, VisinaKordinata+i);
                for (int j = 0; j < RizikoLogo.GetLength(1); j++)
                {
                    if (RizikoLogo[i, j] == 'A')
                    {
                        Console.Write("\u2588");
                    }
                    else Console.Write(" ");
                }
            }
        }

        static Kartica[] PravljenjeKartica ()
        {
          Kartica[] sveKartice = new Kartica[brojTeritorija * 4];
          int _br = 0;
          for (int i = 0; i < brojTeritorija.Length; i++)
          {
            for (int j = 0; j < 4; j++)
            {
              sveKartice[_br].KomePripada = 0;
              sveKartice[_br].Teritorija = i;
              sveKartice[_br].Tip = j;
              _br++;
            }
          }
          return sveKartice;
        }

        static void RaspodelaTeritorija ()
        {
          
        }
        
        static bool DaLiMozeDaSeNapadne (string ter1, string ter2)
        {
          string imeMape;
          if(brojMape == 1) imeMape = "MAPA1IT";
          else if(brojMape == 2) imeMape = "MAPA2IT";
          else imeMape = "MAPA3IT";
          StreamReader proveraTeritorija = new StreamReader (imeMape);
          string niz;
          string[] teritorijeNiza;
          while(!proveraTeritorija.EndOfStream)
          {
            niz = proveraTeritorija.ReadLine();
            teritorijeNiza = niz.Split();
            if(teritorijeNiza[0] == ter1)
            {
              for (int i = 1;i < teritorijeNiza.Length; i++)
              {
                if(teritorijeNiza[i] == ter2) return true;
              }
            }
            else continue;
          }
          proveraTeritorija.Close();
          return false;
        }
static void NacrtajHorizontalnoPolje(string tekst, int left, int top)
        {
            int sirinaDugmeta = 26;
            char hor = '\u2500', ver = '\u2502';
            char gLevi = '\u250C', gDesni = '\u2510';
            char dLevi = '\u2514', dDesni = '\u2518';
            //gornji red:
            Console.SetCursorPosition(left, top);
            Console.Write(gLevi);
            for (int i = 0; i < sirinaDugmeta; i++) Console.Write(hor);
            Console.WriteLine(gDesni);
            //prazan red iznad srednjeg reda:
            Console.SetCursorPosition(left, top + 1);
            Console.Write(ver);
            Console.SetCursorPosition(left + sirinaDugmeta + 1, top + 1);
            Console.Write(ver);
            //srednji red:
            Console.SetCursorPosition(left, top + 2);
            Console.Write(ver);
            Console.SetCursorPosition(left + (sirinaDugmeta+2 - tekst.Length)/2,top+2);
            Console.Write(tekst);
            Console.SetCursorPosition(left + sirinaDugmeta+1, top + 2);
            Console.Write(ver);
            //prazan red ispod srednjeg reda:
            Console.SetCursorPosition(left, top + 3);
            Console.Write(ver);
            Console.SetCursorPosition(left + sirinaDugmeta + 1, top + 3);
            Console.Write(ver);
            //skroz donji red:
            Console.SetCursorPosition(left, top + 4);
            Console.Write(dLevi);
            for (int i = 0; i < sirinaDugmeta; i++) Console.Write(hor);
            Console.WriteLine(dDesni);
        }
        static void MainMenu()
        {
            int leftCordPrvog=106;
            int topCordPrvog = 24;
            int brojOpcije = 0;
            IspisiRizikoLogo(leftCordPrvog-6, topCordPrvog-8);
            ConsoleKeyInfo unetoDugme;
            NacrtajHorizontalnoPolje("Nova igra", leftCordPrvog, topCordPrvog);
            NacrtajHorizontalnoPolje("Učitaj igru", leftCordPrvog, topCordPrvog + 7);
            NacrtajHorizontalnoPolje("Napusti riziko", leftCordPrvog, topCordPrvog + 14);
            Console.ForegroundColor = ConsoleColor.Red;
            NacrtajHorizontalnoPolje("Nova igra", leftCordPrvog, topCordPrvog);
            Console.ResetColor();
            do
            {
                
                unetoDugme = Console.ReadKey(true);
                
                if(unetoDugme.Key==ConsoleKey.DownArrow && brojOpcije<2)
                {
                    Console.ResetColor();
                    NacrtajHorizontalnoPolje("Nova igra", leftCordPrvog, topCordPrvog);
                    NacrtajHorizontalnoPolje("Učitaj igru", leftCordPrvog, topCordPrvog + 7);
                    NacrtajHorizontalnoPolje("Napusti riziko", leftCordPrvog, topCordPrvog + 14);
                    Console.ForegroundColor = ConsoleColor.Red;
                    brojOpcije++;
                    if (brojOpcije == 1) NacrtajHorizontalnoPolje("Učitaj igru", leftCordPrvog, topCordPrvog + 7);
                    if (brojOpcije == 2) NacrtajHorizontalnoPolje("Napusti riziko", leftCordPrvog, topCordPrvog + 14);
                    Console.SetCursorPosition(0, 0);
                }
                if (unetoDugme.Key == ConsoleKey.UpArrow && brojOpcije >0)
                {
                    Console.ResetColor();
                    NacrtajHorizontalnoPolje("Nova igra", leftCordPrvog, topCordPrvog);
                    NacrtajHorizontalnoPolje("Učitaj igru", leftCordPrvog, topCordPrvog + 7);
                    NacrtajHorizontalnoPolje("Napusti riziko", leftCordPrvog, topCordPrvog + 14);
                    Console.ForegroundColor = ConsoleColor.Red;
                    brojOpcije--;
                    if (brojOpcije == 0) NacrtajHorizontalnoPolje("Nova igra", leftCordPrvog, topCordPrvog);
                    if (brojOpcije == 1) NacrtajHorizontalnoPolje("Učitaj igru", leftCordPrvog, topCordPrvog + 7);
                    Console.SetCursorPosition(0, 0);
                }
            } while (unetoDugme.Key!=ConsoleKey.Enter);
            return brojOpcije;
        }
    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("HAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALO");
        MainMenu();
        IspisiRizikoLogo(100,16);
        Console.SetCursorPosition(108, 25);
        Console.Write("Izaberite broj igraca: ");
        BiranjeIgraca();
        Console.SetCursorPosition(91, 31);
        Console.Write("Izaberite mapu (1 - svet, 2 - nzm, 3 - mapa iz GoT-a): ");
        BiranjeMape();
        string imeMape;
        if (brojMape == 1) imeMape = "MAPA1";
        else if (brojMape == 2) imeMape = "MAPA2";
        else imeMape = "MAPA3";
        int[,] Vrednosti = new int[4, 40];//cije, vojnici, konji, avioni
        for (int i = 0; i < Vrednosti.GetLength(1); i++) Vrednosti[0, i] = i % 7;
        char[,] Mapa = PretvaracTxtMapeUMatricu(imeMape);
        IspisMape(Mapa, Vrednosti);
        IspisiStatusIgreNaTabli(Vrednosti,imeMape);
        int[] VrednostiNaKockama = NasumicneKocke(2,1);
        //IspisiKocke(VrednostiNaKockama);
        ZameniVlasnika(Mapa,"A1",3);  
        
    }
}