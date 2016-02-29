namespace CustomLinkedList
{
    public class ListNode<T>
    {
        public ListNode(T element)
        {
            this.Element = element;
            this.NextNode = null;
        }

        public ListNode(T element, ListNode<T> prevNode)
        {
            this.Element = element;
            prevNode.NextNode = this;
        }

        public T Element { get; set; }

        public ListNode<T> NextNode { get; set; }
    }
}
