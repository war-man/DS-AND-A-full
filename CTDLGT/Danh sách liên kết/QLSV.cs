using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;

namespace CTDLGT.Danh_sách_liên_kết
{
    public class SV : IComparable<SV>
    {
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

        public SV()
        {
            tensv = ""; diachi = ""; lop = ""; khoa = 0;
        }
        public SV(string ten, string dc, string lp, int kh)
        {
            tensv = ten; diachi = dc; lop = lp; khoa = kh;
        }
        public SV(string ten)
        {
            tensv = ten;
        }
        //Nhập thông tin một sinh viên
        public void Input()
        {
            Console.Write("Nhap ten SV: ");
            tensv = Convert.ToString(Console.ReadLine());
            Console.Write("Nhap Dia chi : ");
            diachi = Convert.ToString(Console.ReadLine());
            Console.Write("Nhap lop : ");
            lop = Convert.ToString(Console.ReadLine());
            Console.Write("Nhap Khoa : ");
            khoa = Convert.ToInt32(Console.ReadLine());
        }
        // Xuất thông tin một sinh viên
        public void PrintSV()
        {
            //Console.WriteLine();
            Console.WriteLine(" {0,-20}     {1,-20}     {2,-10}     {3}", tensv, diachi, lop, khoa);
        }

        public int CompareTo(SV other)
        {
            return string.Compare(this.tensv, other.tensv);
        }
    }
    public class QLSV
    {
        public QLSV()
        {
        }
        // phần trình bày dữ liệu cụ thể SV để áp dụng bài 21
        // SV : dữ liệu cho một Node
        
        internal class SV_SortByNameByAscendingOrder : IComparer<SV>
        {
            public int Compare(SV x, SV y)
            {
                return string.Compare(getLastName(x.Tensv), getLastName(y.Tensv));
            }
        }
        public static string getLastName(String fullName)
        {
            var names = fullName.Split(' ').ToList();
            var firstName = names.Last();

            return firstName;
        }
        internal class SV_SortByDiachiByAscendingOrder : IComparer<SV>
        {
            public int Compare(SV x, SV y)
            {
                return string.Compare(x.Diachi, y.Diachi);
            }
        }
        internal class SV_SortByLopByAscendingOrder : IComparer<SV>
        {
            public int Compare(SV x, SV y)
            {
                return string.Compare(x.Lop, y.Lop);
            }
        }
        internal class SV_SortByKhoaByAscendingOrder : IComparer<SV>
        {
            public int Compare(SV x, SV y)
            {
                if (x.Khoa > y.Khoa) return 1;
                else if (x.Khoa < y.Khoa) return -1;
                else
                    return 0;
            }
        }
        public static void Addrange(GenericList<SV> ds, GenericList<SV> ds2)
        {
            foreach (SV item in ds2)
            {
                ds.AddLast(item);
            }
        }
        // Các phương thức cho danh sách
        // Nhập danh sách
        public static void InputListSV(GenericList<SV> ds)
        {
            Console.Write("Nhap si so SV = ");
            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var sv = new SV();
                sv.Input();
                ds.Addhead(sv);
            }
        }
        // Xuất danh sách liên kết, lúc này dữ liệu cụ thể mô tả trong SV
        public static void PrintListSV(GenericList<SV> ds)
        {
            Console.WriteLine("DANH SACH SINH VIEN");
            foreach (SV x in ds)
                x.PrintSV();
            Console.WriteLine();
        }
        // Đọc file dssv.txt ra dslk 
        // file dssv.txt lưu trong thư mục Textfile
        public static void FileToList(GenericList<SV> ds)
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
                    var sv = new SV(ten, dc, lp, kh);
                    ds.Addhead(sv);
                }
            }

        }
        public static void SearchSinhVien(GenericList<SV> ds)
        {
            Console.WriteLine("Nhap ten sinh vien can tim: ");
            var tmp = Console.ReadLine();
            var a = new SV("Le Van Minh");
            if (ds.SearchNode1(a) >= 1)
            {
                Console.WriteLine("=> Tim thay sinh vien co ten {0} trong danh sach !!!", tmp);
            }
            else
            {
                Console.WriteLine("=> Khong tim thay ");
            }
        }
        
        public static void Sort(GenericList<SV> ds)
        {
            Console.WriteLine("1. sap xep theo lop" +
            Environment.NewLine + "2. sap xep theo ten" +
            Environment.NewLine + "3. sap xep theo dia chi");
            var ans = Console.ReadLine();
            var choice = 0;
            if (int.TryParse(ans, out choice))
            {
                switch (choice)
                {
                    case 1:
                        {
                            var tmp = new SV_SortByLopByAscendingOrder();
                            ds.SelectionSort(tmp);
                            Console.WriteLine("=> DA SAP XEP THEO LOP");
                            PrintListSV(ds);
                            break;
                        }
                    case 2:
                        {
                            var tmp = new SV_SortByNameByAscendingOrder();
                            ds.SelectionSort(tmp);
                            Console.WriteLine("=> DA SAP XEP THEO TEN");
                            PrintListSV(ds);
                            break;
                        }
                    case 3:
                        {
                            var tmp = new SV_SortByDiachiByAscendingOrder();
                            ds.SelectionSort(tmp);
                            Console.WriteLine("=> DA SAP XEP THEO DIA CHI");
                            PrintListSV(ds);
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
