using CsharpOperation.Algorithm.DijkstraLesson;
using CsharpOperation.Algorithm.DynamicProgramLesson;
using CsharpOperation.Algorithm.GreedLesson;
using CsharpOperation.Algorithm.KMPLesson;
using CsharpOperation.ArrayLesson;
using CsharpOperation.GraphLesson;
using CsharpOperation.HashTableLesson;
using CsharpOperation.LinkedListLesson;
using CsharpOperation.QueueLesson;
using CsharpOperation.RecursionLesson;
using CsharpOperation.TreeLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;//直接使用那些靜態成員，而無須指涉型別名稱

namespace CsharpOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            //new WhileTotal().GetWhileTotal();

            //陣列
            //--------------------------------------------
            //new ArrayPrac().GetOneDim();//一維陣列
            //new ArrayPrac().GetTwoDim();//二維陣列
            //new ArrayPrac().GetTwoDim2();//二維陣列
            //new ArrayPrac().GetTwoDim3();//三維陣列
            //new ArrayPrac().UnReguArr(); //不規則陣列
            //new ArrayPrac().GetMatrixArr(); //相乘矩陣
            //new ArrayPrac().ReverMatrixArr(); //轉置矩陣
            //new ArrayPrac().SparceMatrixArr(); //稀疏矩陣
            //SparseArray.Run();
            //---------------------------------------------
            //對列
            //ArrayQueue.Run();
            //鏈結串列
            //---------------------------------------------
            //單鍊表
            //LinkedList1.Run();

            //遞歸
            //地圖回溯
            //MiGong.Run();
            //八皇后
            //EightQueen.Run();

            //哈希表
            //HashTable1.Run();

            //樹
            //BinaryTreeDemo1.Run();
            //ArrayBinaryTreeDemo1.Run();
            //ThreadedBinaryTreeDemo1.Run();
            //HeapSortDemo1.Run();
            //BinarySortTreeDemo1.Run();
            //AVLTreeDemo1.Run();

            //圖
            //GraphLesson1.Run();
            //DFS
            //DepthFirstSearch.Run();
            //BFS
            //BroadFirstSearch.Run();
            //DFS、BFS 比較
            //CompareBFSDFS.Run();


            //------------演算法----------------
            //貪心算法
            //GreedLessonDemo1.Run();

            //動態規劃算法
            //KnapsackProblem.Run();


            //KMP算法
            //KMPLessonDemo1.Run();

            //Dijkstra 算法
            DijkstraLessonDemo1.Run();
            ReadKey();

        }
    }
}
