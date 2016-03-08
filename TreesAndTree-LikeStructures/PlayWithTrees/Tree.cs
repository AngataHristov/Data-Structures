namespace PlayWithTrees
{
    using System.Collections.Generic;

    public class Tree<T>
    {
        private readonly IList<Tree<T>> children;
        private long? subTreeSum;

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.children = new List<Tree<T>>(children);

            foreach (Tree<T> child in children)
            {
                child.Parent = this;
            }
        }

        public T Value { get; set; }

        public Tree<T> Parent { get; set; }

        public IEnumerable<Tree<T>> Children
        {
            get
            {
                return this.children;
            }
        }

        public int ChildrenCount
        {
            get { return this.children.Count; }
        }

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public long SubTreeSum
        {
            get
            {
                if (this.subTreeSum == null)
                {
                    this.CalculateSubTreeSum();
                }

                return this.subTreeSum.Value;
            }
        }

        private void CalculateSubTreeSum()
        {
            this.subTreeSum = 0L;
            this.subTreeSum += (dynamic)this.Value;

            foreach (var child in this.Children)
            {
                this.subTreeSum += child.SubTreeSum;
            }
        }
    }
}
