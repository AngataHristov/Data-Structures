namespace FindTheRoot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FindTheRootMain
    {
        public static void Main()
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            int numberOfEdges = int.Parse(Console.ReadLine());

            var hasRoot = new bool[numberOfNodes];

            for (int i = 0; i < numberOfEdges; i++)
            {
                var childNode = (Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray())[1];

                hasRoot[childNode] = true;
            }

            var nodesWithoutRoot = new List<int>();

            for (int node = 0; node < numberOfNodes; node++)
            {
                if (!hasRoot[node])
                {
                    nodesWithoutRoot.Add(node);
                }
            }

            if (nodesWithoutRoot.Count == 0)
            {
                Console.WriteLine("No root!");
            }
            else if (nodesWithoutRoot.Count == 1)
            {
                Console.WriteLine(nodesWithoutRoot[0]);
            }
            else
            {
                Console.WriteLine("Multiple root nodes! {0}",string.Join(", ",nodesWithoutRoot));
            }
        }
    }
}
