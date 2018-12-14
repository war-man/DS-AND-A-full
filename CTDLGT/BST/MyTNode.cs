using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.BST
{
    public class MyTNode<T> where T : IComparable<T>
    {
        public T data;
        public MyTNode<T> pLeft = null;
        public MyTNode<T> pRight = null;

        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public MyTNode<T> PLeft
        {
            get
            {
                return pLeft;
            }
            set
            {
                pLeft = value;
            }
        }
        public MyTNode<T> PRight
        {
            get
            {
                return pRight;
            }
            set
            {
                pRight = value;
            }
        }

        public MyTNode(T x)
        {
            Data = x;
            PLeft = null;
            pRight = null;
        }
        public bool InsertData(T x)
        {
            if (x.CompareTo(Data) == 0)
                return false;
            if (x.CompareTo(Data) < 0)
            {
                if (PLeft == null)
                    PLeft = new MyTNode<T>(x);
                else return PLeft.InsertData(x);
            }
            else
            {
                if (pRight == null)
                    pRight = new MyTNode<T>(x);
                else return pRight.InsertData(x);
            }
            return true;
        }
        /// <summary>
        /// Các cách duyệt cây
        /// </summary>
        public void NLR()
        {
            Console.Write(Data + "; ");
            if (PLeft != null)
                PLeft.NLR();
            if (pRight != null)
                pRight.NLR();
        }
        public void LNR()
        {
            if (PLeft != null)
                PLeft.NLR();
            Console.Write(Data + "; ");
            if (pRight != null)
                pRight.NLR();
        }
        public void LRN()
        {
            if (PLeft != null)
                PLeft.NLR();
            if (pRight != null)
                pRight.NLR();
            Console.Write(Data + "; ");
        }

        public MyTNode<T> SearchX(T x)
        {
            if (Data.CompareTo(x) == 0)
                return this;
            if (Data.CompareTo(x) < 0)
            {
                if (PLeft == null)
                    return null;
                return PLeft.SearchX(x);
            }
            else
            {
                if (pRight == null)
                    return null;
                return pRight.SearchX(x);
            }
        }
        public MyTNode<T> RightMost()
        {
            if (pRight == null)
                return this;
            return pRight.RightMost();
        }
        public MyTNode<T> LeftMost()
        {
            if (PLeft == null)
                return this;
            return PLeft.LeftMost();
        }
        public int ChieuCao(MyTNode<T> c)
        {
            if (c != null)
            {
                var a = ChieuCao(PLeft);
                var b = ChieuCao(pRight);
                var max = (a > b) ? a : b;
                return 1 + max;
                //return Math.Max(ChieuCao(pLeft), ChieuCao(pRight)) + 1;
            }
            return 0;
        }
        //Kiem tra  nut la
        public bool IsLeaf(MyTNode<T> T)
        {
            return (T.PLeft == null) && (T.PRight == null);
        }
        public int CountLeaf(MyTNode<T> T)
        {
            if (T == null)
                return 0;
            else
               if (IsLeaf(T))
                return 1;
            else
                return CountLeaf(T.PLeft) + CountLeaf(T.PRight);
        }
        public int CountChildren(MyTNode<T> node)
        {
            if (node == null) return 0;
            return ((node.PLeft == null) ? 0 : CountChildren(node.PLeft) + 1) + ((node.PRight == null) ? 0 : CountChildren(node.PRight) + 1);
        }
        public MyTNode<T> SearchLCA(MyTNode<T> searchTree, T compareValue1, T compareValue2)
        {
            if (searchTree == null)
            {
                return null;
            }
            if (searchTree.Data.CompareTo(compareValue1) > 0 && searchTree.Data.CompareTo(compareValue2) > 0)
            {
                return SearchLCA(searchTree.PLeft, compareValue1, compareValue2);
            }
            if (searchTree.Data.CompareTo(compareValue1) < 0 && searchTree.Data.CompareTo(compareValue2) < 0)
            {
                return SearchLCA(searchTree.PRight, compareValue1, compareValue2);
            }
            return searchTree;
        }
        /*1) Tìm đường dẫn từ gốc đến n1 và lưu nó trong một vectơ hoặc mảng.
            2) Tìm đường dẫn từ gốc đến n2 và lưu trữ nó trong một vectơ hoặc mảng khác.
        3) Di chuyển cả hai đường dẫn cho đến khi các giá trị trong mảng giống nhau. Trả về phần tử phổ biến ngay trước khi không khớp.
        */
        public T MinValue()
        {
            if (pLeft == null)
                return data;
            return pLeft.MinValue();
        }
        public T MaxValue()
        {
            if (pRight == null)
                return data;
            return pRight.MaxValue();
        }
    }


}
