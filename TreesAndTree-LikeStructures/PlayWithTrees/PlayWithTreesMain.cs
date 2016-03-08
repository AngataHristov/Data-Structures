namespace PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayWithTreesMain
    {
        private static Dictionary<int, Tree<int>> NodeByValue = new Dictionary<int, Tree<int>>();
        private static int longestPath;
        private static Tree<int> longestPathLeaf;

        public static void Main()
        {
            int nodeCount = int.Parse(Console.ReadLine());

            for (int i = 1; i < nodeCount; i++)
            {
                string[] edge = Console.ReadLine().Split();
                int parentValue = int.Parse(edge[0]);
                Tree<int> parentNode = GetTreeNodeByValue(parentValue);
                int childValue = int.Parse(edge[1]);
                Tree<int> childNode = GetTreeNodeByValue(childValue);
                parentNode.AddChild(childNode);
                childNode.Parent = parentNode;
            }

            int pathSum = int.Parse(Console.ReadLine());
            int subtreeSum = int.Parse(Console.ReadLine());

            Console.WriteLine("Root node: {0}", FindRootNode().Value);

            List<int> leafNodesValues = FindLeafNodes()
                .Select(n => n.Value)
                .ToList();
            leafNodesValues.Sort();

            Console.WriteLine("Leaf nodes: {0}", string.Join(", ", leafNodesValues));

            List<int> middleNodesValues = FindMiddleNodes()
                .Select(n => n.Value)
                .ToList();
            middleNodesValues.Sort();

            Console.WriteLine("Middle nodes: {0}", string.Join(", ", middleNodesValues));

            var rootNode = FindRootNode();
            FindLongestPath(rootNode);
            var longest = BackTrackPath(longestPathLeaf);
            Console.WriteLine($"Longest path:{Environment.NewLine}{longest} (length = {longestPath})");

            Console.WriteLine($"Paths of sum {pathSum}:");
            FindAllPathsWithSum(rootNode, rootNode.Value, pathSum);

            Console.WriteLine($"Subtrees of sum {subtreeSum}:");
            FindAllSubTreesOfSum(rootNode, subtreeSum);
        }

        private static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!NodeByValue.ContainsKey(value))
            {
                NodeByValue[value] = new Tree<int>(value);
            }

            return NodeByValue[value];
        }

        private static Tree<int> FindRootNode()
        {
            var rootNode = NodeByValue.Values.FirstOrDefault(n => n.Parent == null);

            return rootNode;
        }

        private static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = NodeByValue.Values
                .Where(n => n.ChildrenCount > 0 && n.Parent != null)
                .ToList();

            return middleNodes;
        }

        private static IEnumerable<Tree<int>> FindLeafNodes()
        {
            var leafNodes = NodeByValue.Values
                .Where(n => n.ChildrenCount == 0 && n.Parent != null)
                .ToList();

            return leafNodes;
        }

        private static void FindLongestPath(Tree<int> tree, int depth = 1)
        {
            if (depth > longestPath)
            {
                longestPath = depth;
                longestPathLeaf = tree;
            }

            foreach (var child in tree.Children)
            {
                FindLongestPath(child, depth + 1);
            }
        }

        private static void FindAllPathsWithSum(Tree<int> tree, int sum, int pathSumWanted)
        {
            if (sum == pathSumWanted)
            {
                Console.WriteLine(BackTrackPath(tree));
            }

            foreach (var child in tree.Children)
            {
                FindAllPathsWithSum(child, sum + child.Value, pathSumWanted);
            }
        }

        private static string BackTrackPath(Tree<int> tree, string separator = " -> ")
        {
            var result = new LinkedList<int>();
            while (tree != null)
            {
                result.AddFirst(tree.Value);
                tree = tree.Parent;
            }

            return string.Join(separator, result);
        }

        private static void DfsTravers(Tree<int> tree, IList<int> subTree)
        {
            subTree.Add(tree.Value);

            foreach (var child in tree.Children)
            {
                DfsTravers(child, subTree);
            }
        }

        private static void FindAllSubTreesOfSum(Tree<int> tree, int subTreeSumWanted)
        {
            if (tree.SubTreeSum == subTreeSumWanted)
            {
                var subTree = new List<int>();
                DfsTravers(tree, subTree);
                Console.WriteLine(string.Join(" + ", subTree));
            }

            foreach (var child in tree.Children)
            {
                FindAllSubTreesOfSum(child, subTreeSumWanted);
            }
        }

    }
}
