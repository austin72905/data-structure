using System;

namespace CsharpOperation.QueueLesson
{
    class ArrayQueue
    {
        /*
            列隊
            特性:先進先出


            數組模擬對列
            1. rear: 列隊最後(包含)， 當元素入對列，rear 會改變
            2. front:列隊最前(不含)， 當元素出對列，front 會改變
            
            數組模擬對列
            新增元素到對列
            rear+1
            

            當 rear == front  代表列隊為空
            
            當 rear == maxSize-1 (到列隊最後了，無法在新增)
         
        */

        public static void Run()
        {
            CircleArrayToQueue circleArrayToQueue = new CircleArrayToQueue(4);
            circleArrayToQueue.addToQueue(5);
            circleArrayToQueue.showQueue();
            circleArrayToQueue.addToQueue(2);
            circleArrayToQueue.addToQueue(4);
            circleArrayToQueue.addToQueue(6);
            circleArrayToQueue.showQueue();
            circleArrayToQueue.getFromQueue();
            circleArrayToQueue.showQueue();
            circleArrayToQueue.getFromQueue();
            circleArrayToQueue.getFromQueue();
            circleArrayToQueue.getFromQueue();
            circleArrayToQueue.addToQueue(6);
            circleArrayToQueue.showQueue();
        }


        /*
            數組模擬對列
            缺點:
            記憶體用過後，就無法再使用了，被取出對列的數據排不掉

            解決: 將他改成環形的數組: 取模%
        */
        class ArrayToQueue
        {
            private int maxSize;
            private int rear;
            private int front;
            private int[] arr;
            public ArrayToQueue(int arrSize)
            {
                maxSize = arrSize;
                arr = new int[arrSize];
                front = -1; //指向對列頭的"前一個位置"
                rear = -1; //指向對列尾的數據(包含最後一個數據)
            }

            //判斷對列是否滿了
            public bool isFull()
            {
                return rear == maxSize - 1;

            }

            //判斷對列是否為空
            public bool isEmpty()
            {
                return rear == front;
            }

            //添加數據
            public void addToQueue(int n)
            {
                //對列沒滿才加
                if (isFull())
                {
                    Console.WriteLine("對列已滿");
                    return;
                }

                rear++;
                arr[rear] = n;
            }

            //獲取數據
            public int getFromQueue()
            {
                //不為空才能取
                if (isEmpty())
                {
                    Console.WriteLine("數據為空");
                    return 0;
                }
                front++;
                return arr[front];
            }

            //顯示數據
            public void showQueue()
            {
                if (isEmpty())
                {
                    Console.WriteLine("數據為空");
                    return;
                }

                for (int i = 0; i < maxSize; i++)
                {
                    Console.WriteLine($"{arr[i]}");
                }
            }
        }

        /*
            將數組做成環形模擬對列
            maxSize : 數組長度， 對列有效空間維 maxSize-1 (留一個空間作約定)
                        ，留空間是為了好計算，下面的規則可以自己用推的


            front : 指向第一個元素
            rear  : 指向最後一個元素的後一個

            初始值
            front = 0
            rear = 0

            對列為空
            front == rear

            對列滿了 (想像成front 跟 rear+1 的距離 等於 maxSize 時 代表滿了)
            (rear+1) % maxSize = front

            對列中有效的數據個數(對列裡目前有幾個值)
            (rear + maxSize - front) % maxSize     (取餘數)
        */
        class CircleArrayToQueue
        {
            private int maxSize;
            private int rear;
            private int front;
            private int[] arr;

            public CircleArrayToQueue(int arrSize)
            {
                maxSize = arrSize;
                arr = new int[arrSize];
                front = 0;
                rear = 0;
            }

            //判斷對列是否滿了
            public bool isFull()
            {
                return (rear + 1) % maxSize == front;

            }

            //判斷對列是否為空
            public bool isEmpty()
            {
                return rear == front;
            }

            //添加數據
            public void addToQueue(int n)
            {
                //對列沒滿才加
                if (isFull())
                {
                    Console.WriteLine("對列已滿");
                    return;
                }
                //本來就是尾元素的下一個位置，可以直接加
                arr[rear] = n;

                rear = (rear + 1) % maxSize; //模擬環狀的數組，當數組都裝滿元素時，要除以數組的長度，回到前面的index
            }

            //獲取數據
            public int getFromQueue()
            {
                //不為空才能取
                if (isEmpty())
                {
                    Console.WriteLine("數據為空");
                    return 0;
                }

                //front 本來就指向第一個元素
                int val = arr[front];
                front = (front + 1) % maxSize; //模擬環狀的數組，當數組都裝滿元素時，要除以數組的長度，回到前面的index
                Console.WriteLine($"取出值為{val}");
                Console.WriteLine("================");
                return val;
            }

            //顯示數據
            public void showQueue()
            {
                if (isEmpty())
                {
                    Console.WriteLine("數據為空");
                    return;
                }

                for (int i = front; i < front + validSize(); i++)
                {
                    Console.WriteLine($"arr[{i % maxSize}] = {arr[i % maxSize]}");
                }

                Console.WriteLine("================");
            }

            //對列中有效的數據個數
            public int validSize()
            {
                /*
                    rear = 4
                    front = 0
                    maxSize = 3

                    (4+3-0)%3=1
                                
                */
                return (rear + maxSize - front) % maxSize;
            }

            //顯示對首元素
            public int headQueue()
            {
                if (isEmpty())
                {
                    Console.WriteLine("數據為空");
                    return 0;
                }

                return arr[front];
            }
        }
    }

}
