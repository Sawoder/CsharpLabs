using System;
using System.Collections;
using System.Collections.Generic;

namespace MyLinkedList
{
    public class MyLinkedList<T> : ICollection<T>
    {
        public event Action<T> Added;
        public event Action<T> Removed;
        public event Action<string> Cleared;
        
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }

        public Node First { get; private set; }
        public Node Last { get; private set; }

        public class Node
        {
            public T Item;
            public Node Next { get; set; }
            public Node Prev { get; set; }

            public Node(T item)
            {
                Item = item;
            }
        }
        
        public void Add(T item)
        {
            Node node = new Node(item);
            if (Count == 0) First = node;
            else
            {
                Last.Next = node;
                node.Prev = Last;
            }
            Last = node;
            Count++;
            Added?.Invoke(item);
        }

        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;
            Cleared?.Invoke("Clear");
        }

        public bool Contains(T item)
        {
            Node current = First;
            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Node current = First;
            while (current != null)
            {
                array[arrayIndex++] = current.Item;
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            Node prev = null;
            Node current = First;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    if (prev != null)
                    {
                        prev.Next = current.Next;
                        if (current.Next == null) Last = prev;
                        else current.Next.Prev = prev;
                        Count--;
                    }
                    else
                    {
                        if (Count != 0)
                        {
                            First = First.Next;
                            Count--;
                            if (Count == 0) Last = null;
                            else First.Prev = null;
                        }
                    }
                    Removed?.Invoke(item);
                    return true;
                }
                prev = current;
                current = current.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = First;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Insert(T item, T index)
        {
            Node current = First;
            Node node = new Node(item);

            while (current != null)
            {
                if (current.Item.Equals(index))
                {
                    node.Next = current;
                    node.Prev = current.Prev;
                    if (current.Prev != null)
                    {
                        current.Prev.Next = node;
                    }
                    current.Prev = node;

                    if (node.Prev == null)
                    {
                        First = node;
                    }
                    Count++;
                    Added?.Invoke(item);
                    return true;
                }
                current = current.Next;
            }
            
            return false;
        }
    }
}
