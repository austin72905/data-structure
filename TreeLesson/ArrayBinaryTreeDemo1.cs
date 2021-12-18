using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.TreeLesson
{
    class ArrayBinaryTreeDemo1
    {
        /*
            順序存儲二叉樹 (會在堆排序時用到)
            
            數組 [ 1、2、3、4、5、6、7 ]
            1. 樹據存放方式可以由數組、樹互相轉換
            2. 要求以數組方式存放
            3. 遍歷時，要可以用前序遍歷、中序遍歷、後序遍歷 來遍歷
            

            特點
            1. 通常只考慮完全二叉樹
            2. 第n個元素的左子節點在數組的下標為 2*n+1
            3. 第n個元素的右子節點在數組的下標為 2*n+2
            4. 第n個元素的父節點在數組的下標為 (n-1)/2
            5. n表示二叉樹中的第幾個元素(從0開始)


           樹長這樣
              1
            2   3
          4  5 6  7
         
        */

        public static void Run()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7 };
            ArrayBinaryTree arrayBinaryTree = new ArrayBinaryTree(arr);
            arrayBinaryTree.preOrder(); //1 2 4 5 3 6 7
        }

        class ArrayBinaryTree
        {
            private int[] arr; 
            
            public ArrayBinaryTree(int[] arr)
            {
                this.arr = arr;
            }
            public void preOrder()
            {
                this.preOrder(0); //看要從第幾個元素開始遍歷
            }
            //完成順序存儲二叉樹的前序遍歷
            public void preOrder(int n)
            {
                //如果數組為空 or lengh = 0
                if(arr==null || arr.Length == 0)
                {
                    Console.WriteLine("array is empty , can not iterator");
                    return;
                }
                //輸出當前元素
                Console.WriteLine(arr[n]);

                //向左遞歸遍歷
                if((2 * n + 1) < arr.Length)
                {
                    preOrder(2 * n + 1);
                }

                //向右遞歸遍歷
                if ((2 * n + 2) < arr.Length)
                {
                    preOrder(2 * n + 2);
                }

            }
        }
    }
}
