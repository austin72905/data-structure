using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.GraphLesson
{
    class BroadFirstSearch
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
            graphArray.BFSall();
        }
        /*
            廣度優先算法(BFS)
            需要使用對列保存訪問過的節點順序，以便按這順序訪問這些節點的鄰街節點

            先從A能訪問到的先訪問完，在從B開始訪問

            算法步驟，以圖廣度優先遍歷為例

            從[0,0]=>[0,1]....開始遍歷，把非0的座標 放進隊列 [a,b,c]

            在從[a,b,c] 中依序取出元素，訪問座標
            [a,0]=>[a,1]=>[a,2]....把非0的座標 放進隊列
            [b,0]=>[b,1]=>[b,2]... 重複上面步驟

            訪問過的做個標記(不放入隊列)，值到隊列為空


            用一個隊列管理，節點下一個的鄰接節點的座標
            1. 這樣確保一個節點，都先找完他所有的鄰接節點
            2. 在從隊列中，取得下一個鄰接節點的座標，重複步驟1

            具體步驟:
            從A 節點開始訪問  隊列 : [0]  (打印A)
            從隊列取出0 ，訪問 [0,1]=>[0,2]=>[0,3]=>[0,4]... ，將非0的座標放入隊列(訪問過的不放入)， 隊列: [1,2] (打印B、打印C)
            從隊列取出1 ，訪問 [1,1]=>[1,2]=>[1,3]=>[1,4]... ，將非0的座標放入隊列(訪問過的不放入)， 隊列: [2,3,4] (打印D、打印E)
            從隊列取出2 ，訪問 [2,1]=>[2,2]=>[2,3]=>[2,4]... ，將非0的座標放入隊列(訪問過的不放入)， 隊列: [3,4]
            後續從隊列取出3、4，前面都訪問過了，所以不會加入隊列
            
            
        */

        public static void RunComp()
        {
            GraphArray graphArray = new GraphArray(8);

            // 要添加的節點
            List<string> vertexs = new List<string> { "1", "2", "3", "4", "5","6","7","8" };

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
            graphArray.BFSall();
        }

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
            public int getNextNeighbor(int v1, int v2)
            {
                for (int i = v2 + 1; i < vertexList.Count; i++)
                {
                    // 0 代表不相鄰
                    if (edges[v1, i] > 0)
                    {
                        return i;
                    }
                }
                return -1;
            }


            //對一個節點進行廣度優先遍歷
            private void BFS(bool[] isVisitedList,int i)
            {
                int u; //表示隊列頭節點的下標
                int w; //鄰接節點w

                //隊列，節點訪問的順序
                Queue<int> queue = new Queue<int>();

                //輸出訪問到節點的訊息
                Console.Write($"{getValueByIndex(i)} => ");
                //標記為已訪問
                isVisitedList[i] = true;

                //將結點的下標加入隊列
                queue.Enqueue(i);

                while (queue.Count != 0)
                {
                    //取出對列的頭節點下標
                    u = queue.Dequeue();
                    //得到第一個鄰街節點的下標
                    w = getFirstNeighbor(u);

                    while (w != -1)//找到鄰接節點
                    {
                        //沒被訪問過
                        if (!isVisitedList[w])
                        {
                            //輸出訪問到節點的訊息
                            Console.Write($"{getValueByIndex(w)} => ");
                            //標記為已訪問
                            isVisitedList[w] = true;
                            //入對列
                            queue.Enqueue(w);
                        }
                        
                        w = getNextNeighbor(u, w);//體現出廣度優先
                    }
                }
            }

            //遍歷所有節點，都進行廣度優先搜索
            public void BFSall()
            {
                for (int i = 0; i < getNumOfVertex(); i++)
                {
                    if (!isVisited[i])
                    {
                        BFS(isVisited, i);
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
