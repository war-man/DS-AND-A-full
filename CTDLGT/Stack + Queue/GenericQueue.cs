using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  CTDLGT.Stack___Queue
{
    public class GenericQueue<T>
    {
        private readonly static Node HeadNode = new Node(default(T));

        private readonly Node _head;
        private Node _tail;

        public GenericQueue()
        {
            _head = HeadNode;
            _tail = _head;
        }

        public int Count { get; private set; }

        public GenericQueue(int tmp)
        {
            _head = HeadNode;
            _tail = _head;
            Count = tmp;
        }
        public void Enqueue(T value)
        {
            _tail = _tail.Add(value);
            Count++;
        }

        public T Dequeue()
        {
            try
            {
                Count--;
                return _head.Remove();
            }

            catch
            {
                throw new InvalidOperationException();
            }
        }
        public bool TryDequeue(out T item)
        {
            var p = _head;
            if (p != null)
            {
                item = p.Data;
                p = p.Next;
                return true;
            }
            item = default(T);
            return false;
        }
        public T Peek()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            var currentElement = this._head.Data;

            return currentElement;
        }
        public bool TryPeek(out T value)
        {
            if (_head != null)
            {
                value = _head.Data;
                return true;
            }
            value = default(T);
            return false;
        }
        public T[] ToArray()
        {
            var arrToReturn = new T[this.Count];
            var currentNode = this._head;
            var arrIndex = 0;
            while (currentNode != null)
            {
                arrToReturn[arrIndex] = currentNode.Data;
                arrIndex++;
                currentNode = currentNode.Next;
            }

            return arrToReturn;
        }
        public override string ToString()
        {
            return string.Join(" ", GetValues().Select(v => v.ToString()));
        }
        public string Reverse()
        {
            return string.Join(" ", GetValues().Select(v => v.ToString()).Reverse().ToArray());
        }

        private IEnumerable<T> GetValues()
        {
            var current = _head.Next;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        #region Node

        public class Node
        {
            T data;     // phần dữ liệu của Node
            Node next = null;  // next trỏ đến Node tiếp theo
                               // Contructor Node với dữ liệu t
            public Node()
            { }
            public Node(T t)
            {
                next = null;
                data = t;
            }
            // Định nghĩa các thuộc tính (Propeties)
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }
            public Node Add(T value)
            {
                Next = new Node(value);
                return Next;
            }
            public T Remove()
            {
                if (Next == null)
                {
                    throw new InvalidOperationException();
                }
                var ret = Next.Data;
                Next = Next.Next;
                return ret;
            }
        }

        #endregion
    }
}
