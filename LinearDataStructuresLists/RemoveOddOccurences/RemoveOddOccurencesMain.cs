namespace RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RemoveOddOccurencesMain
    {
        public static void Main()
        {
            Dictionary<int, int> numbersOccurence = new Dictionary<int, int>();
            Console.WriteLine("Enter sequence of integer numbers: ");

            List<int> sequence = Console.ReadLine()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            Dictionary<int, int> results = new Dictionary<int, int>();

            foreach (int number in sequence)
            {
                if (!numbersOccurence.ContainsKey(number))
                {
                    numbersOccurence[number] = 0;
                }

                numbersOccurence[number]++;
            }

            foreach (var pair in numbersOccurence)
            {
                if (pair.Value % 2 == 0)
                {
                    results.Add(pair.Key, pair.Value);
                }
            }

            foreach (var pair in results)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    Console.Write("{0} ", pair.Key);
                }
            }

            Console.WriteLine();
        }
    }
}
