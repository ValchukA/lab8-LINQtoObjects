using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQtoObjects
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public int Size { get; private set; }

        class Node
        {
            public T data;
            public Node next;

            public Node(T data) => this.data = data;
        }

        private Node head;
        private Node tail;

        public T First
        {
            get
            {
                ValidateSize();

                return head.data;
            }
        }

        public T Last
        {
            get
            {
                ValidateSize();

                return tail.data;
            }
        }

        public T this[int index]
        {
            get => GetNodeAt(index, true).data;
            set => GetNodeAt(index, true).data = value;
        }

        public LinkedList() { }

        public LinkedList(IEnumerable<T> collection) => AddRange(collection);

        public void AddFirst(T item) => Insert(0, item);

        public void AddLast(T item) => Insert(Size, item);

        public void Insert(int index, T item)
        {
            if (index == 0)
            {
                Node newNode = new(item) { next = head };

                head = newNode;

                if (Size == 0)
                {
                    tail = newNode;
                }
            }
            else if (index == Size)
            {
                Node newNode = new(item);

                tail.next = newNode;
                tail = newNode;
            }
            else
            {
                Node tmp = GetNodeAt(index - 1, true);

                Node newNode = new(item) { next = tmp.next };

                tmp.next = newNode;
            }

            Size++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                AddLast(item);
            }
        }

        public void RemoveFirst()
        {
            ValidateSize();

            RemoveAt(0);
        }

        public void RemoveLast()
        {
            ValidateSize();

            RemoveAt(Size - 1);
        }

        public void Remove(T item)
        {
            Node tmp = head;
            Node tmpPrev = null;

            while (tmp != null)
            {
                if (tmp.data.Equals(item))
                {
                    if (tmp == head)
                    {
                        head = head.next;
                    }
                    else if (tmp == tail)
                    {
                        tmpPrev.next = null;
                        tail = tmpPrev;
                    }
                    else
                    {
                        tmpPrev.next = tmp.next;
                    }
                }

                tmpPrev = tmp;
                tmp = tmp.next;
            }
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            if (Size == 1)
            {
                head = null;
                tail = null;
            }
            else if (index == 0)
            {
                head = head.next;
            }
            else if (index == Size - 1)
            {
                Node prev = GetNodeAt(index - 1);

                prev.next = null;
                tail = prev;
            }
            else
            {
                Node prev = GetNodeAt(index - 1);

                prev.next = prev.next.next;
            }

            Size--;
        }

        public int? Find(T item)
        {
            ValidateSize();

            Node tmp = head;

            for (int index = 0; index < Size; index++)
            {
                if (tmp.data.Equals(item))
                {
                    return index;
                }

                tmp = tmp.next;
            }

            return null;
        }

        public void Sort()
        {
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException("Object of type T must implement IComparable<T>.");
            }

            var collectionAsArray = this.ToArray();

            Array.Sort(collectionAsArray);

            head = null;
            tail = null;

            Size = 0;

            AddRange(collectionAsArray);
        }

        private void ValidateSize()
        {
            if (Size <= 0)
            {
                throw new InvalidOperationException("The collection is empty.");
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the collection.");
            }
        }

        private Node GetNodeAt(int index, bool validateIndex = false)
        {
            if (validateIndex == true)
            {
                ValidateIndex(index);
            }

            Node tmp = head;

            for (int i = 0; i < index; i++)
            {
                tmp = tmp.next;
            }

            return tmp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node tmp = head;

            while (tmp != null)
            {
                yield return tmp.data;

                tmp = tmp.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static LinkedList<T> operator +(LinkedList<T> first, LinkedList<T> second)
        {
            LinkedList<T> newCollection = new();

            newCollection.AddRange(first);
            newCollection.AddRange(second);

            return newCollection;
        }
    }
}
