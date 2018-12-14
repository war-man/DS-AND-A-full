using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    class decimal_to_binary
    {
        public static void MulBase(int n, int b)
        {
            var Digits = new GenericStack<int>();
            do
            {
                Digits.push(n % b);
                n /= b;
            } while (n != 0);
            while (Digits.Size > 0)
                Console.Write(Digits.pop());
        }
    }
}
