using System;
using System.Linq;

namespace CsharpOperation.Algorithm.KMPLesson
{
    class KMPLessonDemo1
    {
        /*
         
            KMP 字串匹配算法
        
            1. 解決模式串在文本串是否出現過，如果出現過，找出最早出現的位置

            2. 常用於在一個文本串S內查找一個模式串P出現的位置
            
            3. 利用之前判斷過訊息，通過一個next數組，保留模式串前後綴的共有元素個數，，每次回溯時，通過next數組找到前面匹配過的位置


            Q: 字符串"BBC ABCDAB ABCDABCDABDE"與搜索詞"ABCDABD"，找出裡面是否包含搜索詞?如果有，第一個出現的位置是?

            懶人包
            1. 先得稻子串的部分匹配表
            2. 使用部分匹配表完成KMP匹配
            
            參考資料
            https://www.ruanyifeng.com/blog/2013/05/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm.html
            
            部分匹配表(PMT) Partial Match Table

            針對搜索詞ABCDABD 產生
            
            搜索詞          A  B  C  D  A  B  D
            部分匹配值      0  0  0  0  1  2  0
            
            如何產生?
            "前綴"和"後綴"的最長的共有元素的長度(不是個數，是長度!!!)

            前綴 : 除了最後一個字符，其他前面字符的所有頭部組合
            後綴 : 除了第一個字符，其他後面字符的所有尾部組合

            搜索詞為     共有元素長度               
            A               0                沒有後綴跟前綴，空集合，共有元素長度0
            AB              0                A為前綴、B為後綴，共有元素長度0
            ABC             0                前綴:[A,AB]                    後綴: [BC,C]，共有元素長度0
            ABCD            0                前綴:[A,AB,ABC]                後綴: [BCD,CD,D]，共有元素長度0
            ABCDA           1                前綴:[A,AB,ABC,ABCD]           後綴: [BCDA,CDA,DA,A]，共有元素為A，長度1
            ABCDAB          2                前綴:[A,AB,ABC,ABCD,ABCDA]     後綴: [BCDAB,CDAB,DAB,AB,B]，共有元素為AB，長度2
            ABCDABD         0
            

            KMP操作方式
                
            1. 先找到字符串的第一個字符，與搜索詞的第一個字符相同
            2. 開始逐步與搜索詞的下一個字符比較是否相同
            3. 直到有個不相同為止
            (這時最直觀的方式是把整個搜索詞往後移一位，再逐個比較，但是效率很差)
            (KMP算法，不把搜索位置移回已經比較過的位置，而是繼續往後移)
            
            4. 公式:   移動位數 = 已匹配的字符數 - 對應的部分匹配值
         
         
         */

        public static void Run()
        {
            string str1 = "BBC ABCDAB ABCDABCDABDE";
            string str2 = "ABCDABD";


            int[] next = KmpNext(str2);

            Console.WriteLine($"next=[{string.Join(",",next.Select(o=>o))}]");

            int index = KmpSearch(str1, str2, next);

            Console.WriteLine($"index={index}");

        }

        //KMP搜索算法
        //如果-1就是沒匹配到，否則返回第一個匹配的位置
        public static int KmpSearch(string str1, string str2, int[] next)
        {
            //遍歷
            for (int i = 0, j = 0; i < str1.Length; i++)
            {
                /*
                    str1[i] != str2[j] 
                    要調整J的大小                 
                */

                while (j > 0 && str1[i] != str2[j])
                {
                    //不斷的往部分匹配表找，直到找到有相符的
                    j = next[j - 1];
                }

                if (str1[i] == str2[j])
                {
                    j++;
                }

                //找到了
                if (j == str2.Length)
                {
                    return i - j + 1;
                }
            }

            return -1;
        }

        //獲取一個字串(搜索詞)的部分匹配值表
        public static int[] KmpNext(string dest)
        {
            //創建next數組保存部分匹配值
            int[] next = new int[dest.Length];
            next[0] = 0; //如果字符串長度為1，部分匹配值=0
            /*
                 後綴: 除了第一個字符，其他後面字符的所有尾部組合，所以i要從1開始
            */
            for (int i = 1, j = 0; i < dest.Length; i++)
            {

                /*
                    當不同(dest[i] != dest[j])時，要從部分匹配表獲取新的j
                    直到有dest[i] == dest[j] 成立時才退出

                     看不太懂next 數組怎麼產生的...
                */

                while (j > 0 && dest[i] != dest[j])
                {
                    j = next[j - 1];
                }

                //ex: 搜索詞 AA
                /*
                    前綴:[A] 後綴:[A]，共有元素為A，長度1
                    滿足時匹配值+1
                */
                if (dest[i] == dest[j])
                {
                    j++;
                }
                next[i] = j;
            }

            return next;
        }


    }
}
