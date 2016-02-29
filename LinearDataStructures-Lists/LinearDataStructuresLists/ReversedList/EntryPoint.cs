namespace ReversedList
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            MyReversedList<int> list = new MyReversedList<int>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(i);
            }
        }
    }
}
