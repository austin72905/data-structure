using System;

namespace CsharpOperation.TreeLesson
{
    class HeapSortDemo1
    {

        /*
            堆排序

            效率是所有排序最高的、也是很省空間只要O(1)
            時間複雜度很穩定效率都是O(n*logn)
            但他本身是不穩定排序

            缺點是
            如果數據很頻繁變化，堆很難維護
            因此最常用的還是快速排序法，堆排序僅在數據更新十分不頻繁的時候才使用

            1. 會應用到順序存儲二叉樹
            2. 都用數組表示
            3. 時間複雜度為O(n*logn)


            大頂堆
            1. 每個節點>左子節點、右子點(左右誰大無所謂)
            2. arr[n] >= arr[2n+1] && arr[n] >= arr[2n+2]
            3. 常用於升序排列

            小頂堆
            1. 每個節點<左子節點、右子點(左右誰大無所謂)
            2. arr[n] <= arr[2n+1] && arr[n] <= arr[2n+2]
            3. 常用於降序排列
        

            實現思路
            1. 將待排序的序列排成一個大/小頂堆 (操作數組)
                (1)從最後一個非葉節點開始 (arr.lengh/2-1) (從左到右,從下到上)
                    將該節點與其子傑點進行比較，找出最大的節點，與其交換
                (2)找到倒數第2個非葉節點，重複上述步驟
                (3)如果造成子樹的結構混亂，要把他調整成正確結構

            2. 此時序列最大值，就是頂堆的根結點
            3. 將跟節點與末尾元素交換，此時末尾就是最大值，之後就先把最大值先晾在旁邊
            4. 將剩餘的n-1個節點重新執行(1)(2)(3)，如此反覆，最後得到一個有序的序列

         */


        public static void Run()
        {
            //要求數組進行升序排列
            int[] arr = { 4, 6, 8, 5, 9 };

            /*
             
            */

            heapSort(arr);


        }
        //堆排序
        public static void heapSort(int[] arr)
        {
            int temp = 0;
            Console.WriteLine("堆排序");
            //adjustHeap(arr, 1, arr.Length);
            //Console.WriteLine($"第一次: [{string.Join(", ", arr)}]");

            //adjustHeap(arr, 0, arr.Length);
            //Console.WriteLine($"第二次: [{string.Join(", ", arr)}]");

            //1. 將一個數組(二叉樹)調整成一個大頂堆
            for (int i = arr.Length / 2 - 1; i >= 0; i--)
            {
                adjustHeap(arr, i, arr.Length);
                //Console.WriteLine($"第x次: [{string.Join(", ", arr)}]");
            }

            ////2. 
            for (int j = arr.Length - 1; j > 0; j--)
            {
                //將跟節點與末尾元素交換，此時末尾就是最大值
                temp = arr[j];
                arr[j] = arr[0];
                arr[0] = temp;
                //Console.WriteLine($"數組: [{string.Join(", ", arr)}]");
                /*
                    把數組從下標為0開始，長度為j的數組，繼續調整成一個大頂堆
                    即以數組的第一個元素為跟節點開始調整
                    因為以arr[0]維根節點的樹，該樹下面的每一個 子樹都已經是大頂堆了
                    調用adjustHeap 只是要把最大值丟到array[0]
                    方便
                    temp = arr[j];
                    arr[j] = arr[0];
                    arr[0] = temp;
                    這段代碼把最大值丟到末尾而已
                */
                adjustHeap(arr, 0, j);
            }
            Console.WriteLine($"數組: [{string.Join(", ", arr)}]");
        }

        //將一個數組(二叉樹)調整成一個大頂堆
        /// <summary>
        /// 將 以 n 對應的非葉節點的樹，調整成大頂堆 ex: [4, 6, 8, 5, 9]  n=1 (6這個節點) => [4,9,8,5,6]
        /// </summary>
        /// <param name="arr">待調整的數組</param>
        /// <param name="i">非葉節點在樹組中的索引</param>
        /// <param name="length">對多少元素進行調整(每次處理的數量都在減少)</param>
        public static void adjustHeap(int[] arr, int n, int length)
        {
            //取出當前元數的值，存在個臨時變量
            int temp = arr[n];

            //往arr[n] 的左子節點
            for (int i = n * 2 + 1; i < length; i = i * 2 + 1)
            {
                //(從左到右,從下到上)
                /*
                    先讓左右子節點比較，選找出較大的，再去和父節點比較
                */

                //說明左子節點<右子節點
                if (i + 1 < length && arr[i] < arr[i + 1]) //如果 i + 1 > length 就沒有必要去比較了(i+1代表右子節點)
                {
                    i = i + 1; // i指向右子節點的索引，為了等等要找到右子節點，來跟父節點比較
                }

                //如果子節點>父節點
                if (arr[i] > temp)
                {
                    //將較大的值賦值給當前節點
                    arr[n] = arr[i];
                    //目前父節點已經是最大值，但是原本父節點的值，要放到原本最大值節點的位置，所以要保存原本最大值的索引
                    //指向i 繼續循環比較
                    n = i;
                }
                else
                {
                    break;//右節點剛剛有比較過了，所以可以直接break
                }
            }

            //for 結束後，已經將n 為父節點的樹的最大值，放在了最頂上
            arr[n] = temp; //將 temp 放到調整後的位置
        }
    }
}
