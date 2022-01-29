using System;
using System.Linq;

namespace CsharpOperation.Algorithm.DijkstraLesson
{
    class DijkstraLessonDemo1
    {

        /*
            Dijkstra 算法
        
            主要特點是以起點為中心向外層層括展(廣度優先)
            

            算法過程
            會有3個數組
            1. 各個頂點集合: V ，裡面各個頂點為vi => V[v1,v2...vi]
            2. 出發頂點到其他頂點距離集合: Dis，紀錄出發點v，到各頂點的距離，v到vi，的距離為di => Dis[d1,d2,d3....di]
            3. 紀錄前一個節點的集合: pre_visited

            出發點: v
            
            步驟
            1. 從Dis中，選出值最小的di，移出Dis，同時移出V中對應的頂點vi，此時v到vi即為最短路徑

            2. 更新Dis，規則為:
                 比較v與各頂點(V)的距離值，與 "v通過vi" 到各頂點的距離值，保留較小的一個
                 同時更新節點的前一個節點vi，表示是通過vi 到達的
            3. 重複1、2，直到最短路徑頂點為目標頂點即可結束



            問題
            有七個村庄(A,B,C,D,E,F,G)，現在有6個郵差，從G點出發，需要分別把郵件送到A~F 這6個村莊
            每個村庄的距離用邊線表示(權)
            如何計算出G村莊到其他村莊的最短距離?
            如果從其他點出發到各個點的最短距離又是多少?



            過程
            1. 創建一個vi 實例，裡面有3個數組
                (1) already_arr : 已經訪問過的頂點，訪問過=1; 未訪問過=0  
                (2) dis : 出發頂點到其他頂點的距離，初始化先設置一個值
                (3) pre_visited : 前一個訪問頂點是哪個，未訪問前是-1                                 
        */

        public static void Run()
        {
            char[] vertex = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            const int N = 65535;//表示不可連接
            int[,] matrix = new int[,]{
                { N,5,7,N,N,N,2 },
                { 5,N,N,9,N,N,3 },
                { 7,N,N,N,8,N,N },
                { N,9,N,N,N,4,N },
                { N,N,8,N,N,5,4 },
                { N,N,N,4,5,N,6 },
                { 2,3,N,N,4,6,N }
            };

            //創建Graph
            Graph graph = new Graph(vertex, matrix);
            graph.ShowGrapth();
            //從G點出發
            graph.DijkstraAlo(2);
            graph.ShowResult();
        }

        class Graph
        {
            private char[] vertex; //頂點數組v
            private int[,] matrix; //鄰接矩陣(各頂點的關係圖)
            private VisistedVertex vv; //表示已經訪問頂點的集合

            public Graph(char[] vertex, int[,] matrix)
            {
                this.vertex = vertex;
                this.matrix = matrix;
            }

            //顯示圖(各頂點的距離關係)
            public void ShowGrapth()
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {

                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 65535)
                        {
                            Console.Write($"{"N",5 }");
                        }
                        else
                        {
                            Console.Write($"{matrix[i, j],5}");
                        }

                    }

                    Console.WriteLine();

                }
            }

            //Dijkstra 算法
            //index : 出發頂點對應的下標
            public void DijkstraAlo(int index)
            {
                vv = new VisistedVertex(vertex.Length, index);
                update(index); //更新index頂點到周圍頂點的距離和前一個節點

                //因為上面已經完成過一次所以這邊少做一次，所以從1開始
                for (int i = 1; i < vertex.Length; i++)
                {
                    index = vv.UpdateArr(); //選擇並返回新的訪問節點
                    update(index);
                }


            }

            //更新index下標頂點到周圍頂點的距離和周圍頂點的前一個節點
            private void update(int index)
            {
                int len = 0;
                //根據鄰接矩陣G的那行
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    //出發頂點到index頂點的距離+index頂點到i 頂點的和
                    /*
                        假設出發點G，就是看G那行，G與各頂點的距離 { 2,3,N,N,4,6,N }
                    */
                    len = vv.GetDis(index) + matrix[index, i];
                    //如果i 這頂點沒有被訪問過，且len小於出發頂點到i頂點的距離，就需要更新(因為是求最小距離)
                    if (!vv.In(i) && len < vv.GetDis(i))
                    {
                        vv.UpdatePre(i, index); //更新i 頂點的前一個節點為index頂點
                        vv.UpdateDis(i, len);//更新出發頂點到i頂點的距離
                    }

                    
                }

                //想追蹤pre_visited的變化，藉此找到他是怎麼走到的，但好像沒辦法 以C為出發點訪問D時會怪怪的
                //更新: (不需要這樣，可以直接用最後的pre_visited來推)
               
                //Console.WriteLine($"每輪already_arr : [ {string.Join(",", vv.already_arr.Select(o => o))} ]");
                //Console.WriteLine($"每輪dis : [ {string.Join(",", vv.dis.Select(o => o))} ]");
                //Console.WriteLine($"每輪pre_visited : [ {string.Join(",", vv.pre_visited.Select(o => o))} ]");
                //Console.WriteLine();
            }

            public void ShowResult()
            {
                vv.show();
            }
        }

        class VisistedVertex
        {
            //已經訪問過的頂點，訪問過=1; 未訪問過=0
            public int[] already_arr;
            //前一個訪問頂點是哪個，未訪問前是0
            public int[] pre_visited;
            // 出發頂點到其他頂點的距離，初始化先設置一個值
            public int[] dis;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="length">頂點個數</param>
            /// <param name="index">從哪個頂點出發，對應的下標，如G 就是6</param>
            public VisistedVertex(int length, int index)
            {
                this.already_arr = new int[length];
                this.pre_visited = new int[length];
                this.dis = new int[length];
                //初始化
                for (int i = 0; i < dis.Length; i++)
                {
                    if (i == index)
                    {
                        //出發的頂點與自身距離=0
                        dis[i] = 0;
                    }
                    else
                    {
                        dis[i] = 65535;
                    }

                }

                //pre_visited 初始化為-1 不知道會不會好點
                for (int i = 0; i < pre_visited.Length; i++)
                {
                    pre_visited[i] = -1;
                }

                this.already_arr[index] = 1; //設置出發頂點被訪問過
            }

            //顯示最後結果
            public void show()
            {
                Console.WriteLine($"already_arr : [ {string.Join(",", already_arr.Select(o => o))} ]");
                //以G為出發點的話，dis最後就是G到各頂點的最短距離
                Console.WriteLine($"dis : [ {string.Join(",", dis.Select(o => o))} ]");
                Console.WriteLine($"pre_visited : [ {string.Join(",", pre_visited.Select(o => o))} ]");

                char[] vertex = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
                int index = 0;
                for (int i = 0; i < dis.Length; i++)
                {
                    //從哪點出發
                    if (dis[i] == 0)
                    {
                        index = i;
                        Console.WriteLine($"{vertex[i]} 與各頂點的距離");
                    }
                }

                for (int i = 0; i < dis.Length; i++)
                {
                    if (dis[i] != 0)
                    {
                        Console.Write($"{vertex[i]} ({dis[i]})  ");
                    }

                }
                Console.WriteLine();

                //打印是怎麼走的(需要用到pre_visited，前一個經過的節點不是G，代表無法從G直達，有經過其他的)
                /*
                    可以用最後的pre_visited 來推
                    以G到D為例， pre_visited 記錄前一個經過的頂點為F(下標5)
                    可以得知D前一個為F  (F=>D)
                    如何得知 F 以前的路徑為何?
                    可以找G到F pre_visited 記錄
                    找到對應為G
                    因此找到經過的路徑為(G=>F=>D)
                    要用倒敘的方式找

                    pre_visited 記錄 的是最短路徑下的上一個節點，F=>D 是最短路徑，G=>F 也是最短路徑，所以G=>F=>D 肯定是最短路徑 
                */
                //這邊是自己寫的
                for (int i = 0; i < pre_visited.Length; i++)
                {
                    //紀錄的上一個節點是出發點
                    if (pre_visited[i] == index)
                    {
                        Console.WriteLine($"{vertex[pre_visited[i]]}=>{vertex[i]} 距離 {dis[i]}");
                    }
                    else
                    {
                        string result =$"{vertex[i]}";
                        int last = pre_visited[i];
                        
                        if (last == -1)
                        {
                            continue;
                        }
                        while (last != index)
                        {
                            result = $"{vertex[last]}=> " + result;
                            last = pre_visited[last];
                                                      
                        }
                        //跳出來代表 pre_visited[i] == index 了
                        result = $"{vertex[index]}=>" + result;
                        Console.WriteLine($"{result} 距離 {dis[i]}");
                    }
                    
                    
                }
                /*
                              
                    already_arr : [ 1,1,1,1,1,1,1 ]
                    dis : [ 2,3,9,10,4,6,0 ]
                    pre_visited : [ 6,6,0,5,6,6,0 ]



                    出發點為C
                    already_arr : [ 1,1,1,1,1,1,1 ]
                    dis : [ 7,12,0,17,8,13,9 ]
                    pre_visited : [ 2,0,-1,5,2,4,0 ]
                 
                */
            }

            //判斷index頂點是否訪問過，訪問過反回true; 未訪問訪回false
            public bool In(int index)
            {
                return already_arr[index] == 1;
            }
            //更新出發頂點到index頂點的距離
            public void UpdateDis(int index, int len)
            {
                dis[index] = len;
            }

            //更新頂點的前一個為index的節點
            public void UpdatePre(int pre, int index)
            {
                pre_visited[pre] = index;
            }

            //返回出發頂點到index頂點的距離
            public int GetDis(int index)
            {
                return dis[index];
            }

            ////如何選取下一個頂點? 比如G完後，就是A點作為新的訪問頂點(注意不是新的出發頂點)
            public int UpdateArr()
            {
                int min = 65535;
                int index = 0;
                for (int i = 0; i < already_arr.Length; i++)
                {
                    //還未訪問過
                    if (already_arr[i] == 0 && dis[i] < min)
                    {
                        min = dis[i];
                        index = i;
                    }
                }

                //更新index頂點被訪問過
                already_arr[index] = 1;
                return index;
            }
        }
    }
}
