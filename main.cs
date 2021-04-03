using System;
using System.IO;
class MainClass
{
    static char[,] PretvaracTxtMapeUMatricu(string ImeFajla)
    {
      
        char[,] Mapa = new char[50, 200];
        for(int i=0;i<Mapa.GetLength(0);i++)for(int j=0;j<Mapa.GetLength(1);j++)Mapa[i,j]='#';
        string linijaMape;
        StreamReader CitacMape = new StreamReader(ImeFajla);
        int brojReda = 0;
        while (!CitacMape.EndOfStream)
        {
            linijaMape = CitacMape.ReadLine();
            for (int i = 0; i < 199; i++) Mapa[brojReda, i] = linijaMape[i];
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
                else if (Mapa[i, j] == 'Z')
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
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (TestVrednosti[0, broj] == 2)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (TestVrednosti[0, broj] == 3)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
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
    public static void Main(string[] args)
    {
        int[,] TestVrednosti = new int[4, 30];
        for (int i = 0; i < TestVrednosti.GetLength(1); i++) TestVrednosti[0, i] = i % 7;
        char[,] Mapa = PretvaracTxtMapeUMatricu("MAPA3");
        IspisMape(Mapa, TestVrednosti);
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
}