using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    class GenericStack<T>
    {
        public class StackNode
        {
            T data;     // phần dữ liệu của Node
            StackNode next = null;  // next trỏ đến Node tiếp theo
            // Contructor Node với dữ liệu t
            public StackNode()
            { }
            public StackNode(T t)
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
            public StackNode Next
            {
                get { return next; }
                set { next = value; }
            }
        }
        /// <summary>
        /// Constructs empty stack with no elements
        /// </summary>
        public GenericStack()
        {
            top = null;
        }

        public GenericStack(object length)
        {
            this.length = length;
        }

        /// <summary>
        /// Push new node onto the top of the stack
        /// </summary>
        /// <param name="value"></param>
        public void push(T value)
        {
            var newNode = new StackNode(value);
            Size++;
            //if this is the first node in stack, it is also the top
            if (Size == 1)
            {
                top = newNode;
                return;
            }
            //else bottom is already established, only change top
            newNode.Next = top;
            top = newNode;
        }
        public T TOP()
        {
            return top.Data;
        }

        /// <summary>
        /// Remove and return the top element from stack.
        /// Returns Null if stack is empty
        /// </summary>
        /// <returns></returns>
        public T pop()
        {
            //if there are elements to pop
            if (Size > 0)
            {
                //get top node's value
                var returnValue = top.Data;
                //if more node's exist, decrement top pointer to next node in stack
                //garbage collector will clean up popped node once de-referenced
                if (top.Next != null)
                {
                    top = top.Next;
                }
                Size--;
                return returnValue;
            }
            else
            {
                return default(T);
            }
        }
        //public T Pop()
        //{
        //    //StackNode n;
        //    //n = top;
        //    //top = n.Next;
        //    //return n.Data;
        //    top = top.Next;
        //    return top.Data;
        //}


        /// <summary>
        /// Returns value of stack's top most element without changing structure of stack
        /// </summary>
        /// <returns></returns>
        public T peek()
        {
            if (Size > 0)
            {
                return top.Data;
            }
            Console.WriteLine("Error -> Unable to view top value of empty stack.");
            return default(T);
        }

        //public void INP_STACK()
        //{
        //    do
        //    {
        //        T x;
        //        Console.Write("Gia tri (nhap <=0 de ket thuc): ");
        //        int.TryParse(Console.ReadLine(), out x);
        //        if (x <= 0) break;
        //        push(x);
        //        if (!kq)
        //        {
        //            Console.WriteLine(">>Stack => full");
        //            break;
        //        }
        //        else
        //        {
        //            Console.WriteLine(">> Da push {0} thanh cong", x);
        //        }
        //    } while (true);
        //}
        /// <summary>
        /// Print all elements of stack
        /// </summary>
        public void print()
        {
            Console.WriteLine("\nPrinting Stack Elments" +
                "\n////////////////////\n////////TOP/////////\n////////////////////");
            var current = top;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
            Console.WriteLine("////////////////////\n//////BOTTOM////////\n////////////////////");
        }

        /// <summary>
        /// Return false if stack size is greater than 0
        /// </summary>
        /// <returns></returns>
        public Boolean isEmpty()
        {
            if (Size > 0)
            {
                return false;
            }
            return true;
        }

        //define bottom and top of stack
        //public StackNode bottom { get; set; }
        public StackNode top { get; set; }
        public int Size { get => size; set => size = value; }

        private int size;
        private object length;
    }
}
