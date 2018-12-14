using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Danh_sách_liên_kết
{
	public class GenericList<T> : ICollection<T>
	{
		// Định nghĩa phần tử của danh sách là Node
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

		}

		// khai báo các thành phần của ds liên kết
		// head : trỏ đến đầu ds
		// last : trỏ đến cuối ds, phần do sinh viên làm thêm
		Node head = null;
		public Node Head //Expose a public property to get to head of the list  
		{
			get { return head; }
		}

		int ICollection<T>.Count => throw new NotImplementedException();

		public bool IsReadOnly => throw new NotImplementedException();

		Node last = null;
		public int Count;

		// phương thức khai báo trả về một phần tử với dữ liệu trừu tượng
		// chương trình (bài 23) áp dụng phương thức này mới cụ thể dữ liệu data
		public IEnumerator<T> GetEnumerator()
		{
			var p = head;
			while (p != null)
			{
				yield return p.Data;
				p = p.Next;
			}
		}
		/*LinkedList<int> linkedList = new LinkedList<int>(new List<int> { 1, 2, 3, 4 });
		 */
		public GenericList()
		{
		}
		public GenericList(IEnumerable<T> initalize)
		{
			foreach (T item in initalize)
			{
				AddLast(item);
			}
		}
		/* Thêm vào Đầu danh sách(AddHead): Giải thích
		 * Vì head chính là Node đầu tiên vì thế ta chỉ cần cập nhật lại head là xong
		 *Cho Next của n (như là sợi dây) trỏ hay buộc vào head. Lúc này head chính là Node đầu tiên thì cập nhật lại head chính là n;
		*/
		#region method phụ
		public bool Isempty() => head == null;
		public int Length()
		{
			var size = 0;
			var P = head;
			while (P != null)
			{
				size++;
				P = P.Next;
			}
			return size;
		}
		public int count(T search_for)
		{
			var current = head;
			var phu = 0;
			while (current != null)
			{
				if (!Equals(current.Data, search_for))
					phu++;
				current = current.Next;
			}
			return phu;
		}
		public T GetHeadData()
		{
			//This trick can be used instead of using an else 
			//The method returns the default value for the type T 
			var temp = default(T);
			if (head != null)
			{
				temp = head.Data;
			}
			return temp;
		}
		public T GetEndAdded()
		{
			// The value of temp is returned as the value of the method. 
			// The following declaration initializes temp to the appropriate 
			// default value for type T. The default value is returned if the 
			// list is empty.
			var temp = default(T);

			var current = head;
			while (current != null)
			{
				temp = current.Data;
				current = current.Next;
			}
			return temp;
		}
		public void Clear()
		{
			head = null;
			last = null;
			Count = 0;
		}

		#endregion
		#region method chính

		public void Reverse()
		{
			Node prev = null;
			var current = head;

			if (current == null)
				return;

			while (current != null)
			{
				var next = current.Next;
				current.Next = prev;
				prev = current;
				current = next;
			}

			head = prev;
		}
		public bool isSortedAsc(Node head, IComparer<T> cmp)
		{
			// Base cases 
			if (head == null || head.Next == null)
				return true;

			// Check first two nodes and recursively 
			// check remaining.    
			return (cmp.Compare(head.Data, head.Next.Data) < 0 &&
				isSortedAsc(head.Next, cmp));
		}
		public void Delete(T value)
		{
			if (head == null) return;

			if (Equals(head.Data, value))
			{
				head = head.Next;
				return;
			}

			var n = head;
			while (n.Next != null)
			{
				if (Equals(n.Next.Data, value))
				{
					n.Next = n.Next.Next;
					return;
				}

				n = n.Next;
			}
		}

		public void Addhead(T t)
		{
			var n = new Node(t);
			if (Isempty())
			{
				head = last = n;
			}
			else
			{
				n.Next = head;// đem Next của Node n vừa tạo trỏ đến head hiện tại
				head = n;//cập nhập lại giá trị head
			}
			Count++;
		}
		public void AddLast(T t)
		{
			var newNode = new Node(t);
			if (Isempty())
			{
				head = last = newNode;
			}
			else
			{
				last.Next = newNode; //gán lại Next của Node cuối cùng (cũ) sẽ trỏ đến Node mới. 
				last = newNode; //cập nhập lại giá trị last,bây giờ Node cuối của danh sách là Node chúng ta vừa thêm.
				Count++;
			}
		}
		public void AddLast2(T t)
		{
			var n = new Node(t);
			Node p, q;
			p = head;
			q = new Node();
			while (p != null)
			{
				q = p;
				p = p.Next;
			}
			q.Next = n;
		}
		public void Addpos(T t, int pos)
		{
			var P = new Node(t);
			var Q = head;
			var i = 1;
			var size = 0;
			while (Q != null)
			{
				size++;
				Q = Q.Next;
			}
			if (pos < 1 && pos > size) Console.WriteLine("Vi tri ko hop le");
			else
			{
				if (pos == 1) Addhead(t); //chen vao vi tri dau tien
				else
				{
					while (Q != null && i != pos - 1) //duyet den vi tri k-1
					{
						i++;
						Q = Q.Next;
					}
					P.Next = Q.Next;// gán Next của Node mới bằng địa chỉ của Node sau q
					Q.Next = P;// cập nhập lại Next của Node q trỏ đến Node mới.
				}
			}
		}
		public int SearchNode1(T t)
		{
			var temp = head;
			var i = 1;
			while (temp != null && string.Compare(temp.Data.ToString(), t.ToString(), StringComparison.CurrentCulture) != 0)
			{
				temp = temp.Next;
				i = i + 1;
			}
			if (temp != null)
			{
				return i;
			}
			return 0;
		}
		public bool SearchNode2(T item)
		{
			var temp = this.head;
			var matched = false;
			while (!(matched = temp.Data.ToString().Equals(item.ToString())) && temp.Next != null)
			{
				temp = temp.Next;
			}
			return matched;

		}
		public int SearchNode3(T item)
		{
			for (Node p = head; p != null; p = p.Next)
			{
				if (string.Compare(p.Data.ToString(), item.ToString(), StringComparison.CurrentCulture) == 0)
					return 1;
			}
			return 0;
		}
		public int countoccurentx(T search_for)
		{
			var current = head;
			var dem = 0;
			while (current != null)
			{
				if (Equals(current.Data, search_for))
					dem++;
				current = current.Next;
			}
			return dem;
		}
		public void RemoveFromStart()
		{
			if (Count > 0)
			{
				head = head.Next;// chuyen con tro cua cai head dau tien qua cai head tiep theo
				Count--;
			}
			else
			{
				Console.WriteLine("No element exist in this linked list.");
			}
		}
		public void RemoveAt(int k)
		{
			var P = head;
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
						P = P.Next;
						i++; // khi i = k -1 thì i  sẽ dừng
					}
					P.Next = P.Next.Next; //cho P tro sang Node ke tiep vi tri k
				}
			}
		}

		public void Remove(T t)
		{
			var ps = SearchNode1(t);
			if (ps == 0)
				return;
			else
			{
				RemoveAt(ps);
			}
		}

		public void SelectionSort(IComparer<T> cmp)
		{
			var currentOuter = head;

			while (currentOuter != null)
			{
				var minimum = currentOuter;
				var currentInner = currentOuter.Next;

				while (currentInner != null)
				{
					if (cmp.Compare(currentInner.Data, minimum.Data) < 0)
					{
						minimum = currentInner;
					}

					currentInner = currentInner.Next;
				}

				if (!Object.ReferenceEquals(minimum, currentOuter))
				{
					var temp = currentOuter.Data;
					currentOuter.Data = minimum.Data;
					minimum.Data = temp;
				}

				currentOuter = currentOuter.Next;
			}
		}
		public void deleteX(T t)//xóa x còn 1 lần
		{
			var k = countoccurentx(t);
			Console.WriteLine(k);
			if (k > 1)
			{
				for (int i = 0; i < k - 1; i++)
				{
					Remove(t);
				}
			}
		}
		public void deleteallofX(T t)
		{
			var k = countoccurentx(t);
			if (k >= 1)
			{
				for (int i = 0; i < k; i++)
				{
					Remove(t);
				}
			}
		}
		public bool ListEmpty()
		{
			return head == null ? true : false;
		}
		// Lấy phần tử đầu danh sách
		public T Pop()
		{
			var n = head;
			head = n.Next;
			return n.Data;
		}
		// Lấy giá trị đầu danh sách
		public T Top()
		{
			return head.Data;
		}


		public void MergeSortedList(Node first, Node second)
		{

			if (Convert.ToInt32(first.Next.Data.ToString())
					> Convert.ToInt32(second.Data.ToString()))
			{
				var t = first;
				first = second;
				second = t;
			}
			head = first;
			while ((first.Next != null) && (second != null))
			{
				if (Convert.ToInt32(first.Next.Data.ToString())
					< Convert.ToInt32(second.Data.ToString()))
				{
					first = first.Next;
				}
				else
				{
					var n = first.Next;
					var t = second.Next;
					first.Next = second;
					second.Next = n;
					first = first.Next;
					second = t;
				}
			}
			if (first.Next == null)
				first.Next = second;
		}
		public void PrintAllNodes()
		{
			//Traverse from head
			Console.Write("Head ");
			var curr = head;
			while (curr != null)
			{
				Console.Write("-> ");
				Console.Write(curr.Data);
				curr = curr.Next;
			}
			Console.Write("-> NULL");
			Console.WriteLine();
		}

		public T[] ToArray()
		{
			var result = new T[Length()];
			var index = 0;
			var node = head;
			while (node != null)
			{
				result[index] = node.Data;
				node = node.Next;
			}
			return result;
		} // TOlIST TUONG TU

		public void SelectionSort2(IComparer<T> cmp)
		{
			Node p, q;
			p = head;
			q = null;
			while (p.Next != null)
			{
				q = p.Next;
				while (q != null)
				{
					if (cmp.Compare(p.Data, q.Data) > 0)
					{
						var tmp = p;
						p = q;
						q = tmp;
					}
					q = q.Next;
				}
				p = p.Next;
			}
		}
		public T maxnode(IComparer<T> cmp)
		{
			var max = head.Data;
			var p = head.Next;
			while (p != null)
			{
				if (cmp.Compare(p.Data, max) > 0)
					max = p.Data;
				p = p.Next;
			}
			return max;
		}
		public T minnode(IComparer<T> cmp)
		{
			var min = head.Data;
			var p = head.Next;
			while (p != null)
			{
				if (cmp.Compare(p.Data, min) < 0)
					min = p.Data;
				p = p.Next;
			}
			return min;
		}
		public void CopyTo(T[] array, int arrayIndex)
		{
			var current = head;
			while (current != null)
			{
				array[arrayIndex++] = current.Data;
				current = current.Next;
			}
		}
		public bool Contains(T value)
		{
			var current = head;
			while (current != null)
			{
				if (current.Next.Equals(value))
				{
					return true;
				}
				current = current.Next;
			}
			return false;
		}

		public void Add(T item)
		{
			throw new NotImplementedException();
		}

		bool ICollection<T>.Remove(T item)
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}