using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    class ConvertStackToQueue
    {
        public static void ConverTo(GenericStack<int> dStack, GenericQueue<int> dQueue)
        {
            while (!dStack.isEmpty())
            {
                var tmp = dStack.peek();
                dQueue.Enqueue(tmp);
                dStack.pop();
            }
            Console.WriteLine(dQueue);
        }
    }
}
