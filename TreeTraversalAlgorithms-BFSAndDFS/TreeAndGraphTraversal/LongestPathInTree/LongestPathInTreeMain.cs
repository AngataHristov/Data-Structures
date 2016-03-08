namespace LongestPathInTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestPathInTreeMain
    {
        private static IDictionary<int, Tree> nodes;

        public static void Main()
        {
            GetNode();

            var longestPath = FindLongestPath();

            Console.WriteLine(longestPath);
        }

        private static int FindLongestPath()
        {
            int longestPath = 0;

            foreach (Tree tree in nodes.Values)
            {
                foreach (Tree value in nodes.Values)
                {
                    if (tree.Value != value.Value)
                    {
                        int currentPath = tree.SumToRoot - value.SumToRoot + value.Value;
                        if (currentPath > longestPath)
                        {
                            longestPath = currentPath;
                        }
                    }
                }
            }

            return longestPath;
        }

        private static void GetNode()
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            int numberOfEdges = int.Parse(Console.ReadLine());

            nodes = new Dictionary<int, Tree>();

            for (int node = 0; node < numberOfEdges; node++)
            {
                var parentChildPair = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var currentParent = GetTree(parentChildPair[0]);
                var currentChild = GetTree(parentChildPair[1]);
            }
        }

        private static Tree GetTree(int value)
        {
            if (!nodes.ContainsKey(value))
            {
                nodes[value] = new Tree(value);
            }

            return nodes[value];
        }
    }
}
