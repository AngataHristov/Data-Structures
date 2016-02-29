namespace LinkedStack
{
    public class Node<T>
    {
        public Node(T element)
        {
            this.Element = element;
            this.NextNode = null;
        }

        public Node(T element, Node<T> nextNode)
        {
            this.Element = element;

            this.NextNode = nextNode;
        }

        public T Element { get; set; }

        public Node<T> NextNode { get; set; }
    }
}
