namespace SumAndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumAndAverageMain
    {
        public static void Main()
        {
            Console.WriteLine("Enter sequence of integer numbers: ");

            List<int> sequence = Console.ReadLine()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            //int sum = sequence.Sum();
            //double average = sequence.Average();

            int sum = Sum(sequence);
            double average = Average(sequence);

            Console.WriteLine("Sum: {0}", sum);
            Console.WriteLine("Average: {0}", average);
        }

        private static int Sum(ICollection<int> sequence)
        {
            int sum = new int();
            foreach (int number in sequence)
            {
                sum += number;
            }

            return sum;
        }

        private static double Average(ICollection<int> sequence)
        {
            double average = (double)Sum(sequence) / sequence.Count;

            return average;
        }
    }
}
