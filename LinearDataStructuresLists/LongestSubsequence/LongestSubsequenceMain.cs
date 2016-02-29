namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestSubsequenceMain
    {
        public static void Main()
        {
            Console.WriteLine("Enter sequence of integer numbers: ");

            List<int> sequence = Console.ReadLine()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            IList<int> longestSubsequence = findLongestSubsequence(sequence);

            Console.WriteLine("{0}", string.Join(" ", longestSubsequence));

            //var groups = sequence.GroupBy(n => n);

            //IGrouping<int, int> maxLengthGroup = null;
            //int maxLength = new int();

            //foreach (IGrouping<int, int> group in groups)
            //{
            //    int currentGroupLength = group.Count();

            //    if (currentGroupLength > maxLength)
            //    {
            //        maxLength = currentGroupLength;
            //        maxLengthGroup = group;
            //    }
            //}

            //for (int i = 0; i < maxLength; i++)
            //{
            //    Console.Write("{0} ", maxLengthGroup.Key);
            //}
        }

        private static IList<int> findLongestSubsequence(IList<int> sequence)
        {
            IList<int> result = new List<int>();

            int bestSequence = 0;
            int currentSequence = 1;
            int bestNum = 0;

            for (int i = 0; i < sequence.Count - 1; i++)
            {
                int currentElement = sequence[i];
                int nextElement = sequence[i + 1];

                if (currentElement == nextElement)
                {
                    currentSequence++;
                }
                else
                {
                    currentSequence = 1;
                }

                if (currentSequence > bestSequence)
                {
                    bestSequence = currentSequence;
                    bestNum = currentElement;
                }
            }

            for (int i = 0; i < bestSequence; i++)
            {
                result.Add(bestNum);
            }

            return result;
        }
    }
}
