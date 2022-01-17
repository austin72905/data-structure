using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpOperation.Algorithm.GreedLesson
{
    class GreedLessonDemo1
    {
        /*
            貪心算法
            

            介紹
            1. 對問題求解時，在每一步選擇都採取最好或最優的選擇  (找到一個覆蓋最多未覆蓋地區的電台)
            2. 所得到的結果不一定是最優的結果(如下題考慮成本)，但是相對接近最優解的結果

            集合覆蓋問題

            
            廣播台      覆蓋地區
            K1          北京、上海、天津
            K2          北京、廣州、深圳
            K3          成都、上海、杭州
            K4          上海、天津
            K5          杭州、大連

            Q : 如何選擇最少的廣播台，讓所有地區接收到訊號?
            
            1. 窮舉法
                列出每個可能廣播台的組合，也被稱為冪集，假設有n 個廣播台，則組合共有 2^n-1 個(就是個規律，有點像乘階，如n=3 ，C3選2+C3選1+C3選3)
                (n過大時，效率會很差 ，ex: n=1000 ，2^1000 (-1常數可忽略))

            2. 貪心法
                目前沒有算可以快速計算得到準確地值，使用貪婪算法，可以得到非常接近的解，而且效率高
                
                以廣播台覆蓋題為例
                (1) 遍歷所有廣播台，找到一個覆蓋最多未覆蓋地區的電台(此電台可能包含一些已覆蓋的地區，但沒有關係)
                (2) 將這個電台加入一個集合(如array)，想辦法把該電台覆蓋的地區在下次比較時去掉
                (3) 重複第一步直到覆蓋全部的地區

                
                具體步驟
                (1) 將所有電台覆蓋的地區，先放到allAreas []，重複的去掉
                (2) 找到一個覆蓋最多未覆蓋地區的電台，放到selects []

            細節
            1. 貪心算法得到的結果不一定是最優解，但都是相似最憂解的結果
            2. 如 k1,k2,k3,k5 可以覆蓋全部地區，但k2,k3,k4,k5 也可以，如果k4使用成本 < k1，那k1,k2,k3,k5雖然滿足條件，但不是最優解
            
        */

        public static void Run()
        {
            //創建廣播台集合
            var broadcasts = new Dictionary<string, HashSet<string>>()
            {
                {"k1",new HashSet<string>{"北京","上海","天津" } },
                {"k2",new HashSet<string>{"廣州", "北京", "深圳" } },
                {"k3",new HashSet<string>{"成都","上海","杭州" } },
                {"k4",new HashSet<string>{"上海","天津" } },
                {"k5",new HashSet<string>{"杭州","大連" } },
            };


            var allAreas = new HashSet<string>();
            //hashset 合併
            foreach (var item in broadcasts)
            {
                allAreas.UnionWith(item.Value);
            }


            //創建selects 集合
            var selects = new List<string>();

            //遍歷過程中，存放過程中電台覆蓋的地區，和當前未覆蓋地區(allAreas)的交集
            var tempSet = new HashSet<string>();

            //定義maxKey，保存在一次遍歷過程中，能覆蓋最大未覆蓋地區的廣播台的key
            //如果maxKey 不為null，則會加入倒selects
            string maxKey = null;

            //如果allAreas 不為空，就代表還有地區沒被覆蓋
            while (allAreas.Count != 0)
            {
                //maxKey = null; //課程寫的位置，但我覺得可以放其他地方

                //取出對應的key
                foreach (var key in broadcasts.Keys)
                {
                    //遍歷完一次就清空
                    //tempSet.Clear(); //課程寫的位置，但我覺得可以放其他地方

                    //覆蓋的地區
                    var areas = broadcasts[key];
                    tempSet.UnionWith(areas);
                    //tempSet 與 allAreas 集合取交集，交集的部分賦給tempSet
                    tempSet.IntersectWith(allAreas);
                    //找到一個覆蓋最多未覆蓋地區的電台，找到的就讓maxKey指向他
                    //每次都選最好的，所以叫貪心算法
                    if (tempSet.Count > 0 && (maxKey == null || tempSet.Count > broadcasts[maxKey].Count))
                    {
                        maxKey = key;
                    }

                    //遍歷完一次就清空
                    tempSet.Clear();
                }

                //maxKey!=null的情況
                if (maxKey != null)
                {
                    selects.Add(maxKey);
                    //將已覆蓋的地區，從allAreas去掉(差集)
                    allAreas.ExceptWith(broadcasts[maxKey]);
                }


                maxKey = null;
            }

            Console.WriteLine($"得到的結果是[ {string.Join(",", selects.Select(o => o))} ]"); //k1,k2,k3,k5
        }
    }
}
