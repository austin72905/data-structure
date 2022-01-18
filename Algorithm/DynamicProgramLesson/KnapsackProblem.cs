using System;

namespace CsharpOperation.Algorithm.DynamicProgramLesson
{
    class KnapsackProblem
    {
        /*
            
            動態規劃算法
            介紹
            1. 核心思想是: 將大問題分為多個小問題進行解決，從而一部部獲取最優解的算法
            2. 與分治算法類似，基本思想是將問題分解成多個子問題，先求子問題，在將子問題的解得到原問題的解
            3. 與分治不同的是，動態規劃求解的問題，經分解得到的紫問題往往不是互相獨立(即下一個子階段的求解是建立在上一個子階段的解的基礎上，進行近一步的求解)
            4. 可以通過填表的方式逐步推進，得到最優解
        

            背包問題(有分01背包(物品不能重複)、完全背包(可重複)，本題為01背包)
            
            容量為4磅
            有以下物品    

            物品    重量     價格
            吉他    1        1500
            音響    4        3000
            電腦    3        2000


            Q: 怎麼裝進背包裡，價值最大，請重量不超出?(物品不能重複)


            思路
            w : 重量
            val : 價格
            n : 共有幾個物品
            c : 背包的容量(可承受的重量)
            v[i][j] : (二維數組)假設目前放入i 個物品到背包了，當前背包的容量為j，假設當前背包中最大價值為v[i][j]
            每次遍歷到第 i 個物品，根據 w[i]、val[i]來確定是否需要將該物品放入背包中

            先用填表分析

            得到的規律
            1. v[i][0] = v[0][j]

                    => 表示填入表的第一行、第一列是0

            2. 當 w[i] > j 時，v[i][j] = v[i-1][j]

                    => 當加入的新商品，重量大於背包承重，直接用上面那格就好

            3. 當 j >= w[i] 時，v[i][j] = max{ v[i-1][j] , val[i]+v[i-1][j-w[i]] }

                    => 當加入新商品後，重量可以放入背包
                        (1) 如果沒有剩餘空間，又放新商品的價格比上一個還低，就直接用上一格的價格 : v[i-1][j]


                        (2) 放入新商品後(val[i])，還有剩餘空間，先放入新商品，加上放入前一個商品(i-1)時，在剩餘空間(j-w[i])的最大值 : v[i]+v[i-1][j-w[i]]

                        在(1)(2)取較大值
            
            
                
        */

        public static void Run()
        {

            //物品的重量
            int[] w = { 1, 4, 3 };
            //物品的價格
            int[] val = { 1500, 3000, 2000 };
            //背包的容量
            int m = 4;
            //物品的個數
            int n = val.Length;




            //創建二維數組
            // v[i][j] 假設目前放入i 個物品到背包了，當前背包的容量為j，假設當前背包中最大價值為v[i][j]
            int[,] v = new int[n + 1, m + 1];

            //為了記錄放入商品的情況，定一個二維數組
            int[,] path = new int[n + 1, m + 1];

            //初始化第一行、第一列，可以不處理，因為默認為0

            for (int i = 0; i < v.GetLength(0); i++)
            {
                v[i, 0] = 0;//將第一列設置為0
            }

            for (int i = 0; i < v.GetLength(1); i++)
            {
                v[0, i] = 0;//將第一行設置為0
            }

            //根據公式來實現動態規劃算法
            for (int i = 1; i < v.GetLength(0); i++)
            {
                for (int j = 1; j < v.GetLength(1); j++)
                {
                    if (w[i - 1] > j) //程序從1開始，w[i]要修改成w[i-1]
                    {
                        v[i, j] = v[i - 1, j];
                    }
                    else
                    {
                        //v[i, j] = Math.Max(v[i - 1, j], val[i - 1] + v[i - 1, j - w[i - 1]]);////程序從1開始，w[i]要修改成w[i-1]、val[i]要調整為val[i-1]
                        //為了記錄商品放入背包的情況，所以將上面的公式做個修正
                        if (v[i - 1, j] < val[i - 1] + v[i - 1, j - w[i - 1]])
                        {
                            v[i, j] = val[i - 1] + v[i - 1, j - w[i - 1]];

                            //將情況記錄到path 
                            path[i, j] = 1;
                        }
                        else
                        {
                            v[i, j] = v[i - 1, j];
                        }
                    }
                }
            }

            //輸出看下
            for (int row = 0; row < v.GetLength(0); row++)
            {
                for (int col = 0; col < v.GetLength(1); col++)
                {
                    //打印每個元素
                    Console.Write($"{v[row, col],5}"); //格式化
                }
                Console.WriteLine();
            }

            //for (int row = 0; row < path.GetLength(0); row++)
            //{
            //    for (int col = 0; col < path.GetLength(1); col++)
            //    {
            //        //打印每個元素
            //        Console.Write($"{path[row, col],5}"); //格式化
            //    }
            //    Console.WriteLine();
            //}

            /*
             path
                             0磅   1磅  2磅   3磅  4磅   <=背包容量           
              無商品           0    0    0    0    0
              吉               0   (1)   1    1    1
              吉+音            0    0    0    0    1
              吉+音+電         0    0    0    1   (1)

            path[3,4]才是我們想要的答案
            
             */

            //輸出最後是放入那些商品
            //顯示最後一個就好
            int prow = path.GetLength(0) - 1;
            int pcol= path.GetLength(1) - 1;
            while (prow > 0 && pcol > 0)//從最後一個遍歷
            {
                if (path[prow, pcol] == 1)
                {
                    //最後一個放入使用的公式是 val[i - 1] + v[i - 1, j - w[i - 1]];
                    //所以先找到了放入的商品val[i - 1]
                    Console.WriteLine($"第{prow}個商品放到了背包"); 
                    pcol -= w[prow - 1]; //將背包容量減去放入商品的重量 j-w[i] 的意思
                }
                prow--;
            }
            /*
                第3個商品放到了背包
                第1個商品放到了背包 
            */
        }
    }
}
