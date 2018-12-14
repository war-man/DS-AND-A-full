using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Sort
{
    public class QSort<T> where T : IComparable
    {

        public IList<T> A;
        public QSort(IList<T> A)
        {
            this.A = A;
        }
        public QSort()
        {

        }
        public void QuickSort(int leftBorder, int rigthBorder)
        {
            var l = leftBorder;
            var r = rigthBorder;
            var pivot = A[(leftBorder + rigthBorder) / 2];

            while (l <= r)
            {
                while (A[l].CompareTo(pivot) < 0)
                {
                    l++;
                }
                while (A[r].CompareTo(pivot) > 0)
                {
                    r--;
                }

                if (l <= r)
                {
                    Swap(l, r);
                    l++;
                    r--;
                    break;
                }
            }

            if (leftBorder < r)
                QuickSort(leftBorder, r);
            if (rigthBorder > l)
                QuickSort(l, rigthBorder);
        }
        public void QuickSortDescending(int leftBorder, int rigthBorder)
        {
            var l = leftBorder;
            var r = rigthBorder;
            var pivot = A[(leftBorder + rigthBorder) / 2];

            while (l <= r)
            {
                while (A[l].CompareTo(pivot) > 0)
                {
                    l++;
                }
                while (A[r].CompareTo(pivot) < 0)
                {
                    r--;
                }

                if (l <= r)
                {
                    Swap(l, r);
                    l++;
                    r--;
                    break;
                }
            }

            if (leftBorder < r)
                QuickSortDescending(leftBorder, r);
            if (rigthBorder > l)
                QuickSortDescending(l, rigthBorder);
        }
        public void Swap(int a, int b)
        {
            var temp = A[a];
            A[a] = A[b];
            A[b] = temp;
        }
    }
}
