using System;
using System.IO;
class MainClass
{
    static char[,] PretvaracTxtMapeUMatricu(string ImeFajla)
    {
        char[,] Mapa = new char[50, 250];
        string linijaMape;
        StreamReader CitacMape = new StreamReader(ImeFajla);
        int brojReda = 0;
        while (!CitacMape.EndOfStream)
        {
            linijaMape = CitacMape.ReadLine();
            for (int i = 0; i < linijaMape.Length; i++) Mapa[brojReda, i] = linijaMape[i];
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

                int broj = Mapa[i, j];

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
                Console.Write(Mapa[i, j]);
            }
            Console.WriteLine();
        }
    }
    public static void Main(string[] args)
    {
        int[,] TestVrednosti = new int[4, 100];
        for (int i = 0; i < TestVrednosti.GetLength(1); i++) TestVrednosti[0, i] = i % 6;
        char[,] Mapa = PretvaracTxtMapeUMatricu("MAPA1");
        IspisMape(Mapa, TestVrednosti);
    }
}