using System;
using System.Collections.Generic;

namespace ATM
{
    internal class Program
    {
        static long inputCheck()
        {
            long value;

            try
            {
                value = Convert.ToInt64(Console.ReadLine());
                if (value <= 0)
                {
                    Console.WriteLine("Invalid input");
                    value = inputCheck();
                }
            }
            catch
            {
                Console.WriteLine("Invalid input");
                return inputCheck();
            }

            return value;
        }

        static long[] inputChanger(long value)
        {
            HashSet<string> exchange = new HashSet<string>();
            string nextVal = "";
            while (nextVal != "0")
            {
                nextVal = Console.ReadLine();
                if (nextVal != "0") exchange.Add(nextVal);
            }

            long match;
            List<long> numbers = new List<long>();
            foreach (string banknote in exchange)
            {
                try
                {
                    match = Convert.ToInt64(banknote);
                    if (match <= 0 || match > value)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    else
                    {
                        numbers.Add(match);
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }
            }

            if (numbers.Count == 0)
            {
                Console.WriteLine("Empty input: ");
                return inputChanger(value);
            }

            numbers.Sort((a, b) => b.CompareTo(a));
            return numbers.ToArray();
        }

        static void findCombinations(long value, long[] banknotes, int i, long[] denomination)
        {
            if (value == 0)
            {
                for (int j = 0; j < denomination.Length; ++j)
                {
                    if (denomination[j] != 0)
                    {
                        for (int k = 0; k < denomination[j]; ++k) Console.Write(banknotes[j] + " ");
                    }
                }

                Console.WriteLine();
                return;
            }

            for (int j = i; j < banknotes.Length; ++j)
            {
                if (value / banknotes[j] > 0)
                    for (long k = value / banknotes[j]; k > 0; --k)
                    {
                        value -= banknotes[j] * k;
                        denomination[j] += k;
                        findCombinations(value, banknotes, j + 1, denomination);
                        denomination[j] -= k;
                        value += banknotes[j] * k;
                    }
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Input an amount: ");
            long value = inputCheck();
            Console.WriteLine("Input amount of change ");
            long[] banknotes = inputChanger(value);
            long[] denomination = new long[banknotes.Length];
            Console.WriteLine("Exchange: ");
            findCombinations(value, banknotes, 0, denomination);
        }
    }
}