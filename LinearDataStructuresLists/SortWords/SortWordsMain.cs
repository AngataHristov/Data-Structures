namespace SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SortWordsMain
    {
        public static void Main()
        {
            Console.WriteLine("Enter sequence of words: ");

            List<string> words = Console.ReadLine()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

            // List<string> sortedWords = words.OrderBy(w => w).ToList();
            // Console.WriteLine("{0}", string.Join(" ", sortedWords));

            //words.Sort();
            //Console.WriteLine("{0}", string.Join(" ", words));

            sortWords(words);

            Console.WriteLine("{0}", string.Join(" ", words));
        }

        private static void sortWords(IList<string> words)
        {
            for (int i = 0; i < words.Count - 1; i++)
            {
                for (int j = i + 1; j < words.Count; j++)
                {
                    if (words[i].CompareTo(words[j]) > 0)
                    {
                        string word = words[i];
                        words[i] = words[j];
                        words[j] = word;
                    }
                }
            }
        }
    }
}
