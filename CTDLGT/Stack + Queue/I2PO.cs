using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    class I2PO
    {
        #region i2po
        public static int Prec(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;
            }
            return -1;
        }

        // The main method that converts given infix expression 
        // to postfix expression.  
        public static String infixToPostfix(String exp)
        {
            // initializing empty String for result 
            var result = "";

            // initializing empty stack 
            var stack = new GenericStack<char>();

            for (int i = 0; i < exp.Length; ++i)
            {
                var c = exp[i];

                // If the scanned character is an operand, add it to output. 
                if (Char.IsLetterOrDigit(c))
                    result += c;

                // If the scanned character is an '(', push it to the stack. 
                else if (c == '(')
                    stack.push(c);

                //  If the scanned character is an ')', pop and output from the stack  
                // until an '(' is encountered. 
                else if (c == ')')
                {
                    while (!stack.isEmpty() && stack.peek() != '(')
                        result += stack.pop();

                    if (!stack.isEmpty() && stack.peek() != '(')
                        return "Invalid Expression"; // invalid expression                 
                    else
                        stack.pop();
                }
                else // an operator is encountered 
                {
                    while (!stack.isEmpty() && Prec(c) <= Prec(stack.peek()))
                        result += stack.pop();
                    stack.push(c);
                }

            }

            // pop all the operators from the stack 
            while (!stack.isEmpty())
                result += stack.pop();

            return result;
        }
        #endregion

        public static double EvaluateInfix(String input)
        {
            var expr = "(" + input + ")";
            var ops = new GenericStack<String>();
            var vals = new GenericStack<Double>();

            for (int i = 0; i < expr.Length; i++)
            {
                var s = expr.Substring(i, 1);
                if (s.Equals("(")) { }
                else if (s.Equals("+")) ops.push(s);
                else if (s.Equals("-")) ops.push(s);
                else if (s.Equals("*")) ops.push(s);
                else if (s.Equals("/")) ops.push(s);
                else if (s.Equals("sqrt")) ops.push(s);
                else if (s.Equals(")"))
                {
                    var count = ops.Size;
                    while (count > 0)
                    {
                        var op = ops.pop();
                        var v = vals.pop();
                        if (op.Equals("+")) v = vals.pop() + v;
                        else if (op.Equals("-")) v = vals.pop() - v;
                        else if (op.Equals("*")) v = vals.pop() * v;
                        else if (op.Equals("/")) v = vals.pop() / v;
                        else if (op.Equals("sqrt")) v = Math.Sqrt(v);
                        vals.push(v);

                        count--;
                    }
                }
                else vals.push(Double.Parse(s));
            }
            return vals.pop();
        }

    }
}
