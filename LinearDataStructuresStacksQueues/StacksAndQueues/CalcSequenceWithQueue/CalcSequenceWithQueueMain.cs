namespace CalcSequenceWithQueue
{
    using System;
    using System.Collections.Generic;

    public class CalcSequenceWithQueueMain
    {
        private const int numberOfMembers = 50;

        public static void Main()
        {
            int number = int.Parse(Console.ReadLine());

            Queue<long> numbers = new Queue<long>();

            numbers.Enqueue(number);

            int counter = 0;

            while (counter < numberOfMembers)
            {
                long currentNumber = numbers.Dequeue();

                Console.Write("{0} ", currentNumber);

                numbers.Enqueue(currentNumber + 1);
                numbers.Enqueue(2 * currentNumber + 1);
                numbers.Enqueue(currentNumber + 2);


                counter ++;
            }

            Console.WriteLine();
        }
    }
}
