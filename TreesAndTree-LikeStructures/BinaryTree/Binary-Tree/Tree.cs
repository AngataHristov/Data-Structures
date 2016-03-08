using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Tree<T>
{
    private readonly IList<Tree<T>> children;

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.children = new List<Tree<T>>();

        //to avoid forEach
       // this.children = new List<Tree<T>>(children);

        foreach (Tree<T> child in children)
        {
            this.children.Add(child);
        }
    }

    public T Value { get; set; }

    public IEnumerable<Tree<T>> Children
    {
        get
        {
            return this.children;
        }
    }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.Value);

        foreach (Tree<T> child in this.children)
        {
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (Tree<T> child in this.children)
        {
            child.Each(action);
        }
    }
}
