namespace RoundDance
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RoundDanceMain
    {
        private static IDictionary<int, IList<int>> nodes = new Dictionary<int, IList<int>>();

        private static int longestRoundDance;

        public static void Main()
        {
            int numberOfFriendships = int.Parse(Console.ReadLine());

            int leader = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfFriendships; i++)
            {
                var edge = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                AddNode(edge[0], edge[1]);
                AddNode(edge[1], edge[0]);
            }

            FindLongestRoundDance(leader, leader);

            Console.WriteLine(longestRoundDance);
        }

        private static void AddNode(int node, int neighbour)
        {
            if (!nodes.ContainsKey(node))
            {
                nodes[node] = new List<int>();
            }

            nodes[node].Add(neighbour);
        }

        public static void FindLongestRoundDance(int node, int prevNode, int count = 0)
        {
            count++;

            foreach (var neighbour in nodes[node])
            {
                if (neighbour == prevNode)
                {
                    continue;
                }

                FindLongestRoundDance(neighbour, node, count);
            }

            if (count > longestRoundDance)
            {
                longestRoundDance = count;
            }
        }
    }
}
