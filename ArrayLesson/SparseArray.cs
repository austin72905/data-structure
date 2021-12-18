using System;

namespace CsharpOperation.ArrayLesson
{
    public static class SparseArray
    {
        //稀疏數組
        /*	需求:
			以二維數組紀錄棋盤，黑子為1;白子為2;未下子為0，
			=> 未下子時紀錄到很多不必要的數據
			=> 使用稀疏數組，進行壓縮

			(1)記錄方式
				row            col             val
				共有幾row      共有幾col        非0值的數量
				第幾row        第幾col          值
				第幾row        第幾col          值
		*/
        public static void Run()
        {
            SparseArr();
        }

        public static void SparseArr()
        {
            //建立一個稀疏矩陣5*5
            int[,] sparceOrigin = {
                {0 , 0, 0,27,0 },
                {0 , 0,13, 0,0 },
                {0 ,41,13, 0,0 },
                {52, 0, 9, 0,0 },
                {0 , 0, 0,18,0 },
            };

            int noZero=0;
            int originalRow = sparceOrigin.GetLength(0);//有幾個維度
            int originalCol = sparceOrigin.GetLength(1);//每個維度有幾個元素
            //打印這個原始的二維數組
            Console.WriteLine("==========原始的稀疏數組==========");

            for (int row = 0; row < originalRow; row++) 
            {
                for (int col = 0; col < originalCol; col++) 
                {
                    //打印每個元素
                    Console.Write($"{sparceOrigin[row,col],5}"); //格式化
                }
                Console.WriteLine();
            }

            //得到非0的個數
            for (int row = 0; row < originalRow; row++)
            {
                for (int col = 0; col < originalCol; col++)
                {
                    if (sparceOrigin[row, col] != 0)
                    {
                        noZero++;
                    }
                }
               
            }
            Console.WriteLine();
            Console.WriteLine($"非0的個數: {noZero}");

            //壓縮稀疏數組
            //row : 非0個數+1  => 因為第一行 要存放原始稀疏數組的大小、非0的個數
            //col : 3
            int[,] newArray = new int[noZero + 1, 3];
            //給第一行值
            newArray[0,0] =originalRow;
            newArray[0, 1] = originalCol;
            newArray[0, 2] = noZero;

            //將稀疏數組的值壓縮到新的數組
            int newArrayRow = 1; //第一行友值了，所以從第二行開始

            Console.WriteLine("==========壓縮後的數組==========");
            for (int row = 0; row < originalRow; row++)
            {
                for (int col = 0; col < originalCol; col++)
                {
                    if (sparceOrigin[row, col] != 0)
                    {
                        int noZeroVal = sparceOrigin[row, col]; //非0的值

                        newArray[newArrayRow, 0] = row; //紀錄非0值所在的row
                        newArray[newArrayRow, 1] = col; //紀錄非0值所在的col
                        newArray[newArrayRow, 2] = noZeroVal; //紀錄非0值

                        newArrayRow++;
                    }
                }

            }

            //打印壓縮後的數組
            for (int row = 0; row < newArray.GetLength(0); row++)
            {
                for (int col = 0; col < newArray.GetLength(1); col++)
                {
                    Console.Write($"{newArray[row, col],5}");
                }
                Console.WriteLine();
            }

            //將壓縮數組恢復成 稀疏數組
            int originRow = newArray[0, 0];
            int originCol = newArray[0, 1];
            int[,] backSparseArray = new int[originRow, originCol];

           

            for (int row = 1; row < newArray.GetLength(0); row++) //第二行開始
            {
                int oriRow = newArray[row, 0];
                int oriCol = newArray[row, 1];
                int oriVal = newArray[row, 2];
                backSparseArray[oriRow, oriCol] = oriVal;

            }

            Console.WriteLine("==========恢復後的數組==========");
            for (int row = 0; row < backSparseArray.GetLength(0); row++)
            {
                for (int col = 0; col < backSparseArray.GetLength(1); col++)
                {
                    //打印每個元素
                    Console.Write($"{backSparseArray[row, col],5}"); //格式化
                }
                Console.WriteLine();
            }

        }

    }
}
