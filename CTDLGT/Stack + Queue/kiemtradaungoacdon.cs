using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTDLGT.Stack___Queue
{
    class kiemtradaungoacdon
    {
        public static bool KiemTraXauNgoac(string str, GenericStack<char> a)
        {
            //string str là chuỗi xâu ngoặc sẽ nhập vào
            for (int i = 0; i < str.Length; i++)//duyệt lần lượt hết chuỗi
            {
                if (str[i] == '(' || str[i] == '[' || str[i] == '{')//nếu gặp dấu mở ngoặc
                {
                    a.push(str[i]);//push hết vào stack 
                }
                else //nếu gặp dấu đóng ngoặc
                {
                    if (!a.isEmpty())//nếu stack khác rỗng 
                    {
                        if (str[i] == ']')//kiểm tra xem phần tử ngoặc đỉnh stack có hợp với str[i] hay không
                        {
                            if (a.TOP() != '[')//không hợp
                            {
                                return false;//sai
                            }
                        }
                        if (str[i] == ')')//kiểm tra xem phần tử ngoặc đỉnh stack có hợp với str[i] hay không
                        {
                            if (a.TOP() != '(')//không hợp
                            {
                                return false;//sai
                            }
                        }
                        if (str[i] == '}')//kiểm tra xem phần tử ngoặc đỉnh stack có hợp với str[i] hay không
                        {
                            if (a.TOP() != '{')//không hợp
                            {
                                return false;//sai
                            }
                        }
                        a.pop();//kiểm tra xong xóa nó đi
                    }
                    else //nếu như stack rỗng, không hợp lệ, có dấu mở mà không có đóng
                    {
                        return false;
                    }
                }
            }
            return a.isEmpty() == true;
            //nếu như cuối cùng stack vẫn rỗng các phần tử đã lấy ra kiểm tra phù hợp hết
        }
    }
}
