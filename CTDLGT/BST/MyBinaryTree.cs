using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.BST
{
    public class MyBinaryTree<T> where T : IComparable<T>
    {

        public MyTNode<T> root = null;
        public MyTNode<T> Root
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
        public bool Insert(T x)
        {
            if (root == null)
            {
                root = new MyTNode<T>(x);
                return true;
            }
            else
            {
                if (root.InsertData(x))
                    return true;

            }
            return false;
        }
        public MyTNode<T> SearchNode(T x)
        {
            if (root == null)
                return null;
            return root.SearchX(x);
        }
        public bool Find(T inputValue)
        {
            var current = root;
            while (current != null)
            {
                if (current.Data.Equals(inputValue))
                {
                    return true;
                }
                else if (current.Data.CompareTo(inputValue) > 0)
                {
                    current = current.PLeft;
                }
                else
                {
                    current = current.PRight;
                }
            }
            return false;
        }


        public void PreOrder()
        {
            if (root == null)
                return;
            root.NLR();
        }
        public void InOrder()
        {
            if (root == null)
                return;
            root.LNR();
        }
        public void PostOrder()
        {
            if (root == null)
                return;
            root.LRN();
        }
        //public MyTNode<T> Max()
        //{
        //    if (root == null)
        //        return null;
        //    return root.RightMost();
        //}
        //public MyTNode<T> Min()
        //{
        //    if (root == null)
        //        return null;
        //    return root.LeftMost();
        //}
        //xóa một node trên cây
        public void Remove(ref MyTNode<T> Parent, T key)
        {
            if (Parent == null) return;
            if (key.CompareTo(Parent.Data) < 0)
                Remove(ref Parent.pLeft , key);
            else if (key.CompareTo(Parent.Data) > 0)
                Remove(ref Parent.pRight, key);
            else if (Parent.pLeft == null && Parent.pRight == null)
            {
                Parent = null;
            }
            else if (Parent.pLeft == null && Parent.pRight != null)
            {
                Parent = Parent.pRight;
            }
            else if (Parent.pLeft != null && Parent.pRight == null)
            {
                Parent = Parent.pLeft;
            }
            else
            {
                var p = Parent.pLeft;
                while (p.pRight != null) p = p.pRight;
                Parent.data = p.data;
                Remove(ref Parent.pLeft, p.data);
            }
        }
        public MyTNode<T> MaxRightOfLeft()
        {
            if (root == null)
                return null;
            var p = root.PLeft;
            //while (p.PRight != null) p = p.PRight;
            return p.RightMost();
        }
        public MyTNode<T> MaxLeftOfRight()
        {
            if (root == null)
                return null;
            var p = root.PRight;
            //while (p.PLeft != null) p = p.PLeft;
            return p.LeftMost();
        }
        public int ChieuCaoTree()
        {
            return root.ChieuCao(root);
        }
        public int Demsonutla()
        {
            return root.CountLeaf(root);
        }
        public static class BTreePrinter
        {
            class NodeInfo
            {
                public MyTNode<T> Node;
                public string Text;
                public int StartPos;
                public int Size { get { return Text.Length; } }
                public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
                public NodeInfo Parent, Left, Right;
            }

            public static void Print(MyTNode<T> root, int topMargin = 3, int leftMargin = 3)
            {
                if (root == null) return;
                var rootTop = Console.CursorTop + topMargin;
                var last = new List<NodeInfo>();
                var next = root;
                for (int level = 0; next != null; level++)
                {
                    var item = new NodeInfo { Node = next, Text = next.Data.ToString() };
                    if (level < last.Count)
                    {
                        item.StartPos = last[level].EndPos + 1;
                        last[level] = item;
                    }
                    else
                    {
                        item.StartPos = leftMargin;
                        last.Add(item);
                    }
                    if (level > 0)
                    {
                        item.Parent = last[level - 1];
                        if (next == item.Parent.Node.PLeft)
                        {
                            item.Parent.Left = item;
                            item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                        }
                        else
                        {
                            item.Parent.Right = item;
                            item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                        }
                    }
                    next = next.PLeft ?? next.PRight;
                    for (; next == null; item = item.Parent)
                    {
                        Print(item, rootTop + 2 * level);
                        if (--level < 0) break;
                        if (item == item.Parent.Left)
                        {
                            item.Parent.StartPos = item.EndPos;
                            next = item.Parent.Node.PRight;
                        }
                        else
                        {
                            if (item.Parent.Left == null)
                                item.Parent.EndPos = item.StartPos;
                            else
                                item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                        }
                    }
                }
                Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
            }

            private static void Print(NodeInfo item, int top)
            {
                SwapColors();
                Print(item.Text, top, item.StartPos);
                SwapColors();
                if (item.Left != null)
                    PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
                if (item.Right != null)
                    PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
            }

            private static void PrintLink(int top, string start, string end, int startPos, int endPos)
            {
                Print(start, top, startPos);
                Print("─", top, startPos + 1, endPos);
                Print(end, top, endPos);
            }

            private static void Print(string s, int top, int left, int right = -1)
            {
                Console.SetCursorPosition(left, top);
                if (right < 0) right = left + s.Length;
                while (Console.CursorLeft < right) Console.Write(s);
            }

            private static void SwapColors()
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = Console.BackgroundColor;
                Console.BackgroundColor = color;
            }
        }
        public void Print()
        {
            BTreePrinter.Print(root);
        }
        public void DeleteTree()
        {
            root = null;
        }
        public int CountChildren()
        {
            return root.CountChildren(root);
        }
        public int Count_nut_co_1_con(MyTNode<T> node)
        {
            if (node == null) return 0;
            if ((node.PLeft != null && node.PRight == null) || (node.PLeft == null && node.PRight != null))
                return 1 + Count_nut_co_1_con(node.PLeft) + Count_nut_co_1_con(node.PRight);
            else return Count_nut_co_1_con(node.PLeft) + Count_nut_co_1_con(node.PRight);
        }
        public MyTNode<T> LCA(T a,T b)
        {
            if (root == null)
                return null;
            return root.SearchLCA(root,a,b);
        }
    }
}
