using System;
using System.IO;
class MainClass
{
    static char[,] PretvaracTxtMapeUMatricu(string ImeFajla)
    {
      
        char[,] Mapa = new char[50, 200];
        for(int i = 0;i < Mapa.GetLength(0); i++) for(int j = 0;j < Mapa.GetLength(1); j++) Mapa[i,j] = '#';
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
      int sirina = 2* brKol + 1;
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
                else {Console.Write(brojevi[broj]);broj++;}
            else
                if (red == 0)
                    if (kol == 0) Console.Write(gLevi);    
                    else if (kol == sirina - 1) Console.Write(gDesni);
                    else if (kol % 2 == 0) Console.Write(gornji);
                    else { TablaZaBiranje(sirina2,hor);}        
                else if (red == visina - 1)
                    if (kol == 0) Console.Write(dLevi);      
                    else if (kol == sirina - 1) Console.Write(dDesni);     
                    else if (kol % 2 == 0) Console.Write(donji);      
                    else { TablaZaBiranje(sirina2,hor);}
                else
                    if (kol == 0) Console.Write(levi);    
                    else if (kol == sirina - 1) Console.Write(desni);     
                    else if (kol % 2 == 0) Console.Write(centralni); 
                    else { TablaZaBiranje(sirina2,hor);}      
      }
    }
    static void TablaZaBiranje(int sirina, char znak)
    {
      for (int i = 0;i < sirina; i++)
      {
        Console.Write(znak);
      }
    }
	  static int x = 5;
	  static int y = 4;
	  static int BrojIgraca;
	  static int kol = 0;
    static int kursorPolja = 18;
    static void IdiDesno(int a)
	  {
		  if (x < kursorPolja) { x += 4; kol++; }
	  }
	  static void IdiLevo()
	  {
		  if (x > 5) { x -= 4; kol--; }
	  }
	  static void IzaberiPolje(int a)
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
      if (x == 5) BrojIgraca = 2;
      if (x == 9) BrojIgraca = 3;
      if (x == 13) BrojIgraca = 4;
      if (x == 17) BrojIgraca = 5;
      if (x == 21) BrojIgraca = 6;
	  }
    static void BiranjeIgraca()
    {
      string[] brojevi={" 2 "," 3 "," 4 "," 5 "," 6 "};
      Nacrtaj(brojevi, 5, 2, 1, 3);
      IzaberiPolje(5);
      Console.SetCursorPosition(4, 9);
      Console.Clear();
      Console.WriteLine(BrojIgraca);
    }
    public static void Main(string[] args)
    {
        //Console.WriteLine("Izaberite broj igraca: ");
        //BiranjeIgraca();
        int[,] TestVrednosti = new int[4, 40];
        for (int i = 0; i < TestVrednosti.GetLength(1); i++) TestVrednosti[0, i] = i % 7;
        char[,] Mapa = PretvaracTxtMapeUMatricu("MAPA1");
        IspisMape(Mapa, TestVrednosti);
    }
}