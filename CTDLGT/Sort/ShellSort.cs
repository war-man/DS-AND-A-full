using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Sort
{
    class ShellSort
    {
        public void Sort(int[] array)
        {
            var n = array.Length;
            var gap = n / 2;
            int temp;

            while (gap > 0)
            {
                for (int i = 0; i + gap < n; i++)
                {
                    var j = i + gap;
                    temp = array[j];

                    while (j - gap >= 0 && temp < array[j - gap])
                    {
                        array[j] = array[j - gap];
                        j = j - gap;
                    }

                    array[j] = temp;
                }

                gap = gap / 2;
            }
        }
    }
}
