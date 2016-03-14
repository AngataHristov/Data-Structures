namespace MyOrderedSet
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            var set = new CustomOrderedSet<int>();
            set.Add(17);
            set.Add(9);
            set.Add(12);
            set.Add(19);
            set.Add(6);
            set.Add(25);
            set.Add(17);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Count is: {0}", set.Count);

            set.Remove(25);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Count is: {0}", set.Count);
        }
    }
}
