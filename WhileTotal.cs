using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;//直接使用那些靜態成員，而無須指涉型別名稱

namespace CsharpOperation
{
    class WhileTotal
    {
        public void GetWhileTotal()
        {
            //while 101~200偶數總和
            int total = 0;
            int firstNum = 101;
            while (firstNum < 200)
            {
                if (firstNum % 2 == 0)
                {
                    total += firstNum;
                }
                firstNum++;
            }            
            WriteLine(total);
            ReadKey();
        }

    }
}
