using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.SortLesson
{
    class TimeComplex
    {
        /*
            時間複雜度
            
            計算程序執行的時間
            1. 事後統計: 讓程序跑，然後統計執行時間，
                缺點: 
                    (1)要先讓程式運行，如果數據龐大，統計時間很久
                    (2)會受到硬體的影響

            2. 事前統計: 分析算法的時間複雜度，判斷哪個程序更優

            時間頻度
            程序花費的時間與他執行語句的次數成正比，語句執行遇多，花費時間愈多
            語句執行次數，也就是時間頻度可記為 T(n)

            

            
            以時間複雜度圖1為例
            
            需求為: 求1~100 之和

            以算法1為例
            循環n次，最後在判斷是否退出迴圈1次
            T(n)=n+1

            算法2只執行一個語句
            T(n)=1

            => 不同的算法，雖然結果一樣，但耗費的時間會出現巨大差異，當n 越大，差距就越大
            

            特點 :
            1. 可以忽略常數項
            2. 可以忽略低次項
            3. 可以忽略系數  (次方數不能忽略)

            時間複雜度算法
            簡言之
            兩個函數的T(n) 相除，為非0的常數，代表兩個函數是同數量級的，代表是同一個時間複雜度的
                ， 計為T(n)=O(f(n))
            
            假設有個函數 T(n)=3n^2+5n+7
            算法:
            1. 把常數項改為1      =>3n^2+5n+1
            2. 只保留最高階項     =>3n^2
            3. 去除最高階項的係數 => n^2

            => 就能得出時間複雜度O  => O(n^2)
            
         
        */

        /*
            常見時間複雜度
            1. 常數階O(1)
            2. 對數階O(logN)
            3. 線性階O(n)
            4. 線性對數階O(nlogN)
            5. 平方階O(n^2)
        */

        /*
            平均時間複雜度、最壞時間複雜度 

            1. 時間複雜度一般都指最壞時間複雜度
                因為不管輸入為何，都能確保程序運行時間不會比這個邊界還要慢
            
            2. 平均時間複雜度、最壞時間複雜度 是否一致，與算法有關係
            
            從圖最壞時間複雜度來看
            基數、shell(希爾)、快速、歸併、堆 的排序方法 在n 較大時，O(nlogN)
            最慢也至少跟其他排序法一樣是O(n2)
            (冒泡、交換、選擇、插入)
         
        */

        //常數階(O(1))
        //所謂的語句執行次數，是指就算變量改變，程序執行次數都不會改變 (ex: 如果有for 之類的語句，就會改變)
        //無論這類代碼有多長，十幾萬行，都可以用O(1) 來表示
        public class TCO1
        {
            public static void Run()
            {
                int i = 1;
                int j = 2;
                i++;
                j++;
                int m = i + j;

            }
        }

        //對數階O(logN)
        //雖然看起來是n ，但i 逐漸向n 逼近
        //假設執行x 次後， i 就大於n 了
        //以下面的代碼就可以說 2 的 x 次方 等於 n 
        // x = logn
        // ex: n=1024 那 x =10   10=log1024
        // log 的底數是 2、3、4、5... 依演算法而定
        public class TCOlogN
        {
            public static void Run(int n)
            {
                int i = 1;
                while (i < n)
                {
                    i = i * 2;
                }

            }
        }

        //線性階O(n)
        public class TCOn
        {
            public static void Run(int n)
            {
                
                for (int i = 0; i < n; i++)
                {
                    i++;
                }

            }
        }

        //線性對數階O(nlogN)
        public class TCOnlogN
        {
            public static void Run(int n)
            {

                for (int i = 0; i < n; i++)
                {
                    while (i < n)
                    {
                        i = i * 2;
                    }
                }

            }
        }
        //平方階O(n^2)
        public class TCn2
        {
            public static void Run(int n)
            {

                for (int i = 0; i < n; i++)
                {
                    for (int  j = 0;  j <n;  j++)
                    {
                        i=i+j;
                    }
                }

            }
        }
    }
}
