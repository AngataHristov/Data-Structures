namespace CustomStringEditor
{
    using System;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            StringEditor editor = new StringEditor();

            string input = Console.ReadLine();

            while (true)
            {
                if (string.IsNullOrEmpty(input))
                {
                    input = Console.ReadLine();

                    continue;
                }

                string[] inputTokens = input
                                .Split()
                                .ToArray();
                string command = inputTokens[0];

                string result = string.Empty;
                switch (command)
                {
                    case "APPEND":
                        string item = inputTokens[1];

                        result = editor.Append(item);
                        break;
                    case "INSERT":
                        int position = int.Parse(inputTokens[1]);
                        string word = inputTokens[2];

                        result = editor.Insert(position, word);
                        break;
                    case "DELETE":
                        int startIndex = int.Parse(inputTokens[1]);
                        int count = int.Parse(inputTokens[2]);

                        result = editor.Delete(startIndex, count);
                        break;
                    case "PRINT":
                        editor.Print();
                        break;
                    case "REPLACE":
                        int index = int.Parse(inputTokens[1]);
                        int counts = int.Parse(inputTokens[2]);
                        string element = inputTokens[3];

                        result = editor.Replace(index, counts, element);
                        break;
                    case "END":
                        editor.End();
                        break;
                }

                Console.WriteLine(result);

                input = Console.ReadLine();
            }
        }
    }
}
