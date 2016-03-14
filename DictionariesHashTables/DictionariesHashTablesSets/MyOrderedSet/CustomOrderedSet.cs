namespace MyOrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomOrderedSet<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private BinaryTree<T> root;

        public CustomOrderedSet()
        {
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Add(T value)
        {
            BinaryTree<T> newNode = new BinaryTree<T>(value);

            if (this.root == null)
            {
                this.root = newNode;
                this.Count++;
            }
            else if (!this.Contains(value))
            {
                this.Insert(newNode, this.root, null);
                this.Count++;
            }
        }

        public bool Contains(T value)
        {
            return this.CheckIfContainsElement(value, this.root);
        }

        public void Remove(T value)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException("Set is empty");
            }

            BinaryTree<T> nodeToRemove = new BinaryTree<T>(value);
            nodeToRemove = this.FindNode(value, this.root, nodeToRemove);

            if (nodeToRemove == null)
            {
                throw new ArgumentException("Node doesn't exist");
            }

            if (nodeToRemove.RightChild == null && nodeToRemove.LeftChild != null)
            {
                nodeToRemove.LeftChild.Parent = nodeToRemove.Parent;
            }

            if (nodeToRemove.LeftChild == null && nodeToRemove.RightChild != null)
            {
                nodeToRemove.RightChild.Parent = nodeToRemove.Parent;
            }

            if (nodeToRemove.LeftChild != null && nodeToRemove.RightChild != null)
            {
                nodeToRemove.LeftChild.Parent = nodeToRemove.RightChild;
                nodeToRemove.RightChild.Parent = nodeToRemove.Parent;
            }

            nodeToRemove = null;

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private List<T> GetChildrenValues(BinaryTree<T> current, List<T> list)
        {
            if (current.LeftChild != null)
            {
                list = this.GetChildrenValues(current.LeftChild, list);
            }

            list.Add(current.Value);

            if (current.RightChild != null)
            {
                list = this.GetChildrenValues(current.RightChild, list);
            }

            return list;
        }

        private BinaryTree<T> FindNode(T value, BinaryTree<T> current, BinaryTree<T> found)
        {
            if (current == null)
            {
                return null;
            }

            if (current.Value.Equals(value))
            {
                return current;
            }

            if (value.CompareTo(current.Value) > 0)
            {
                found = this.FindNode(value, current.RightChild, found);
            }
            else
            {
                found = this.FindNode(value, current.LeftChild, found);
            }

            return found;
        }

        private bool CheckIfContainsElement(T value, BinaryTree<T> current)
        {
            if (current == null)
            {
                return false;
            }

            if (current.Value.Equals(value))
            {
                return true;
            }

            if (value.CompareTo(current.Value) > 0)
            {
                this.CheckIfContainsElement(value, current.RightChild);
            }
            else
            {
                this.CheckIfContainsElement(value, current.LeftChild);
            }

            return false;
        }

        private void Insert(BinaryTree<T> newNode, BinaryTree<T> current, BinaryTree<T> parent)
        {
            if (current == null)
            {
                if (newNode.Value.CompareTo(parent.Value) > 0)
                {
                    parent.RightChild = newNode;
                    parent.RightChild.Parent = parent;
                }
                else
                {
                    parent.LeftChild = newNode;
                    parent.LeftChild.Parent = parent;
                }

                return;
            }

            if (newNode.Value.CompareTo(current.Value) > 0)
            {
                this.Insert(newNode, current.RightChild, current);
            }
            else
            {
                this.Insert(newNode, current.LeftChild, current);
            }

        }
    }
}
