using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.RecursionLesson
{
    class MiGong
    {
        /*
            迷宮回溯問題
            
        */
        public static void Run()
        {
            //創建迷宮
            int[,] map = new int[8, 7];

            //將周圍加上牆，設為1
            //上下
            for (int i = 0; i < 7; i++)
            {
                map[0, i] = 1;
                map[7, i] = 1;
            }
            //左右
            for (int i = 0; i < 8; i++)
            {
                map[i, 0]=1;
                map[i, 6]=1;
            }
            //隔板
            //map[3, 1] = 1;
            map[3, 2] = 1;
            map[4, 2] = 1;
            map[5, 2] = 1;
            map[6, 2] = 1;

            //打印第圖
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write($"{map[i,j],3}");   
                }
                Console.WriteLine();
            }

            //開始遞歸
            SetWay(map, 1, 1);
            Console.WriteLine("新地圖情況");
            //打印走的路徑
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write($"{map[i, j],3}");
                }
                Console.WriteLine();
            }
        }

        //使用遞歸回溯，來給小球路線
        /*
            約定
            map : 地圖
            起始位置: [1,1]
            目標位置: [6,5]
            當map[row,col] 為 0代表沒走過 ; 1代表牆 ; 2代表路通可走 ; 3 代表已經走過，但是走不通

            找到通路返回true

            行走策略:
            下 -> 右 -> 上 -> 左，
            如果該點走不通在回溯
            
            思考:
            如何走出最短路徑(在還沒學更好的算法前，可以改變行走策略試試)
    
        */
        public static bool SetWay(int[,] map, int row, int col)
        {
            //找到出口
            if (map[6, 5] == 2)
            {
                return true;
            }

            //如果沒有走過
            if (map[row, col] == 0)
            {
                //先假定這條路可以通
                map[row, col] = 2;
                if (SetWay(map, row + 1, col))//向下走
                {
                    return true;
                }
                else if (SetWay(map, row, col + 1))//向右走
                {
                    return true;
                }
                else if(SetWay(map, row-1, col))//向上走
                {
                    return true;
                }
                else if(SetWay(map, row, col - 1))//向左走
                {
                    return true;
                }
                else
                {
                    //代表走不通
                    map[row, col] = 3;
                    return false;
                }
            }

            //剩下就是遇到 1,2,3
            return false;
        }
        
    }
}
