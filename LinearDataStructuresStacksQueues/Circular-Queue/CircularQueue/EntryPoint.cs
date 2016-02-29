namespace Circular_Queue
{
    using System;
    using System.Collections.Generic;

    public class EntryPoint
    {
        public static void Main()
        {
            var queue = new CircularQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);

            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(String.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            var first = queue.Dequeue();
            Console.WriteLine("First = {0}", first);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(String.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            queue.Enqueue(-7);
            queue.Enqueue(-8);
            queue.Enqueue(-9);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(String.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            first = queue.Dequeue();
            Console.WriteLine("First = {0}", first);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(String.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            queue.Enqueue(-10);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(String.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            first = queue.Dequeue();
            Console.WriteLine("First = {0}", first);
            Console.WriteLine("Count = {0}", queue.Count);
            Console.WriteLine(String.Join(", ", queue.ToArray()));
            Console.WriteLine("---------------------------");

            Queue<int> queue1 = new Queue<int>();

            int n = 3;
            int m = 16;
            int index = 1;

            if (m < n)
            {
                Console.WriteLine("Not possible");
            }

            if (n == m)
            {
                Console.WriteLine(index);
            }

            queue1.Enqueue(n);

            while (true)
            {
                var elem = queue1.Dequeue();
                var add1 = elem + 1;

                index++;

                if (add1 == m)
                {
                    Console.WriteLine(index);
                    break;
                }

                queue1.Enqueue(add1);

                var multiply2 = elem * 2;

                index++;

                if (multiply2 == m)
                {
                    Console.WriteLine(index);
                    break;
                }

                queue1.Enqueue(multiply2);
            }
        }
    }
}
