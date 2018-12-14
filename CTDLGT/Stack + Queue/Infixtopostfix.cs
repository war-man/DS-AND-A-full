using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    public class StackNode<T>
    {
        class Node
        {
            // Định nghĩa phần tử của danh sách là Node
            T data;  // phần dữ liệu của Node
            Node next;  // next trỏ đến Node tiếp theo

            // Contructor Node với dữ liệu t
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
        }

        Node head;
        public void Push(T x) //Thêm phần tử
        {
            Node n = new Node(x);
            n.Next = head;
            head = n;
        }
        public T Pop() //Lấy phần tử
        {
            Node n = head;
            head = n.Next;
            return n.Data;
        }
        public T Top() //Lấy phần tử đầu
        {
            Node n = head;
            return n.Data;
        }
        public bool IsEmpty()
        {
            if (head == null)
            {
                return true;
            }
            return false;
        }
        public void PrintStack()
        {
            Node p = head;
            while (p != null)
            {
                Console.Write(" " + p.Data);
                p = p.Next;
            }
        }
    }
    public partial class Infixtopostfix
    {
        public Infixtopostfix()
        {
        }
        public string InfixtoPosfix(string nhap)
        {
            var a = new StackNode<string>();
            var ketQuahauto = "";
            string tk;
            nhap = nhap.Trim(); //Cắt hết khoảng trắng ở đầu và cuối chuỗi
            var s = "";
            for (int i = 0; i < nhap.Length; i++)
            {
                if (nhap[i] != ' ')
                {
                    s += nhap[i];
                }
            }

            var demMoNgoac = 0;
            var demDongNgoac = 0;
            var demToanTu = 0;
            var demToanHang = 0;
            for (int i = 0; i < s.Length;)
            {
                GetTokken(s, out tk, ref i);
                if (tk[0] == '(')
                {
                    demMoNgoac++;
                }
                else if (tk[0] == ')')
                {
                    demDongNgoac++;
                }
                else if (LaToanTu(tk[0]))
                {
                    demToanTu++;
                }
                else
                {
                    demToanHang++;
                }
            }
            if (demDongNgoac == demMoNgoac && demToanHang - 1 == demToanTu)
            {
                for (int i = 0; i < s.Length;)
                {
                    GetTokken(s, out tk, ref i);
                    if (!LaToanTu(tk[0]))
                    {
                        ketQuahauto += tk + " ";
                    }
                    else
                    {
                        if (tk[0] == '(')
                        {
                            a.Push(tk);
                        }
                        else if (tk[0] == ')')
                        {
                            while (a.Top()[0] != '(')
                            {
                                ketQuahauto += a.Top() + " ";
                                a.Pop();
                            }
                            if (a.Top()[0] == '(')
                            {
                                a.Pop();
                            }
                        }
                        else
                        {
                            if (!a.IsEmpty() && DoUuTien(a.Top()[0]) < DoUuTien(tk[0]))
                            {
                                a.Push(tk);
                                continue;
                            }
                            else if (a.IsEmpty())
                            {
                                a.Push(tk);
                                continue;
                            }
                            while (!a.IsEmpty() && DoUuTien(a.Top()[0]) >= DoUuTien(tk[0]))
                            {
                                ketQuahauto += a.Top() + " ";
                                a.Pop();
                            }
                            a.Push(tk);
                        }
                    }
                }
                while (!a.IsEmpty())
                {
                    if (a.Top()[0] == '(')
                    {
                        a.Pop();
                        continue;
                    }
                    ketQuahauto += a.Top() + " ";
                    a.Pop();

                }
                return ketQuahauto;
            }
            else
            {
                return "\nBieu thuc sai";
            }
        }
        public void GetTokken(string chuoiNhap, out string tokken, ref int start)
        {
            tokken = "";
            for (; start < chuoiNhap.Length; start++)
            {
                if (LaToanTu(chuoiNhap[start]))
                {
                    if (tokken == "")
                    {
                        tokken += chuoiNhap[start];
                        start++;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    tokken += chuoiNhap[start];
                }
            }
        }
        public double TinhBieuThuc(string posfix)
        {
            string[] tt = posfix.Split(' ');
            var kq = new StackNode<string>();
            for (int i = 0; i < tt.Length - 1; i++)
            {
                if (!LaToanTu(tt[i][0]))
                {
                    kq.Push(tt[i]);
                }
                else
                {
                    string a = kq.Top();
                    kq.Pop();
                    string b = kq.Top();
                    kq.Pop();
                    kq.Push(TinhToan(tt[i], b, a));
                }
            }
            double ketqua = Convert.ToDouble(kq.Top());
            return ketqua;
        }
        public string TinhToan(string toanTu, string soHang1, string soHang2)
        {
            double a = Convert.ToDouble(soHang1);
            double b = Convert.ToDouble(soHang2);
            if (toanTu[0] == '+')
            {
                return Convert.ToString(a + b);
            }
            else if (toanTu[0] == '-')
            {
                return Convert.ToString(a - b);
            }
            else if (toanTu[0] == '*')
            {
                return Convert.ToString(a * b);
            }
            else if (toanTu[0] == '/' && b != 0)
            {
                return Convert.ToString(a / b);
            }
            return "";
        }
        public bool LaToanTu(char a)
        {
            if (a == '+' || a == '-' || a == '*' || a == '/' || a == '(' || a == ')')
            {
                return true;
            }
            return false;
        }
        public int DoUuTien(char a)
        {
            if (a == '+' || a == '-')
            {
                return 1;
            }
            else if (a == '*' || a == '/')
            {
                return 2;
            }
            return 0;
        }
    }
}
