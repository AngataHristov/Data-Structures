namespace AvlTreeLab
{
    using System;
    using System.Linq;

    public class AvlTree<T> where T : IComparable<T>
    {
        private Node<T> root;

        public int Count { get; private set; }

        public void Add(T item)
        {
            bool inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            else
            {
                inserted = this.InsertInternal(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (item.CompareTo(node.Value) == 0)
                {
                    return true;
                }

                if (item.CompareTo(node.Value) > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    node = node.LeftChild;
                }
            }

            return false;
        }

        public void ForeachDfs(Action<int, T> action)
        {
            if (this.Count == 0)
            {
                return;
            }

            this.InOrderDfs(this.root, 1, action);
        }

        private bool InsertInternal(Node<T> node, T item)
        {
            var currentNode = node;
            var newNode = new Node<T>(item);
            var shouldRetrace = false;

            while (true)
            {
                if (currentNode.Value.CompareTo(item) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newNode;
                        currentNode.BalanceFactor--;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        currentNode.BalanceFactor++;
                        shouldRetrace = currentNode.BalanceFactor != 0;

                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }

                break;
            }

            if (shouldRetrace)
            {
                this.RetraceInsert(currentNode);
            }

            return true;
        }

        private void RetraceInsert(Node<T> node)
        {
            var parent = node.Parent;

            while (parent != null)
            {
                if (node.IsLeftChild)
                {
                    if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor++;
                        if (node.BalanceFactor == -1)
                        {
                            this.RotateLeft(parent);
                        }

                        this.RotateRight(parent);
                    }
                    else if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        parent.BalanceFactor = 1;
                        break;
                    }

                    node = parent;
                    parent = node.Parent;
                }
                else
                {
                    if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor--;
                        if (node.BalanceFactor == 1)
                        {
                            this.RotateRight(parent);
                        }

                        this.RotateLeft(parent);
                    }
                    else if (parent.BalanceFactor == 11)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        parent.BalanceFactor = -1;
                        break;
                    }
                }

                node = parent;
                parent = node.Parent;
            }
        }

        private void RotateLeft(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.RightChild;

            if (parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            node.RightChild = child.LeftChild;
            child.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.LeftChild;

            if (parent != null)
            {
                if (node.IsRightChild)
                {
                    parent.RightChild = child;
                }
                else
                {
                    parent.LeftChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            node.LeftChild = child.RightChild;
            child.RightChild = node;

            node.BalanceFactor -= 1 + Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor -= 1 + Math.Max(node.BalanceFactor, 0);
        }

        private void InOrderDfs(Node<T> node, int depth, Action<int, T> action)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, depth + 1, action);
            }

            action(depth, node.Value);

            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, depth + 1, action);
            }
        }
    }
}
