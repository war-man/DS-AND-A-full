using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    public static class Chuoi
    {
        static public string Trim(string str)
        {
            if (str == null)
            {
                return null;
            }
            var count = str.Length;

            var sb = new GenericQueue<Char>();

                for (int i = 0; i < count; i++)
                    if (str[i] != ' ')
                        sb.Enqueue(str[i]);
            var sb2 = new StringBuilder();

            while (sb.Count != 0)
            {
                sb2.Append(sb.Dequeue());
            }

            return sb2.ToString();
        }
    }
}
