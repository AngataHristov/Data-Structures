namespace ReverseNumbersWithStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseNumbersWithStackMain
    {
        public static void Main()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                Environment.Exit(0);
            }

            int[] numbers = input
                .Split()
                .Select(int.Parse)
                .ToArray();

            Stack<int> buffer = new Stack<int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                buffer.Push(numbers[i]);
            }

            while (buffer.Count > 0)
            {
                int number = buffer.Pop();

                Console.Write("{0} ", number);
            }

            Console.WriteLine();
        }
    }
}
