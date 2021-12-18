using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.GraphLesson
{
    class DepthFirstSearch
    {
        public static void Run()
        {
            GraphArray graphArray = new GraphArray(5);
            //要添加的節點
            List<string> vertexs = new List<string> { "A", "B", "C", "D", "E" };

            foreach (string vertex in vertexs)
            {
                graphArray.insertVertex(vertex);
            }

            //添加邊
            //按圖的關係
            graphArray.insertEdge(0, 1, 1); //A-B 關係
            graphArray.insertEdge(0, 2, 1); //A-C 關係
            graphArray.insertEdge(1, 2, 1); //B-C 關係
            graphArray.insertEdge(1, 3, 1); //B-D 關係
            graphArray.insertEdge(1, 4, 1); //B-E 關係

            graphArray.showGraph();

            //DFS 測試
            graphArray.DFSall();
        }


        public static void RunComp()
        {
            GraphArray graphArray = new GraphArray(8);

            // 要添加的節點
            List<string> vertexs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" };

            foreach (string vertex in vertexs)
            {
                graphArray.insertVertex(vertex);
            }

            //添加邊
            //按圖的關係
            graphArray.insertEdge(0, 1, 1);
            graphArray.insertEdge(0, 2, 1);
            graphArray.insertEdge(1, 3, 1);
            graphArray.insertEdge(1, 4, 1);
            graphArray.insertEdge(3, 7, 1);
            graphArray.insertEdge(4, 7, 1);
            graphArray.insertEdge(2, 5, 1);
            graphArray.insertEdge(2, 6, 1);
            graphArray.insertEdge(5, 6, 1);

            graphArray.showGraph();

            //DFS 測試
            graphArray.DFSall();
        }
        /*
            圖遍歷
            對圖中節點的訪問

            1. 深度優先遍歷
            2. 廣度優先遍歷
         
        */

        /*
             深度優先遍歷(DFS)
        
            1. 選一個初始節點，從這個節點訪問第一個鄰接節點，在將被訪問的鄰接節點設為初始節點
               反覆操作(遞歸)

            2. 縱向挖掘深入，不是對一個節點的所有鄰接節點進行橫向訪問

            算法步驟
            1. 訪問起始節點v，並標記已訪問
            2. 查找 v 的第一個鄰接節點 w  
                (1) 如果 w 不存在，返回 步驟1.
                (2) 如果 w  存在， 到步驟3.
            3. 如果w未被訪問，把 w 當作起始節點，在重新進行 步驟1、步驟2


            以圖深度優先遍歷來說
            節點的集合 ["A", "B", "C", "D", "E"]
            
            DFSall() i=0

            先訪問A 節點
            從[0,0] 開始訪問 => [0,1] 找到非0的值(代表找到第一個鄰接點)(對應到B)(A找到第一個與他鄰接的點B)，
            把找到的點B，重新當作起始節點 [1,0]=>[1,1](A訪問過了，跳過)=>[1,2]找到非0的值(對應到C)(B找到第一個與他鄰接的點C)，
            把找到的點C，重新當作起始節點 [2,0](A訪問過了，跳過)=>[2,1](B訪問過了，跳過)=>[2,3]....都是0，
            回到B [1,3]找到非0的值(對應到D)(B找到與他鄰接的點D)，
            把找到的點D，重新當作起始節點 [3,0]=>[3,1](B訪問過了，跳過)=>[3,2]....都是0，
            回到B [1,4]找到非0的值(對應到E)(B找到與他鄰接的點E)，
            把找到的點E，重新當作起始節點 [4,0]=>[4,1](B訪問過了，跳過)=>[4,2]...都是0，
            B 的鄰近節點訪問完畢，
            回到A [0,2](C訪問過了，跳過)=>[0,3]...都是0，
            
            DFSall() i=1

            開始訪問B (但在A 的時候其實都訪問過了..所以都跳過)
            
         
        */

        public class GraphArray
        {
            private List<string> vertexList; //節點的集合
            private int[,] edges;  //節點間的關係圖
            private int numOfEdges; // 邊的個數
            //定義一個bool 數組，紀錄節點是否已訪問過
            private bool[] isVisited;

            public GraphArray(int n)//n 為節點的個數
            {
                edges = new int[n, n];
                vertexList = new List<string>();
                isVisited = new bool[n];
                numOfEdges = 0;
            }

            
            /// <summary>
            /// 得到第一個連接節點的Index
            /// 要找的節點不存在，返回-1
            /// 存在的話返回對應的下標
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public int getFirstNeighbor(int index)
            {
                for (int i = 0; i < vertexList.Count; i++)
                {
                    // 0 代表不相鄰 
                    if (edges[index, i] > 0)
                    {
                        return i;
                    }
                }

                return -1;
            }

            //根據前一個鄰接節點的下標，來獲取下一個鄰接節點
            public int getNextNeighbor(int v1,int v2)
            {
                for (int i =v2+1; i < vertexList.Count; i++)
                {
                    // 0 代表不相鄰
                    if (edges[v1, i] > 0)
                    {
                        return i;
                    }
                }
                return -1;
            }

            //深度優先遍歷算法
            //i 第一次就是0
            private void DFS(bool[] isVisitedList,int i)
            {
                //打印訪問的節點
                Console.Write($"{ getValueByIndex(i)} => ");
                //將訪問過的節點，設為已訪問
                isVisitedList[i] = true;
                //查找節點i的第一個鄰近節點w
                int w = getFirstNeighbor(i); //他也像是把找到的節點當作是新的起始節點的感覺
                while (w != -1) //說明有鄰接節點
                {
                    //沒有被訪問過
                    if (!isVisitedList[w])
                    {
                        DFS(isVisitedList, w);
                    }
                    //如果w 已經被訪問過
                    //根據前一個鄰接節點的下標，來獲取下一個鄰接節點
                    w = getNextNeighbor(i, w);
                }
            }

            //對DFS 進行一個重載，遍歷所有的節點
            public void DFSall()
            {
                //遍歷所有節點，進行DFS
                for (int i = 0; i < getNumOfVertex(); i++)
                {
                    if (!isVisited[i])
                    {
                        DFS(isVisited, i);
                    }
                }
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
            public void insertEdge(int v1, int v2, int weight) //weight : 權值(邊對應的值)  沒有填就是0
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
                        Console.Write($"{edges[row, col],5}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
