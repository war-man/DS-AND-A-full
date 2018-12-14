using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.AVL
{
    public class AVL<T> : IEnumerable
    where T : IComparable
    {
        private Node root;
        public AVL()
        {
            Root = null;
        }
        public AVL(Node r)
        {
            Root = r;
        }
        public Node Root
        {
            get
            {
                return root;
            }

            set
            {
                root = value;
            }
        }
        public class Node
        {
            private T value;
            private Node right;
            private Node left;
            private Node parent;
            private int leftCounter;
            private int rightCounter;

            public Node(T v)
            {
                value = v;
                Right = null;
                Left = null;
                Parent = null;
                LeftCounter = 0;
                RightCounter = 0;
            }
            public Node(T v, Node r, Node l, Node p)
            {
                if (p != null && p.Value.Equals(v))
                {

                }
                else
                {
                    value = v;
                    Right = r;
                    Left = l;
                    Parent = p;
                    if (p != null && p.Value.CompareTo(v) > 0)
                    {
                        p.Left = this;
                    }
                    else if (p != null && p.Value.CompareTo(v) < 0)
                    {
                        p.Right = this;
                    }
                    if (Right != null)
                    {
                        Right.Parent = this;
                    }
                    if (Left != null)
                    {
                        Left.Parent = this;
                    }
                    CounterFactor(this);
                }
            }
            public Node() : this(default(T)) { }
            public Node(Node n) : this(n.Value, n.Right, n.Left, n.Parent) { }
            public T Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }

            public Node Right
            {
                get
                {
                    return right;
                }

                set
                {
                    right = value;
                }
            }

            public Node Left
            {
                get
                {
                    return left;
                }

                set
                {
                    left = value;
                }
            }

            public int LeftCounter
            {
                get
                {
                    return leftCounter;
                }

                set
                {
                    leftCounter = value;
                }
            }

            public int RightCounter
            {
                get
                {
                    return rightCounter;
                }

                set
                {
                    rightCounter = value;
                }
            }

            public Node Parent
            {
                get
                {
                    return parent;
                }

                set
                {
                    parent = value;
                }
            }

            public override string ToString()
            {
                return string.Format(Value.ToString());
            }

            protected internal int Balance
            {
                get
                {
                    return LeftCounter - RightCounter;
                }
            }
        }
        public static void CounterFactor(Node node)
        {
            if (node.Left != null)
            {
                node.Left.RightCounter = node.Left.Right != null ? Math.Max(node.Left.Right.LeftCounter, node.Left.Right.RightCounter) + 1 : 0;
                node.Left.LeftCounter = node.Left.Left != null ? Math.Max(node.Left.Left.LeftCounter, node.Left.Left.RightCounter) + 1 : 0;
                node.LeftCounter = Math.Max(node.Left.LeftCounter, node.Left.RightCounter) + 1;
            }
            else
            {
                node.LeftCounter = 0;
            }
            if (node.Right != null)
            {
                node.Right.RightCounter = node.Right.Right != null ? Math.Max(node.Right.Right.LeftCounter, node.Right.Right.RightCounter) + 1 : 0;
                node.Right.LeftCounter = node.Right.Left != null ? Math.Max(node.Right.Left.LeftCounter, node.Right.Left.RightCounter) + 1 : 0;
                node.RightCounter = Math.Max(node.Right.LeftCounter, node.Right.RightCounter) + 1;
            }
            else
            {
                node.RightCounter = 0;
            }
        }
        public void Insert(T value)
        {
            if (root == null)
            {
                root = new Node(value);
                return;
            }
            else
            {
                var current = root;
                var previous = root;
                while (current != null)
                {
                    if (current.Value.CompareTo(value) == 0)
                    {
                        return;
                    }
                    else if (current.Value.CompareTo(value) > 0)
                    {
                        previous = current;
                        current = current.Left;
                    }
                    else
                    {
                        previous = current;
                        current = current.Right;
                    }
                }
                current = new Node(value, null, null, previous);
                Node refactor = new Node(current);
                while (refactor != null)
                {
                    CounterFactor(refactor);
                    refactor = refactor.Parent;
                }
                CounterFactor(current);
                Balance(current);
                //while(current != null)
                //{
                //    CounterFactor(current);
                //    current = current.Parent;
                //}
            }
        }
        public void Delete(T value)
        {
            var nodeToDelete = Find(value);
            if (nodeToDelete != null)
            {
                var parent = nodeToDelete.Parent;
                if (nodeToDelete.Left == null && nodeToDelete.Right == null)
                {
                    nodeToDelete = null;
                    if (parent != null && parent.Left.Value.Equals(value))
                    {
                        parent.Left = null;
                        parent.LeftCounter--;
                    }
                    else if (parent != null)
                    {
                        parent.Right = null;
                        parent.RightCounter--;
                    }
                }
                else if (nodeToDelete.Left == null && nodeToDelete.Right != null)
                {
                    nodeToDelete = new Node(nodeToDelete.Right.Value, null, null, parent);
                }
                else
                {
                    var current = nodeToDelete.Left;
                    while (current != null)
                    {
                        if (current.Left == null && current.Right == null)
                        {
                            nodeToDelete.Value = current.Value;
                            if (parent != null)
                            {
                                parent.RightCounter--;
                                parent.Right = null;
                            }
                            current = null;
                        }
                        else if (current.Left != null && current.Left.Right != null && current.Right == null)
                        {
                            parent = current;
                            current = current.Left;
                        }
                        else if (current.Right != null)
                        {
                            parent = current;
                            current = current.Right;
                        }
                        else
                        {
                            nodeToDelete.Value = current.Value;
                            if (parent != null)
                            {
                                parent.Right = current.Left;
                            }
                            current.Left.Parent = parent;
                            current = null;
                        }
                    }
                }
                var refactor = parent;
                while (refactor != null)
                {
                    CounterFactor(refactor);
                    refactor = refactor.Parent;
                }
                if (parent != null)
                {
                    CounterFactor(parent);
                    Balance(parent);
                }
                BalanceAll();
                while (parent != null)
                {
                    CounterFactor(parent);
                    parent = parent.Parent;
                }
            }
        }
        public Node Find(T value)
        {
            var current = root;
            while (current != null)
            {
                if (value.CompareTo(current.Value) > 0)
                {
                    current = current.Right;
                }
                else if (value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }
        public void Clear()
        {
            foreach (Node item in this)
            {
                Delete(item.Value);
            }
            root = null;
        }
        public IEnumerator GetEnumerator()
        {
            var q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Any())
            {
                var current = q.Dequeue();
                yield return current;
                if (current.Left != null)
                {
                    q.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    q.Enqueue(current.Right);
                }
            }
        }
        private void Balance(Node start)
        {
            var current = start;
            while (current != null)
            {
                if (current.Balance < -1 || current.Balance > 1)
                {
                    Rotate(current);
                }
                current = current.Parent;
            }
        }
        public void BalanceAll()
        {
            foreach (Node node in this)
            {
                if (node.Balance < -1 || node.Balance > 1)
                {
                    Rotate(node);
                }
            }
        }
        private void Rotate(Node node)
        {
            if (node.Balance > 1)
            {
                if (node.Left.Balance > 1 || node.Left.Right == null)
                {
                    var l = new Node(node.Left.Left);
                    var r = new Node(node);
                    var isRoot = false;
                    if (node == root)
                    {
                        isRoot = true;
                    }
                    T rotatedValue = r.Left.Value;
                    if (node.Left.Right != null)
                    {
                        node.Left.Right.Parent = r;
                    }
                    r.Left = node.Left.Right;
                    node = new Node(rotatedValue, r, l, r.Parent);
                    if (isRoot)
                    {
                        root = node;
                    }
                    CounterFactor(node);
                }
                else //if(node.Left.Balance < -1 || node.Left.Left == null)
                {
                    var l = new Node(node.Left);
                    var r = new Node(node);
                    var isRoot = false;
                    if (node == root)
                    {
                        isRoot = true;
                    }
                    T rotatedValue = l.Right.Value;
                    if (node.Left.Right.Right != null)
                    {
                        node.Left.Right.Right.Parent = r;
                    }
                    r.Left = node.Left.Right.Right;
                    if (node.Left.Right.Left != null)
                    {
                        node.Left.Right.Left.Parent = l;
                    }
                    l.Right = node.Left.Right.Left;
                    node = new Node(rotatedValue, r, l, r.Parent);
                    if (isRoot)
                    {
                        root = node;
                    }
                    CounterFactor(node);
                }
            }
            else
            {
                if (node.Right.Balance < -1 || node.Right.Left == null)
                {
                    var r = new Node(node.Right.Right);
                    var l = new Node(node);
                    var isRoot = false;
                    if (node == root)
                    {
                        isRoot = true;
                    }
                    var rotatedValue = l.Right.Value;
                    if (node.Right.Left != null)
                    {
                        node.Right.Left.Parent = l;
                    }
                    l.Right = node.Right.Left;
                    node = new Node(rotatedValue, r, l, l.Parent);
                    if (isRoot)
                    {
                        root = node;
                    }
                    CounterFactor(node);
                }
                else if (node.Right.Balance > 1 || node.Right.Right == null)
                {
                    var r = new Node(node.Right);
                    var l = new Node(node);
                    var isRoot = false;
                    if (node == root)
                    {
                        isRoot = true;
                    }

                    T rotatedValue = r.Left.Value;
                    if (node.Right.Left.Left != null)
                    {
                        node.Right.Left.Left.Parent = l;
                    }
                    l.Right = node.Right.Left.Left;
                    if (node.Right.Left.Right != null)
                    {
                        node.Right.Left.Right.Parent = r;
                    }
                    r.Left = node.Right.Left.Right;
                    node = new Node(rotatedValue, r, l, l.Parent);
                    if (isRoot)
                    {
                        root = node;
                    }
                    CounterFactor(node);
                }
            }
        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                if (current.Left != null)
                {
                    InOrderDisplayTree(current.Left);
                }
                Console.Write("{0}, ", current.Value);
                if (current.Right != null)
                {
                    //temp = temp.Right;
                    InOrderDisplayTree(current.Right);
                }
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.Left);
                int r = getHeight(current.Right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.Left);
            int r = getHeight(current.Right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }

    }
}
