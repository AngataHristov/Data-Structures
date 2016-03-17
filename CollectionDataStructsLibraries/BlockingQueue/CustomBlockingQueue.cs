using System.Collections.Generic;
using System.Threading;

public class CustomBlockingQueue<T>
{
    private readonly Queue<T> queue = new Queue<T>();
    private readonly int capacity;

    public CustomBlockingQueue(int capacity = 16)
    {
        this.capacity = capacity;
    }

    public void Add(T item)
    {
        lock (queue)
        {
            while (queue.Count >= this.capacity)
            {
                // Release lock and sleep until another thread wakes you up
                Monitor.Wait(queue);
            }

            queue.Enqueue(item);

            if (queue.Count == 1)
            {
                // Wake up any blocked dequeue
                Monitor.PulseAll(queue);
            }
        }
    }

    public T Take()
    {
        lock (queue)
        {
            while (queue.Count == 0)
            {
                Monitor.Wait(queue);
            }

            T item = queue.Dequeue();

            if (queue.Count == this.capacity - 1)
            {
                // wake up any blocked enqueue
                Monitor.PulseAll(queue);
            }

            return item;
        }
    }
}