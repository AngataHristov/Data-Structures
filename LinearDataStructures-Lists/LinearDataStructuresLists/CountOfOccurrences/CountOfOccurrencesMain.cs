namespace CountOfOccurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CountOfOccurrencesMain
    {
        public static void Main()
        {
            SortedDictionary<int, int> numbersOccurence = new SortedDictionary<int, int>();
            Console.WriteLine("Enter sequence of integer numbers: ");

            List<int> sequence = Console.ReadLine()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            foreach (int number in sequence)
            {
                if (!numbersOccurence.ContainsKey(number))
                {
                    numbersOccurence[number] = new int();
                }

                numbersOccurence[number]++;
            }

            foreach (var number in numbersOccurence)
            {
                Console.WriteLine("{0} -> {1} times.", number.Key, number.Value);
            }
        }
    }
}
