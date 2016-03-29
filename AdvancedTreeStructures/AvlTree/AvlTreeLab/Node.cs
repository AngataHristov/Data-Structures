namespace AvlTreeLab
{
    using System;

    public class Node<T> where T : IComparable<T>
    {
        private Node<T> leftChild;
        private Node<T> rightChild;

        public Node(T value)
        {
            this.Value = value;
            this.Count = 1;
        }

        public T Value { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public Node<T> Parent { get; set; }

        public int BallanceFactor { get; set; }

        public int Count { get; set; }

        public bool IsLeftChild
        {
            get
            {
                return this.Parent.LeftChild == this;
            }
        }

        public bool IsRightChild
        {
            get
            {
                return this.Parent.RightChild == this;
            }
        }

        public int ChildrenCount
        {
            get
            {
                return this.LeftChild.ChildrenCount + this.RightChild.ChildrenCount;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}

