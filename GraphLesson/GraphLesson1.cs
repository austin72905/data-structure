using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.GraphLesson
{
    class GraphLesson1
    {
        public static void Run()
        {
            GraphArray graphArray = new GraphArray(5);
            //要添加的節點
            List<string> vertexs = new List<string> {"A","B","C","D","E" };

            foreach(string vertex in vertexs)
            {
                graphArray.insertVertex(vertex);
            }

            //添加邊
            //按圖的關係
            graphArray.insertEdge(0,1,1); //A-B 關係
            graphArray.insertEdge(0, 2, 1); //A-C 關係
            graphArray.insertEdge(1, 2, 1); //B-C 關係
            graphArray.insertEdge(1, 3, 1); //B-D 關係
            graphArray.insertEdge(1, 4, 1); //B-E 關係

            graphArray.showGraph();
        }
        /*
            圖
            1. 多對多關係的節點

            2. 兩個節點之間的連接:  邊
            3. 節點可以稱為頂點(vertex)
            4. 邊可分為 有方向、沒方向
            5. 點到點 可稱為路徑 (如圖的基本介紹 D=>C 路徑有: D=>B=>C 、 D=>A=>B=>C)
            6. 帶權圖 (邊有帶值的圖，也可叫網)

            圖的表示方式
            1. 鄰接矩陣 : 把節點間的關係用二維數組表示(浪費空間)，存的值0: 不能直接連接 ; 1: 直接連接
            2. 鄰接表 : 把節點間的關係用 數組 + 鍊表表示 (圖 ex: 1、2、3、4)

         
        */

        /*
             鄰接矩陣 呈現 圖

             1. 節點以 string 表示，以 array 存放
             2. array 表示節點間的關係
        */

        public class GraphArray
        {
            private List<string> vertexList; //節點的集合
            private int[,] edges;  //節點間的關係圖
            private int numOfEdges; // 邊的個數

            public GraphArray(int n)//n 為節點的個數
            {
                edges = new int[n, n];
                vertexList = new List<string>();
                numOfEdges = 0;
            }

            //插入節點
            public void insertVertex(string vertex)
            {
                vertexList.Add(vertex);
            }
            /// <summary>
            /// 添加邊
            /// </summary>
            /// <param name="v1">頂點對應的index</param>
            /// <param name="v2">頂點對應的index</param>
            /// <param name="weight"></param>
            public void insertEdge(int v1,int v2,int weight) //weight : 權值(邊對應的值)  沒有填就是0
            {
                edges[v1, v2] = weight;
                edges[v2, v1] = weight;
                //邊數++
                numOfEdges++;

            }

            //圖中常用的方法

            //返回節點的個數
            public int getNumOfVertex()
            {
                return vertexList.Count;
            }

            //返回邊的個數
            public int getNumOfEdges()
            {
                return numOfEdges;
            }

            //返回下標對應的值
            public string getValueByIndex(int i)
            {
                return vertexList[i];
            }

            //返回權值
            public int getWeight(int v1, int v2)
            {
                return edges[v1, v2];
            }

            //顯示圖按例1對應的矩陣
            public void showGraph()
            {
                for (int row = 0; row < edges.GetLength(0); row++)
                {
                    for (int col = 0; col < edges.GetLength(1); col++)
                    {
                        Console.Write($"{edges[row,col],5}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
