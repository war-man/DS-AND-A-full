using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Danh_sách_liên_kết
{
    class Node
    {
        private Node pNext = null;
        internal Node PNext
        {
            get
            {
                return pNext;
            }

            set
            {
                pNext = value;
            }
        }
        string tensv;

        public string Tensv
        {
            get
            {
                return tensv;
            }

            set
            {
                tensv = value;
            }
        }

        string diachi;

        public string Diachi
        {
            get
            {
                return diachi;
            }

            set
            {
                diachi = value;
            }
        }

        string lop;

        public string Lop
        {
            get
            {
                return lop;
            }

            set
            {
                lop = value;
            }
        }

        int khoa;

        public int Khoa
        {
            get
            {
                return khoa;
            }

            set
            {
                khoa = value;
            }
        }
        public Node(string ten = null, string dc = null, string lp = null, int kh = 0)
        {
            tensv = ten; diachi = dc; lop = lp; khoa = kh;
        }
        public Node(string ten)
        {
            Tensv = ten;
        }

    }
    class LinkedList
    {
        private Node pHead = null;
        private Node pTail = null;
        internal Node PHead
        {
            get
            {
                return pHead;
            }

            set
            {
                pHead = value;
            }
        }

        internal Node PTail
        {
            get
            {
                return pTail;
            }

            set
            {
                pTail = value;
            }
        }
        //----------------------------
        public int Length()
        {
            var size = 0;
            var P = PHead;
            while (P != null)
            {
                size++;
                P = P.PNext;
            }
            return size;
        }
        public bool IsEmpty() // Kiem tra danh sach rong
        {
            if (PHead == null)
                return true;
            return false;
        }

        //----------------------------
        public void AddHead(Node pNew) // Them Node vao dau danh sach
        {
            if (IsEmpty() == true)
                PHead = PTail = pNew;
            else
                pNew.PNext = PHead;
            PHead = pNew;
        }

        //----------------------------

        public void AddTail(Node pNew) // Them Node vao cuoi danh sach
        {
            if (IsEmpty() == true)
                PHead = PTail = pNew;
            else
                pNew.PNext = PHead;
            PHead = pNew;
        }
        public void RemoveFromStart()
        {
            var tmp = Length();
            if (tmp > 0)
            {
                PHead = PHead.PNext;// chuyen con tro cua cai head dau tien qua cai head tiep theo
                tmp --;
            }
            else
            {
                Console.WriteLine("No element exist in this linked list.");
            }
        }
        public void RemoveAt(int k)
        {
            var P = PHead;
            var i = 1;
            var size = Length();
            if (k < 1 && k > size) Console.WriteLine("Vi tri ko hop le");
            else
            {
                if (k == 1) RemoveFromStart();
                else
                {
                    while (P != null && i != k - 1) //duyet den vi tri k-1
                    {
                        P = P.PNext;
                        i++; // khi i = k -1 thì i  sẽ dừng
                    }
                    P.PNext = P.PNext.PNext; //cho P tro sang Node ke tiep vi tri k
                }
            }
        }

        public void Remove1(Node t)
        {
            var ps = SearchNode1(t);
            if (ps == 0)
                return;
            else
            {
                RemoveAt(ps);
            }
        }
        public int SearchNode1(Node pNew)
        {
            var temp = PHead;
            var i = 1;
            while (temp != null && temp.Tensv != pNew.Tensv)
            {
                temp = temp.PNext;
                i++;
            }
            if (temp != null)
            {
                return i;
            }
            return 0;
        }
        public void Remove2(Node t)
        {
            var ps = SearchNode2(t);
            if (ps == 0)
                return;
            else
            {
                RemoveAt(ps);
            }
        }
        public int SearchNode2(Node pNew)
        {
            var temp = PHead;
            var i = 1;
            while (temp != null && temp.Lop != pNew.Lop)
            {
                temp = temp.PNext;
                i++;
            }
            if (temp != null)
            {
                return i;
            }
            return 0;
        }
        public void Remove3(Node t)
        {
            var ps = SearchNode3(t);
            if (ps == 0)
                return;
            else
            {
                RemoveAt(ps);
            }
        }
        public int SearchNode3(Node pNew)
        {
            var temp = PHead;
            var i = 1;
            while (temp != null && temp.Diachi != pNew.Diachi)
            {
                temp = temp.PNext;
                i++;
            }
            if (temp != null)
            {
                return i;
            }
            return 0;
        }
        public static void FileToList(LinkedList ds)
        {
            using (var sr = new StreamReader(@"dssv.txt"))
            {
                var n = int.Parse(sr.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    var ten = sr.ReadLine();
                    var dc = sr.ReadLine();
                    var lp = sr.ReadLine();
                    var kh = int.Parse(sr.ReadLine());
                    var sv = new Node(ten, dc, lp, kh);
                    ds.AddHead(sv);
                }
            }
        }
        public static void PrintListSV(LinkedList ds)
        {
            Console.WriteLine("DANH SACH SINH VIEN");
            Node p = ds.pHead;
            while (p != null)
            {
                Console.WriteLine(" {0,-20}     {1,-20}     {2,-10}     {3}", p.Tensv, p.Diachi, p.Lop, p.Khoa);
                Console.WriteLine("------------------>>");
                p = p.PNext;
            }
            Console.WriteLine("null");
            Console.WriteLine();
        }
        public static void SearchSinhVien(LinkedList ds)
        {
            Console.WriteLine("1. Search theo ten" +
            Environment.NewLine + "2. Search theo lop" +
            Environment.NewLine + "3. Search theo dia chi");
            Console.Write("LUA CHON: ");
            var ans = Console.ReadLine();
            var choice = 0;
            if (int.TryParse(ans, out choice))
            {
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Nhap ten sinh vien can tim: ");
                            var tmp = Console.ReadLine();
                            var a = new Node(tmp, null, null, 0);
                            if (ds.SearchNode1(a) >= 1)
                            {
                                Console.WriteLine("=> Tim thay sinh vien co ten {0} trong danh sach o vi tri {1} !!!", tmp, ds.SearchNode1(a));
                            }
                            else
                            {
                                Console.WriteLine("=> Khong tim thay ");
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Nhap lop sinh vien can tim: ");
                            var tmp = Console.ReadLine();
                            var a = new Node(null, null, tmp, 0);
                            if (ds.SearchNode2(a) >= 1)
                            {
                                Console.WriteLine("=> Tim thay sinh vien hoc lop {0} trong danh sach o vi tri {1} !!!", tmp, ds.SearchNode2(a));
                            }
                            else
                            {
                                Console.WriteLine("=> Khong tim thay ");
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Nhap dia chi sinh vien can tim: ");
                            var tmp = Console.ReadLine();
                            var a = new Node(null, tmp, null, 0);
                            if (ds.SearchNode3(a) >= 1)
                            {
                                Console.WriteLine("=> Tim thay sinh vien co dia chi {0} trong danh sach o vi tri {1} !!!", tmp, ds.SearchNode3(a));
                            }
                            else
                            {
                                Console.WriteLine("=> Khong tim thay ");
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Wrong selection!!!" +
                            Environment.NewLine + "Press any kay for exit");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("You must type numeric value only!!!" +
                    Environment.NewLine + "Press any key for exit");
                Console.ReadKey();
            }
        }

        public static void RemoveSinhVien(LinkedList ds)
        {
            Console.WriteLine("1. Xoa theo lop" +
            Environment.NewLine + "2. Xoa theo ten" +
            Environment.NewLine + "3. Xoa theo dia chi");
            Console.Write("LUA CHON: ");
            var ans = Console.ReadLine();
            var choice = 0;
            if (int.TryParse(ans, out choice))
            {
                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Nhap sinh vien co lop can xoa: ");
                            var tmp = Console.ReadLine();
                            ds.Remove2(new Node(null, null, tmp, 0));
                            Console.WriteLine("=> DA XOA");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Nhap sinh vien co ten can xoa: ");
                            var tmp = Console.ReadLine();
                            ds.Remove1(new Node(tmp, null, null, 0));
                            Console.WriteLine("=> DA XOA");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Nhap sinh vien co dia chi can xoa: ");
                            var tmp = Console.ReadLine();
                            ds.Remove3(new Node(null, tmp, null, 0));
                            Console.WriteLine("=> DA XOA");
                            break;
                        }
                    default:
                        Console.WriteLine("Wrong selection!!!" +
                            Environment.NewLine + "Press any kay for exit");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("You must type numeric value only!!!" +
                    Environment.NewLine + "Press any key for exit");
                Console.ReadKey();
            }
        }
    }
}
