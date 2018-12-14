/*
***********************************************
*                                             *
* CTDLGT.cs           2018                    *
* Môn: Cấu trúc dữ liệu giải thuật TH         *
* Giáo viên: Thầy Bình                        *
* Bài Kiểm tra cuối môn                       *
* Sinh viên: Nguyễn Minh Đức Khôi             *
* MSSV: 17DH111108                            *
* lớp : TH1715                                *
*                                             *
***********************************************
*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;



using CTDLGT.Menu;
using CTDLGT.Sort;
using CTDLGT.Danh_sách_liên_kết;
using CTDLGT.BST;
using CTDLGT.Stack___Queue;
using CTDLGT.AVL;

namespace CTDLGT
{
	class IntComparer : IComparer<int>
	{
		public int Compare(int x, int y)
		{
			return x - y;
		}
	}
	class Program
	{
		static void Main()
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			var headerText = @"
	_____ _______ _____  _         _____ _______      _______ ______  _____ _______   __  __ ______ _   _ _    _ 
  / ____|__   __|  __ \| |       / ____|__   __|    |__   __|  ____|/ ____|__   __| |  \/  |  ____| \ | | |  | |
 | |       | |  | |  | | |      | |  __   | |          | |  | |__  | (___    | |    | \  / | |__  |  \| | |  | |
 | |       | |  | |  | | |      | | |_ |  | |          | |  |  __|  \___ \   | |    | |\/| |  __| | . ` | |  | |
 | |____   | |  | |__| | |____  | |__| |  | |          | |  | |____ ____) |  | |    | |  | | |____| |\  | |__| |
  \_____|  |_|  |_____/|______|  \_____|  |_|          |_|  |______|_____/   |_|    |_|  |_|______|_| \_|\____/ 
																												
																																 
																 ";


			Console.Clear();

			// Setup the menu
			ConsoleMenu mainMenu = new ConsoleMenu();

			ConsoleMenu subMenu1 = new ConsoleMenu("==>");
			subMenu1.SubTitle = "---------------- Sort Menu -----------------";
			subMenu1.addMenuItem(0, "backToMain", subMenu1.hideMenu);
			subMenu1.addMenuItem(1, "Quick Sort", QuickSort);
			subMenu1.addMenuItem(2, "Shell Sort", ShellSort);
			subMenu1.addMenuItem(3, "Heap Sort", HeapSort);

			subMenu1.ParentMenu = mainMenu;
				
			mainMenu.Header = headerText;
			subMenu1.Header = mainMenu.Header;

			ConsoleMenu subMenu2 = new ConsoleMenu("==>");
			subMenu2.SubTitle = "---------------- Linked List Menu -----------------";
			subMenu2.addMenuItem(0, "backToMain", subMenu2.hideMenu);
			subMenu2.addMenuItem(1, "Linked List", TestLL);
			subMenu2.addMenuItem(2, "Danh sach sinh vien", TestDSSV);

			subMenu2.ParentMenu = mainMenu;

			mainMenu.Header = headerText;
			subMenu2.Header = mainMenu.Header;

			ConsoleMenu subMenu3 = new ConsoleMenu("==>");
			subMenu3.SubTitle = "---------------- Stack & Queue Menu -----------------";
			subMenu3.addMenuItem(0, "backToMain", subMenu3.hideMenu);
			subMenu3.addMenuItem(1, "Stack & cac bai tap ap dung", TestStack);
			subMenu3.addMenuItem(2, "Queue & cac bai tap ap dung", TestQueue);

			subMenu3.ParentMenu = mainMenu;

			mainMenu.Header = headerText;
			subMenu3.Header = mainMenu.Header;

			mainMenu.SubTitle = "-------------------- Menu Test (90 min) ----------------------" +"\n"+
				"[Su dung con tro chuot de lua chon yeu cau can thuc hien]";
			mainMenu.addMenuItem(0, "1. Sort(Quick Sort,Shell Sort,Heap Sort)", subMenu1.showMenu);
			mainMenu.addMenuItem(1, "2. Generic(Linked List,Student Management)", subMenu2.showMenu);
			mainMenu.addMenuItem(2, "3. Generic Stack Queue and Aplication", subMenu3.showMenu);
			mainMenu.addMenuItem(3, "4. Generic BST tree", TestCÂY);
			mainMenu.addMenuItem(4, "5. Generic AVL tree", HelloWorld);
			mainMenu.addMenuItem(5, nameof(Exit), Exit);
			// Display the menu
			mainMenu.showMenu();
		}


		public static void Exit()
		{
			Environment.Exit(0);
		}
		#region Sort
		public static void QuickSort()
		{
			int[] arr;
			FileToArr(out arr);
			Console.WriteLine("- Danh sach ban dau lay tu File:");
			Console.WriteLine("[{0}]", string.Join(", ",arr));
			Console.WriteLine("- Danh sach sau khi duoc sap xep:");
			var sw = Stopwatch.StartNew();
			var sorter = new QSort<int>(arr);
			sorter.QuickSort(0, arr.Length - 1);
			sw.Stop();
			Console.WriteLine("[{0}]", string.Join(", ", arr));
			Console.WriteLine("Time taken QuickSort: {0} ms", sw.Elapsed.TotalMilliseconds);
			var sw1 = Stopwatch.StartNew();
			var sorter1 = new QSort<int>(arr);
			sorter1.QuickSortDescending(0, arr.Length - 1);
			sw1.Stop();
			Console.WriteLine("[{0}]", string.Join(", ", arr));
			Console.WriteLine("Time taken QuickSort: {0} ms", sw1.Elapsed.TotalMilliseconds);
			Console.ReadKey(true);
		}
		public static void HeapSort()
		{
			int[] arr;
			FileToArr(out arr);
			Console.WriteLine("- Danh sach ban dau lay tu File:");
			Console.WriteLine("[{0}]", string.Join(", ", arr));
			Console.WriteLine("- Danh sach sau khi duoc sap xep:");
			var sw1 = Stopwatch.StartNew();
			var sorter = new HeapSort();
			sorter.Sort(arr);
			sw1.Stop();
			Console.WriteLine("[{0}]", string.Join(", ", arr));
			Console.WriteLine("Time taken HeapSort: {0} ms", sw1.Elapsed.TotalMilliseconds);
			Console.ReadKey(true);
		}
		public static void ShellSort()
		{
			int[] arr;
			FileToArr(out arr);
			Console.WriteLine("- Danh sach ban dau lay tu File:");
			Console.WriteLine("[{0}]", string.Join(", ", arr));
			Console.WriteLine("- Danh sach sau khi duoc sap xep:");
			var sw2 = Stopwatch.StartNew();
			var sorter = new ShellSort();
			sorter.Sort(arr);
			sw2.Stop();
			Console.WriteLine("[{0}]", string.Join(", ", arr));
			Console.WriteLine("Time taken ShellSort: {0} ms", sw2.Elapsed.TotalMilliseconds);
			Console.ReadKey(true);
		}
		#region File => Array
		public static void FileToArr(out int[] arr)
		{
			string line;
			var parsedNumbers = new List<int>();

			//Open the file handle
			using (System.IO.StreamReader file =
			   new System.IO.StreamReader(@"Sort.txt"))
			{

				//Read in the next line, cancel if there is no next line
				while ((line = file.ReadLine()) != null)
				{
					var temp = int.Parse(line);
					parsedNumbers.Add(temp);
				}
				file.Close();
			}

			arr = parsedNumbers.ToArray();
		}
		#endregion
		#endregion
		#region Danh sách liên kết
		public static void TestLL()
		{
			GenericList<int> ds;
			ds = new GenericList<int>();
			var ds1 = new GenericList<int>();
			var ds2 = new GenericList<int>();
			var ds3 = new GenericList<int>();
			var chon = 0;
			do
			{
				chon = MenuDSLK();
				switch (chon)
				{
					case 1:
						{
							FileToList(ds); break;
						}
					case 2:
						{
							Input(ds);
							break;
						}
					case 3:
						{
							ds.PrintAllNodes(); break;
						}
					case 4:
						{
							Console.Write("PHAN TU CAN NHAP: ");
							var n = int.Parse(Console.ReadLine());
							ds.Addhead(n);
							ds.PrintAllNodes();
							break;
						}
					case 5:
						{
							Console.Write("PHAN TU CAN NHAP: ");
							var n = int.Parse(Console.ReadLine());
							ds.AddLast(n);
							ds.PrintAllNodes();
							break;                         
						}
					case 6:
						{
							ds.PrintAllNodes();
							Console.Write("PHAN TU CAN TIM: ");
							var n = int.Parse(Console.ReadLine());
							Console.WriteLine("=> Phan tu {0} nam o vi tri {1} . ",n,ds.SearchNode1(n));
							break;
						}
					case 7:
						{
							ds.PrintAllNodes();
							Console.Write("PHAN TU CAN XOA: ");
							var n = int.Parse(Console.ReadLine());
							ds.deleteallofX(n);
							//ds.Delete(n);
							ds.PrintAllNodes();
							break;
						}
					case 8:
						{
							Console.WriteLine("Phan tu lon nhat cua link list: "+ ds.maxnode(new IntComparer()));
							Console.WriteLine("Phan tu nho nhat cua link list: "+ ds.minnode(new IntComparer()));
							Console.Write("Phan tu chan cua link list: ");
							Evennode(ds);
							break;
						}
					case 9:
						{
							FileTotwolist(ds1, ds2);
							Console.WriteLine("+ Kiem tra tinh tang dan cua hai danh sach :");
							if (ds1.isSortedAsc(ds1.Head, new IntComparer()) == true && ds2.isSortedAsc(ds2.Head, new IntComparer()) == true)
							{
								Console.WriteLine("Ca 2 danh sach nhap tu FILE dau tang dan");
								ds1.PrintAllNodes();
								ds2.PrintAllNodes();
							}
							else
								Console.WriteLine("Out ra sua lai file");
							//ds3.MergeSortedList(ds1.Head, ds2.Head);
							//Console.Write("=> KET QUA danh sach 3: ");
							//ds3.PrintAllNodes();
							Console.Write("=> KET QUA danh sach 3: ");
							Merge(ds1, ds2, ds3);
							ds3.PrintAllNodes();
							Console.WriteLine("\nPress any key to terminate...");
							break;
						}
					case 10:
						{
							break;
						}
					case 0:
						{
							break;
						}
					default:
						{
							Console.WriteLine("Hello");
							break;
						}
				}
				Console.ReadKey();
				Console.Clear();
			} while (chon != 0);
		}
		public static int MenuDSLK()
		{
			int ch;
			Console.WriteLine("\n CAC THAO TAC VOI DANH SACH LIEN KET \n");
			Console.WriteLine("1. TAO DANH SACH TU FILE");
			Console.WriteLine("2. TAO DANH SACH BANG CACH NHAP TU BAN PHIM");
			Console.WriteLine("3. IN DANH SACH");
			Console.WriteLine("4. THEM MOT PHAN TU BANG PHUONG THUC <ADD HEAD>");
			Console.WriteLine("5. THEM MOT PHAN TU BANG PHUONG THUC <ADD LAST>");
			Console.WriteLine("6. TIM MOT PHAN TU");
			Console.WriteLine("7. XOA MOT PHAN TU");
			Console.WriteLine("8. DUYET CAC PHAN TU CO DIEU KIEN");
			Console.WriteLine("9. TRON 2 DS CO THU TU THANH DS THU 3 CO THU TU");

			Console.WriteLine("0. THOAT");
			Console.Write("         CHON CONG VIEC : ");
			ch = int.Parse(Console.ReadLine());
			return ch;
		}
		public static void FileToList(GenericList<int> ds)
		{
			using (StreamReader sr = new StreamReader("Dayso.txt"))
			{
				var n = int.Parse(sr.ReadLine());
				for (int i = 0; i < n; i++)
				{
					var x = int.Parse(sr.ReadLine());
					ds.Addhead(x);                   
				}
			}
		}
		public static void Merge(GenericList<int> d1, GenericList<int> d2, GenericList<int> d3)
		{
			int p, q;
			while (!d1.ListEmpty() && !d2.ListEmpty())
			{
				p = d1.Top(); q = d2.Top();
				if (p < q)
				{
					p = d1.Pop();
					d3.AddLast(p);
				}
				else
				{
					q = d2.Pop();
					d3.AddLast(q);
				}
			}
			while (!d1.ListEmpty())
			{
				p = d1.Pop();
				d3.AddLast(p);
			}
			while (!d2.ListEmpty())
			{
				q = d2.Pop();
				d3.AddLast(q);
			}
		}
		public static void FileTotwolist(GenericList<int> ds1, GenericList<int> ds2)
		{
			using (StreamReader sr = new StreamReader("Dayso1.txt"))
			{
				var n = int.Parse(sr.ReadLine());
				for (int i = 0; i < n; i++)
				{
					var x = int.Parse(sr.ReadLine());
					ds1.AddLast(x);
				}
			}
			using (StreamReader sr1 = new StreamReader("Dayso2.txt"))
			{
				var n = int.Parse(sr1.ReadLine());
				for (int i = 0; i < n; i++)
				{
					var x = int.Parse(sr1.ReadLine());
					ds2.AddLast(x);
				}
			}
		}
		public static void Input(GenericList<int> ds)
		{
			var x = 0;
			do
			{
				Console.Write("Nhap vao so nguyen duong (nhap -1 ket thuc): ");
				x = Int32.Parse(Console.ReadLine());
				if (x < 0)
					break;
				else
				{
					ds.Addhead(x);
					//AddTail(pNew);
				}
			} while (true);
		}
		public static void Evennode(GenericList<int> ds)
		{
			//Traverse from head
			Console.Write("Head ");
			var curr = ds.Head;
			while (curr != null)
			{
				if(curr.Data % 2 == 0)
					Console.Write("-> " + curr.Data);
				curr = curr.Next;
			}
			Console.Write("-> NULL");
			Console.WriteLine();
		}
		public static void TestDSSV()
		{
			var dssv = new GenericList<SV>();
			var dssvphu = new LinkedList();
			var chon = 0;
			do
			{
				chon = MenuDSSV();
				switch (chon)
				{
					case 1:
						{
							QLSV.FileToList(dssv);
							LinkedList.FileToList(dssvphu);
							break;
						}
					case 2:
						{
							LinkedList.PrintListSV(dssvphu);
							break;
						}
					case 3:
						{
							QLSV.Sort(dssv); break;
						}
					case 4:
						{
							LinkedList.SearchSinhVien(dssvphu); break;
						}
					case 5:
						{
							LinkedList.RemoveSinhVien(dssvphu); break;
						}
					case 0:
						{
							break;
						}
					default:
						{
							Console.WriteLine("Hello");
							break;
						}
				}
				Console.ReadKey();
				Console.Clear();
			} while (chon != 0);
		}
		public static int MenuDSSV()
		{
			int ch;
			Console.WriteLine("\n CAC THAO TAC VOI DANH SACH SINH VIEN TAO BANG DANH SACH LIEN KET \n");
			Console.WriteLine("1. TAO DANH SACH SINH VIEN TU FILE");
			Console.WriteLine("2. XUAT DANH SACH SINH VIEN");
			Console.WriteLine("3. SAP XEP DANH SACH SINH VIEN ");
			Console.WriteLine("4. TIM SINH VIEN THEO TEN,LOP,DIACHI");
			Console.WriteLine("5. XOA SINH VIEN THEO TEN,LOP,DIACHI");

			Console.WriteLine("0. THOAT");
			Console.Write("         CHON CONG VIEC : ");
			ch = int.Parse(Console.ReadLine());
			return ch;
		}
		#endregion
		#region Cây nhị phân tìm kiếm
		public static int MenuCây()
		{
			int ch;
			Console.WriteLine("\n CAC THAO TAC VOI CAY NHI PHAN TIM KIEM \n");
			Console.WriteLine("1. TAO CAY TU FILE");
			Console.WriteLine("2. TAO CAY RANDOM");
			Console.WriteLine("3. THEM NUT");
			Console.WriteLine("4. XOA NUT");
			Console.WriteLine("5. TIM NUT");
			Console.WriteLine("6. XOA TOAN BO CAY");
			Console.WriteLine("7. DEM SO NUT CUA CAY");
			Console.WriteLine("8. DEM SO NUT LA");
			Console.WriteLine("9. DEM SO NUT CHI CO MOT CAY CON");
			Console.WriteLine("10. TIM CAY CON NHO NHAT CUA HAI NUT X,Y CHO TRUOC");
			Console.WriteLine("11. TIM X VA CHO BIET X O TANG THU MAY TREN CAY");
			Console.WriteLine("12. XUAT CAY THEO MUC");
			Console.WriteLine("13. XUAT CAY THEO HINH CAY");
			Console.WriteLine("0. THOAT");
			Console.Write("         CHON CONG VIEC : ");
			ch = int.Parse(Console.ReadLine());
			return ch;
		}
		public static void TestCÂY()
		{
			var tree = new MyBinaryTree<int>();
			var chon = 0;
			do
			{
				chon = MenuCây();
				switch (chon)
				{
					case 1: { FileToBST(tree); break; }
					case 2: { TreeRandom(tree); break; }
					case 3:
						{
							Console.Write("\n\nNhap nut can them: ");
							var keyadd = int.Parse(Console.ReadLine());
							tree.Insert(keyadd);
							break;
						}
					case 4:
						{
							PrintTree(tree);
							Console.Write("\n\nNhap nut can xoa ");
							var keydelete = int.Parse(Console.ReadLine());
							tree.Remove(ref tree.root,keydelete);
							break;
						}
					case 5:
						{
							PrintTree(tree);
							Console.Write("\n\nNhap khoa can tim kiem: ");
							var keysearch = int.Parse(Console.ReadLine());
							var result = tree.Find(keysearch);
							if (result) Console.WriteLine("Tim thay {0} trong cay nhi phan tim kiem", keysearch);
							else Console.WriteLine("Khong tim thay {0} trong cay nhi phan tim kiem", keysearch);
							break;
						}
					case 6:
						{
							tree.DeleteTree();
							Console.WriteLine("\n => Da xoa toan bo cay");
							break;
						}
					case 7:
						{
							PrintTree(tree);
							Console.WriteLine("\n => so nut tren cay: {0}",tree.CountChildren());
							break;
						}
					case 8:
						{
							PrintTree(tree);
							Console.WriteLine("\n => so nut la tren cay: {0}", tree.Demsonutla());
							break;
						}
					case 9:
						{
							PrintTree(tree);
							Console.WriteLine("\n => so nut chi co mot cay con: {0}", tree.Count_nut_co_1_con(tree.Root));
							break;
						}
					case 10:
						{
							PrintTree(tree);
							Console.Write("\n\n- Nhap khoa 1: ");
							var v1 = int.Parse(Console.ReadLine());
							Console.Write("\n- Nhap khoa 2: ");
							var v2 = int.Parse(Console.ReadLine());
							var lca = tree.LCA(v1, v2);
							Console.WriteLine("=> LCA of {0} and {1} is: {2}", v1, v2, (lca != null ? lca.Data.ToString() : "No LCA Found"));
							break;
						}
					case 11:
						{
							PrintTree(tree);
							Console.Write("\n\nNhap khoa can tim kiem: ");
							var key = int.Parse(Console.ReadLine());
							Console.WriteLine(getLevelOfNode(tree.Root, key, 1).ToString());
							break;
						}
					case 12:
						{
							Console.WriteLine();
							PrintTreeOnLevel(tree.root, 0);
							break;
						}
					case 13:
						{
							PrintTree(tree); break;
						}
					default:
						Console.WriteLine("Unexpected Case");
						break;
				}
				Console.ReadKey();
				Console.Clear();
			} while (chon != 0);
		}
		#region Các cách nhập dữ liệu vào cây
		// nhập từ bàn phím
		public static void INPUT(MyBinaryTree<int> a)
		{
			do
			{
				int x;
				Console.Write("Nhap gia tri(trung ket thuc): ");
				int.TryParse(Console.ReadLine(), out x);
				if (a.Insert(x))
				{
					Console.WriteLine("Da them vao cay");
				}
				else
				{
					Console.WriteLine("Bi trung, ket thuc");
					return;
				}
			} while (true);
		}
		// nhập từ mảng cho sẵn
		public static void ADD_ARRAY(MyBinaryTree<int> a, int[] array)
		{
			foreach (var x in array)
			{
				if (a.Insert(x))
				{
					Console.WriteLine("Da them vao cay");
				}
				else
				{
					continue;
				}
			}
		}
		// nhập từ FILE
		public static void FileToBST(MyBinaryTree<int> root)
		{
			using (var sr = new StreamReader("BST.txt"))
			{
				var n = int.Parse(sr.ReadLine());
				for (int i = 0; i < n; i++)
				{
					var key = int.Parse(sr.ReadLine());
					root.Insert(key);
				}
			}
		}
		// nhập ngẫu nhiên
		public static void TreeRandom(MyBinaryTree<int> root)
		{
			int key;
			Console.WriteLine("Cac gia tri duoc them vao cay:");
			var random = new Random();
			for (int i = 1; i <= 6; i++)
			{
				key = random.Next(100);
				Console.Write(key + " ");
				root.Insert(key);
			}
		}
		#endregion
		// Xuất cây theo mức
		public static void PrintTreeOnLevel(MyTNode<int> root, int muc)
		{
			int i;
			//Console.WriteLine();
			if (root != null)
			{
				PrintTreeOnLevel(root.PRight, muc + 1);
				for (i = 0; i <= muc; i++) Console.Write("    ");
				Console.Write("          " + root.Data + "\n\n");
				PrintTreeOnLevel(root.PLeft, muc + 1);
			}
		}
		public static void PrintTree(MyBinaryTree<int> root)
		{
			root.Print();
		}
		public static int getLevelOfNode(MyTNode<int> root, int key, int level)
		{
			if (root == null)
				return 0;
			if (root.Data == key)
				return level;

			var result = getLevelOfNode(root.PLeft, key, level + 1);
			if (result != 0)
			{
				// If found in left subtree , return 
				return result;
			}
			result = getLevelOfNode(root.PRight, key, level + 1);

			return result;
		}

		#endregion
		#region Cây AVL
		public static void HelloWorld()
		{
			var path = @"Commands.txt";
			var d = DateTime.Now;
			var tree = new AVL<int>();
			for (int i = 1; i <= 5; i++)
			{
				tree.Insert(int.Parse(Console.ReadLine()));
			}

			tree.DisplayTree();

			tree.Delete(5);
			Console.WriteLine("");
			tree.DisplayTree();

			//RunCommands(tree, path);

			tree.DisplayTree();
			tree.Clear();
			var elapse = DateTime.Now - d;
			Console.WriteLine(elapse.ToString()); 
			Console.ReadKey(true);
		}
		public static void RunCommands(AVL<int> tree, string path)
		{
			var lines = System.IO.File.ReadAllLines(path);
			foreach (string line in lines)
			{
				if (line.Contains("Insert:"))
				{
					var onlyNum = line.Remove(0, "Insert:".Length);
					var numbers = onlyNum.Split(' ');
					foreach (var item in numbers)
					{
						int num;
						if (Int32.TryParse(item, out num))
						{
							tree.Insert(num);
							Console.WriteLine($"Inserted {num}");
						}
					}
				}
				else if (line.Contains("Delete:"))
				{
					var onlyNum = line.Remove(0, "Delete:".Length);
					var numbers = onlyNum.Split(' ');
					foreach (var item in numbers)
					{
						int num;
						if (Int32.TryParse(item, out num))
						{
							tree.Delete(num);
							Console.WriteLine($"Deleted {num}");

						}
					}
				}
				else if (line.Contains("Find:"))
				{
					var onlyNum = line.Remove(0, "Find:".Length);
					var numbers = onlyNum.Split(' ');
					foreach (var item in numbers)
					{
						int num;
						if (Int32.TryParse(item, out num))
						{
							if (tree.Find(num) != null)
							{
								Console.WriteLine($"Found {num}");
							}
						}
					}
				}
			}
		}
		#endregion
		#region Stack
		public static void TestStack()
		{
			var chon = 0;
			do
			{
				chon = MenuStack();
				switch (chon)
				{
					case 1:
						{
							Console.Write("Nhap infix: ");
							var s = Console.ReadLine();
							//var kq = I2PO.infixToPostfix(s);
							var kq = new Infixtopostfix().InfixtoPosfix(s);
							Console.WriteLine("=> postfix: {0}",kq);
							Console.WriteLine("+ Evaluate: ");
							var dt = new DataTable();
							var v1 = dt.Compute(s, "");
							s += "=" + v1;
							var v2 = new Infixtopostfix().TinhBieuThuc(kq);
							kq += "=" + v2;
							Console.WriteLine("==> Infix: {0}",s);
							Console.WriteLine("==> Postfix: {0}",kq);
							if (new IntComparer().Compare((int)v1,(int)v2) == 0)
							{
								Console.WriteLine("KET QUA DUNG !!!!!");
							}
							else
								Console.WriteLine("KIEM TRA LAI BAI LAM");
							Console.WriteLine("\nPress any key to terminate...");
							break;
						}
					case 2:
						{
							int num, baseNum;
							Console.Write("Enter a decimal number: ");
							num = Convert.ToInt32(Console.ReadLine());
							Console.Write("Enter a base: ");
							baseNum = Convert.ToInt32(Console.ReadLine());
							Console.Write(num + " converts to ");
							decimal_to_binary.MulBase(num, baseNum);
							Console.WriteLine(" Base " + baseNum);
							Console.WriteLine("\nPress any key to terminate...");
							break;
						}
					case 3:
						{
							var a = new GenericStack<char>();
							var str = "[{}{}{}]";
							var str2 = "())()()()())))";
							if (kiemtradaungoacdon.KiemTraXauNgoac(str, a))
								Console.WriteLine(@"[ {0} ] => Xau ngoac dung", str);
							else
								Console.WriteLine(@"[ {0} ] => Xau ngoac sai", str);
							if (kiemtradaungoacdon.KiemTraXauNgoac(str2, a))
								Console.WriteLine(@"[ {0} ] => Xau ngoac dung", str2);
							else
								Console.WriteLine(@"[ {0} ] => Xau ngoac sai", str2);
							Console.WriteLine("\nPress any key to terminate...");
							break;
						}
					case 0:
						{
							break;
						}

					default:
						{
							Console.WriteLine("Unexpected Case");
							break;
						}
				}
				Console.ReadKey();
				Console.Clear();
			} while (chon != 0);
		}
		public static int MenuStack()
		{
			int ch;
			Console.WriteLine("\n CAC THAO TAC VOI UNG DUNG CUA STACK \n");
			Console.WriteLine("1. INFIX TO POSTFIX AND EVALUATE");
			Console.WriteLine("2. DECIMAL NUMBER TO BINARY NUMBER");
			Console.WriteLine("3. KIEM TRA TINH HOP LE CUA DAU NGOAC DON");
			Console.WriteLine("0. THOAT");
			Console.Write("         CHON CONG VIEC : ");
			ch = int.Parse(Console.ReadLine());
			return ch;
		}
		#endregion
		#region Queue
		public static void TestQueue()
		{
			GenericQueue<int> queue;
			queue = new GenericQueue<int>();
			GenericStack<int> stack;
			stack = new GenericStack<int>();
			var chon = 0;
			do
			{
				chon = MenuQueue();
				switch (chon)
				{
					case 1:
						{
							INP_STACK(stack);
							Console.WriteLine("STACK : ");
							stack.print();
							if (queue.Count == 0)
								Console.WriteLine("Queue : NULL");
							Console.WriteLine("---------Convert Stack to Queue----------");
							Console.Write("Queue : ");
							ConvertStackToQueue.ConverTo(stack, queue);
							if (stack.Size == 0)
								Console.WriteLine("Stack : NULL");
							Console.WriteLine("\nPress any key to terminate...");
							break;
						}
					case 2:
						{
							var queue1 = new GenericQueue<int>();
							INP_QUEUE(queue1);
							Console.WriteLine("queue luc ban dau: "+queue1);
							Console.WriteLine("queue luc sau khi dao nguoc: "+queue1.Reverse());
							break;
						}
					case 3:
						{
							Console.Write("+ Nhap Chuoi: ");
							var str = Console.ReadLine();
							Console.WriteLine("[Cach lam: chay cai phan tu chuoi tu 0 den het chuoi, \nneu gap phan tu khac rong thi Enqueue. " +
								"\nsau do cho queue dequeue vao string builder roi in ra]");
							Console.WriteLine("Chuoi sau khi loai bo khoang trang: ");
							Console.WriteLine("=> " + Chuoi.Trim(str));
							break;
						}
					case 0:
						{
							break;
						}
					default:
						{
							Console.WriteLine("Unexpected Case");
							break;
						}
				}
				Console.ReadKey();
				Console.Clear();
			} while (chon != 0);
		}
		public static int MenuQueue()
		{
			int ch;
			Console.WriteLine("\n CAC THAO TAC VOI UNG DUNG CUA QUEUE \n");
			Console.WriteLine("1. CONVERT FROM STACK S TO QUEUE Q");
			Console.WriteLine("2. RESERVE QUEUE");
			Console.WriteLine("3. LOAI BO KHOANG TRANG DU THUA TRONG MOT CHUOI");
			Console.WriteLine("0. THOAT");
			Console.Write("         CHON CONG VIEC : ");
			ch = int.Parse(Console.ReadLine());
			return ch;
		}
		public static void INP_STACK(GenericStack<int> a)
		{
			do
			{
				int x;
				Console.Write("Gia tri (nhap <=0 de ket thuc): ");
				int.TryParse(Console.ReadLine(), out x);
				if (x <= 0) break;
				a.push(x);
				Console.WriteLine(">> Da push {0} thanh cong", x);
			} while (true);
		}
		public static void INP_QUEUE(GenericQueue<int> a)
		{
			do
			{
				int x;
				Console.Write("Gia tri (nhap <=0 de ket thuc): ");
				int.TryParse(Console.ReadLine(), out x);
				if (x <= 0) break;
				a.Enqueue(x);
				Console.WriteLine(">> Da Enqueue {0} thanh cong", x);
			} while (true);
		}
		#endregion
	}
}
