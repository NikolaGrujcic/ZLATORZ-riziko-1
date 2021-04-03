using System;
using System.IO;
class MainClass {
  static char[,] PretvoriMapuUMatricu(string ImeFajla)
    {
        char[,] Mapa = new char[250, 50];
        string linijaMape;
        StreamReader CitacMape = new StreamReader(ImeFajla);
        int brojReda = 0;
        while (!CitacMape.EndOfStream)
        {
            linijaMape = CitacMape.ReadLine();
            for (int i = 0; i < linijaMape.Length; i++) Mapa[i, brojReda] = linijaMape[i];
            brojReda++;
        }
        return Mapa;
    }
    
  public static void Main (string[] args) 
  {
    int[,] 
  }
}