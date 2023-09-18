﻿using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Inserisci una targa nel formato AA 000 AA: ");
        string targa = Console.ReadLine();

        // Rimuovi gli spazi dalla targa per la validazione
        targa = targa.Replace(" ", "");

        if (IsValidTarga(targa))
        {
            int valoreNumerico = ConvertTargaToNumero(targa);
            Console.WriteLine($"Il valore numerico della targa {targa} è: {valoreNumerico}");
        }
        else
        {
            Console.WriteLine("La targa inserita non è valida.");
        }
        Console.ReadLine();
    }

    static bool IsValidTarga(string targa)
    {
        if (targa.Length != 7)
            return false;

        for (int i = 0; i < 2; i++)
        {
            if (!Char.IsLetter(targa[i]) || !Char.IsUpper(targa[i]))
                return false;
        }

        for (int i = 2; i < 5; i++)
        {
            if (!Char.IsDigit(targa[i]))
                return false;
        }

        for (int i = 5; i < 7; i++)
        {
            if (!Char.IsLetter(targa[i]) || !Char.IsUpper(targa[i]))
                return false;
        }

        return true;
    }

    static int ConvertTargaToNumero(string targa)
    {
        int numero = 0;

        for (int i = 0; i < 2; i++)
        {
            numero = numero * 26 + (targa[i] - 'A' + 1);
        }

        for (int i = 2; i < 5; i++)
        {
            numero = numero * 10 + (targa[i] - '0');
        }

        for (int i = 5; i < 7; i++)
        {
            numero = numero * 26 + (targa[i] - 'A' + 1);
        }

        return numero;
    }
}
