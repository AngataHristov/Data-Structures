namespace LongestPathInTree
{
    using System.Collections.Generic;

    public class Tree
    {
        private int? sumToRoot;

        public Tree(int value)
        {
            this.sumToRoot = null;
            this.Value = value;
            this.Children = new List<Tree>();
        }

        public int Value { get; private set; }

        public Tree Parent { get; set; }

        public IList<Tree> Children { get; }

        public int SumToRoot
        {
            get
            {
                if (this.sumToRoot == null)
                {
                    this.CalculateSumToRoot();
                }

                return this.sumToRoot.GetValueOrDefault();
            }
        }

        private void CalculateSumToRoot()
        {
            this.sumToRoot = 0;
            this.sumToRoot += this.Value;

            if (this.Parent != null)
            {
                this.sumToRoot += this.Parent.sumToRoot;
            }
        }
    }
}
