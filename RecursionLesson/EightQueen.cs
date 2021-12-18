using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.RecursionLesson
{
    class EightQueen
    {
        public static void Run()
        {
            Queen8 queen8 = new Queen8();
            queen8.check(0); //先放第一個皇后
            Console.WriteLine($"一共有{Queen8.solution_count}種解法");
            Console.WriteLine($"一共判斷了{Queen8.judge_count}次");
        }
        /*
            八皇后問題
        
            8*8 的西洋棋棋盤
            在上面擺8個皇后，使其不能互相攻擊(不能在各自的直、橫、斜行上)，問有幾種排法?
            (92 種排法)

            理論上用二維數組表示棋盤，但其實可以透過算法，用一個一維數組解決
            (表示排法) arr[8]={0,4,7,5,2,6,1,3}
            下標代表，第 1 個 皇后 ，排在 第1行、第 1 列
                     第 2 個 皇后 ，排在 第2行、第 5 列
                     第 3 個 皇后 ，排在 第3行、第 8 列

            arr[n]=val
            表示  第 n+1 個 皇后 ，排在 第n+1 行、第 val+1列


            算法步驟
            
            1. 第1個皇后，從第1行、第1列開始嘗試
            2. 第2個皇后，從第2行、第1列開始嘗試
                (1) 不會互相攻擊就放著
                (2) 會互相攻擊就第2行、第2列開試試看
            .... 直到第8個皇后


            (效率其實不高，來回檢測大概1萬5千多次)
            3. 回溯: 
                  1. 當解出一種排法時，會倒過來，從最後一個擺的，嘗試其他位置(只移動最後一個擺的旗子看能不能找到另一個正確解)
                  2. 找不到就在網前找前前一個擺的旗子，重複1的步驟
                  3. 最後回溯到第一個皇后
                  
        */
        public class Queen8
        {

            //定義一個max 表示共有多少皇后
            int max =8;
            //計算有幾組解法
            public static int solution_count=0;
            //判斷衝突的次數
            public static int judge_count = 0;
            //定義數組，保存皇后放置的結果
            int[] array = new int[8];

            //判斷擺的是第n個皇后時，該皇后是否會和前面擺放的皇后互相攻擊
            private bool judge(int n)
            {
                judge_count++;
                //要和前幾個擺的比較
                for (int i = 0; i <n; i++)
                {
                    //判斷第n 個皇后是否和前面擺過的皇后在同一列  array[i] == array[n]
                    //判斷第n 個皇后是否和前面擺過的皇后在同一斜線  Math.Abs(n-i) ==Math.Abs(array[n]-array[i])(陣列的值與皇后所在的行列有規律)
                    // 行的差值 與 列的差值一樣，代表在同一斜線
                    if (array[i] == array[n] || Math.Abs(n-i) ==Math.Abs(array[n]-array[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            //放置第n 個皇后
            //chekc 是在for裡面，會進行回溯
            public void check(int n)
            {
                //代表8個皇后都已經放好了
                if (n == max)
                {
                    print();
                    return;
                }

                //依次放入皇后，並判斷是否衝突
                for (int i = 0; i < max; i++)
                {
                    //先把當前皇后 n ，放到該行的第1列
                    array[n] = i;
                    //判斷當放置第n個皇后到i 列時，是否衝突
                    if (judge(n))//不衝突
                    {
                        //接著放n+1個皇后
                        check(n + 1);
                    }

                }
            }

            //寫一個方法，將皇后擺放的位置輸出
            public void print()
            {
                solution_count++;
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"{array[i],3} ");
                }
                Console.WriteLine();
            }

            

        }
    }
}
