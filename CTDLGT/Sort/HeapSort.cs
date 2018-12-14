using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Sort
{
    class HeapSort
    {
        public void Sort(int[] array)
        {
            var heapSize = array.Length;
            var indexOfLastParent = (heapSize - 2) / 2;
            for (int i = indexOfLastParent; i >= 0; i--)
                CreateHeap(array, heapSize, i);

            for (int i = array.Length - 1; i > 0; i--)
            {
                Swap(array, i, 0);

                heapSize--;
                CreateHeap(array, heapSize, 0);
            }
        }

        public void CreateHeap(int[] array, int heapSize, int index)
        {
            var left = 2 * index + 1;
            var right = 2 * index + 2;

            var max = index;

            if (left < heapSize && array[left] > array[index])
                max = left;

            if (right < heapSize && array[right] > array[max])
                max = right;

            if (max != index)
            {
                Swap(array, max, index);

                CreateHeap(array, heapSize, max);
            }
        }

        public void Swap(int[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
