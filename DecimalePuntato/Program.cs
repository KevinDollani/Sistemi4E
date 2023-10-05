﻿using System;

namespace DecimalePuntato
{
    class Program
    {
        static void Main(string[] args)
        {
            string address;
            int[] dottedPunctuation = new int[4];
            int intAddressFromDP = 0;
            int intAddressFromBin = 0;
            int[] dottedDecimalFromBin;

            Console.WriteLine("Inserisci l'indirizzo IP:\n");
            address = Console.ReadLine();

            dottedPunctuation = GetOctet(address);

            bool[] binaryIP = new bool[32];

            binaryIP = Convert_DP_to_Bin(dottedPunctuation);

            intAddressFromDP = Convert_DP_to_Int(dottedPunctuation);

            intAddressFromBin = Convert_Bin_to_Int(binaryIP);

            Console.WriteLine("IP decimale da Dotted: " + intAddressFromDP);

            Console.WriteLine("IP decimale da Binario: " + intAddressFromBin);

            Console.WriteLine("Dotted Decimal IP da Binario: ");
            dottedDecimalFromBin = Convert_Bin_to_DP(binaryIP);

            foreach (var octet in dottedDecimalFromBin)
            {
                Console.Write(octet.ToString() + ' ');
            }

            Console.ReadKey();
        }

        static int[] GetOctet(string address)
        {
            string[] octetStrings = address.Split('.');
            int[] ip = new int[4];

            if (octetStrings.Length != 4)
            {
                throw new ArgumentException("Formato dell'indirizzo IP non valido. L'indirizzo dovrebbe avere 4 ottetti.");
            }

            for (int i = 0; i < octetStrings.Length; i++)
            {
                if (!int.TryParse(octetStrings[i], out ip[i]))
                {
                    throw new ArgumentException("Valore ottetto non valido. L'ottetto dovrebbe essere un numero intero valido compreso tra 0 e 255.");
                }

                if (ip[i] < 0 || ip[i] > 255)
                {
                    throw new ArgumentException("Valore ottetto non valido. L'ottetto dovrebbe essere un numero intero valido compreso tra 0 e 255.");
                }
            }

            return ip;
        }

        static bool[] Convert_DP_to_Bin(int[] dp)
        {
            bool[] boolIP = new bool[32];
            int index = 0;

            foreach (int octet in dp)
            {
                for (int i = 7; i >= 0; i--)
                {
                    boolIP[index] = (octet & (1 << i)) != 0;
                    index++;
                }
            }

            return boolIP;
        }

        static int Convert_DP_to_Int(int[] dp)
        {
            int result = 0;

            for (int i = 0; i < 4; i++)
            {
                result += dp[i] * (int)Math.Pow(256, 3 - i);
            }

            return result & 0x7FFFFFFF;
        }

        static int Convert_Bin_to_Int(bool[] bn)
        {
            int result = 0;

            for (int i = bn.Length - 1; i >= 0; i--)
            {
                if (bn[i])
                {
                    result += (int)Math.Pow(2, bn.Length - 1 - i);
                }
            }

            return result & 0x7FFFFFFF;
        }

        static int[] Convert_Bin_to_DP(bool[] b)
        {
            int index = 0;
            int[] dottedDecimalArray = new int[4];

            for (int i = 0; i < b.Length; i += 8)
            {
                int octetValue = 0;

                for (int j = 0; j < 8; j++)
                {
                    octetValue += (b[i + j] ? 1 : 0) << (7 - j);
                }

                dottedDecimalArray[index] = octetValue;
                index++;
            }

            return dottedDecimalArray;
        }
    }
}
