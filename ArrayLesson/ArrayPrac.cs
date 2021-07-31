using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;//直接使用那些靜態成員，而無須指涉型別名稱

namespace CsharpOperation.ArrayLesson
{
    class ArrayPrac
    {
        //dim 表維度
        //一維陣列
        public void GetOneDim()
        {
            //小名考試的分數
            int[] score ={45, 30, 21};

            //GetUpperBound(維度): 取得(維度)last index
            //GetLowerBound(維度): 取得(維度)first index
            for (int i=0;i <= score.GetUpperBound(0);i++)
            {
                Write($"{score.GetValue(i),3}");//3 元素空格
            }

            ReadKey();
        }

        //二維陣列
        public void GetTwoDim()
        {
            //init a 3*4 two dim arr
            int[,] numbox = {
                {1,2,3,4 },
                {5,6,7,8 },
                {11,12,13,14 },
            };
            //GetLength(維度); 該(維度)長度 
            int row = numbox.GetLength(0);
            int col = numbox.GetLength(1);

            //print val in numbox
            for(int i=0; i< row ;i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Write($"{numbox[i,j],3}");                   
                }
                WriteLine();
            }
            ReadKey();

        }


        public void GetTwoDim2()
        {
            string[] student = { "app", "ban", "cat" };
            foreach(string item in student)
            {
                Write("{0,7}",item);//空格
            }
            WriteLine();
            //成績 3*3
            int[,] score = {
                {1,2,3},
                {5,6,7},
                {11,12,13},
            };
            //莊總分
            int[] total =new int[3];

            //GetLength(維度); 該(維度)長度 
            int row = score.GetLength(0);
            int col = score.GetLength(1);
            for (int i=0;i < row ;i++)
            {
                for(int j=0;j < col ;j++)
                {
                    Write($"{score[i,j],7}");//空格
                }
                WriteLine();
                total[0] += score[i, 0]; //col 加起來
                total[1] += score[i, 1]; //col 加起來
                total[2] += score[i, 2]; //col 加起來

            }
            WriteLine();
            Write($"合計: {total[0]} {total[1],6} {total[2],6}");
            ReadKey();
        }

        //三維陣列
        public void GetTwoDim3()
        {
            //3 dim arr  3(table)*2(row)*3(col)
            int[,,] arr3d = {
                {
                    {1,2,3 },
                    {4,5,6 } 
                },
                {
                    {7,8,9 },
                    {10,12,13 }
                },
                {
                    {20,21,22 },
                    {30,31,13 }
                }
            };

            //取得個維度的index 個素
            int tab = arr3d.GetLength(0);
            int row =arr3d.GetLength(1);
            int col = arr3d.GetLength(2);
            //用3層 for 印出元素
            for(int one=0; one<tab; one++)
            {
                WriteLine($"table :{one+1}");
                for (int two = 0; two < row; two++)
                {
                    for (int thr = 0; thr < col; thr++)
                    {
                        Write($"{arr3d[one, two, thr],3}");
                    }
                    WriteLine();
                }
                WriteLine();
            }
            ReadKey();   
                    
        }

        //不規則陣列
        public void UnReguArr()
        {
            //使用陣列中的陣列宣告
            string[][] lesson = new string[][]
                {
                    new string[] {"paul","國文","英文" },
                    new string[] {"lee","國文","英文" },
                    new string[] {"sana","國文","英文","數學" },
                };

            for(int i=0;i<lesson.Length;i++)//3
            {
                for(int j=0;j<lesson[i].Length ;j++)//3、3、4
                {
                    Write($"{lesson[i][j],-6}"); //靠左對齊
                }
                WriteLine();
            }
            ReadKey();
        }

        //矩陣 (二維陣列)
        public void GetMatrixArr()
        {
            //declare兩個two dim
            int[,] mar1 = {
                {1,2 },
                {3,4 },
                {5,6 },
            };

            int[,] mar2 = {
                {7,9,11 },
                {8,10,12 },
            };

            //將兩個矩陣相乘
            int row1 = mar1.GetLength(0);

            int col1 = mar1.GetLength(1);
            int col2 = mar2.GetLength(1);
            int[,] MatrixMul = new int [row1,col2]; //矩陣乘法是第一個矩陣(row)*第二個矩陣(col)

            for(int i=0;i < row1; i++)//乘出來的矩陣 row = row1 
            {
                for(int j=0;j < col2; j++)//乘出來的矩陣 col =col2
                {
                    for(int k=0;k < col1; k++) //這層for迴圈 跳出點在mar1的col 乘完
                    {
                        MatrixMul[i, j] += mar1[i, k] * mar2[k, j];
                        //mar1[i, k] 很正常 一row 一row的乘完
                        //mar2[k, j] 他是要直的乘 是col-maj  col1=row2  A=n*m的矩陣， B=m*p的矩陣，要col1=row2才有辦法相乘

                    }

                }
            }

            for(int a=0;a< row1; a++)
            {
                for(int b=0;b < col2 ;b++)
                {
                    Write($"{MatrixMul[a,b],-6}");
                }
                WriteLine();
            }


            ReadKey();

        }

        //轉置矩陣
        public void ReverMatrixArr()
        {
            int[,] mar1 = {
                {1,2 },
                {3,4 },
                {5,6 },
            };

            int row = mar1.GetLength(0);
            int col =mar1.GetLength(1);

            //n*m => m*n
            int[,] reverseMatrix = new int[col, row];
            for(int i=0;i<col ;i++)
            {
                for(int j=0;j<row ;j++)
                {
                    reverseMatrix[i, j] = mar1[j, i];

                    Write($"{reverseMatrix[i, j],3}");
                }
                WriteLine();
            }           
            ReadKey();

        }

        //稀疏矩陣(元素很多0)，可用3-tuple結構存放
        public void SparceMatrixArr()
        {
            //建立一個稀疏矩陣
            int[,] sparce = {
                {0,0,0,27,0},
                {0,0,13,0,0 },
                {0,41,13,0,0 },
                {52,0,9,0,0 },
                {0,0,0,18,0 },
            };

            int Nozero = 0;
            int row = sparce.GetLength(0);
            int col = sparce.GetLength(1);
            WriteLine("-----稀疏矩陣-----");
            for (int i=0;i < row ;i++)
            {
                for(int j=0;j < col ;j++)
                {
                    if(sparce[i, j]!=0)
                    {
                        Nozero++;
                    }
                    Write($"{sparce[i,j],5}");
                }
                WriteLine();
            }
            WriteLine("非零個數: "+ Nozero);
            WriteLine("-----壓縮後的矩陣-----");
            int[,] Matrix = new int[Nozero+1, 3];
            //3元式 first row 代表 稀疏矩陣的row 、 col 、 非0個數
            //      之後的row      非0元素的座標[x,y]、值       
            Matrix[0, 0] = row;
            Matrix[0, 1] = col;
            Matrix[0, 2] = Nozero;

            //之後資料會從row 1 開始加入
            int idx = 1;

            for(int i=0;i<row ;i++)
            {
                for(int j=0;j<col ;j++)
                {
                    //將非0元素的座標[x,y]、值 加到 Matrix
                    if (sparce[i,j]!=0)
                    {
                        Matrix[idx, 0] = i + 1; //第幾row   index從0開始所以+1
                        Matrix[idx, 1] = j + 1; //第幾col 
                        Matrix[idx, 2] = sparce[i, j];
                        idx++;
                    }
                }
            }

            for(int i=0;i<Nozero+1 ;i++)
            {
                for(int j=0;j<3 ;j++)
                {
                    Write($"{Matrix[i, j],5}");
                }
                WriteLine();
            }

            ReadKey();


        }

    }
}
