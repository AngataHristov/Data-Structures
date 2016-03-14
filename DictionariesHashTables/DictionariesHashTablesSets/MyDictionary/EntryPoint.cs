namespace MyDictionary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            //// Count Symbols
            //CustomDictionary<char, int> data = new CustomDictionary<char, int>();

            //char[] input = Console.ReadLine().ToCharArray();

            //foreach (char symbol in input)
            //{
            //    if (!data.ContainsKey(symbol))
            //    {
            //        data.Add(symbol, 0);
            //    }

            //    data[symbol]++;
            //}

            //var sorteresSymbols = data.OrderBy(element => element.Key);

            //foreach (KeyValue<char, int> keyValue in sorteresSymbols)
            //{
            //    Console.WriteLine("{0} -> {1} time/s", keyValue.Key, keyValue.Value);
            //}

            //Phonebook

            string input = Console.ReadLine();

            CustomDictionary<string, string> phoneNumberByName = new CustomDictionary<string, string>();

            FillPhonebook(input, phoneNumberByName);

            while (true)
            {
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (input.Split('-').Length > 1)
                {
                    FillPhonebook(input, phoneNumberByName);

                    continue;
                }

                if (phoneNumberByName.ContainsKey(input))
                {
                    Console.WriteLine("{0} -> {1}", input, phoneNumberByName[input]);

                    continue;
                }

                Console.WriteLine("Contact {0} does not exist.", input);
            }
        }

        private static void FillPhonebook(string input, CustomDictionary<string, string> phoneNumberByName)
        {
            while (input != "search")
            {
                if (string.IsNullOrEmpty(input))
                {
                    input = Console.ReadLine();
                    continue;
                }

                string[] inputArgs = input.Split('-');

                string name = inputArgs[0];
                string phoneNumber = inputArgs[1];

                if (!phoneNumberByName.ContainsKey(name) || phoneNumberByName[name] != phoneNumber)
                {
                    phoneNumberByName[name] = phoneNumber;
                }

                input = Console.ReadLine();
            }
        }
    }
}
