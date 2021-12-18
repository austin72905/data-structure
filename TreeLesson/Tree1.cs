﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.TreeLesson
{
    class Tree1
    {
        /*
            樹
            
            為什麼需要樹?
            (比較個數據結構存放數據的優缺點)    
            數組
            優點: 通過下標訪問元素，速度快; 有敘數組還可以用二分查找等方式提高查詢速度
            缺點: 檢索具體某個值(元素是對像，要找對像裡的某個值)，或插入值需要整體移動(底層需要創建新數組，拷貝原來數據，並插入新數據)，效率較低
        
         
            鍊表
            優點: 插入節點，鍊接到鍊表鐘即可，刪除效率也很好
            缺點: 查找時，還是需要從頭節點開始遍歷，效率還是很低


            樹
            能夠提高存儲、讀取的效率，比如使用二叉排序樹(Binary Sort Tree)，
            既可以保證樹的的查找速度，也可以保證數據的插入、刪除、修改的速度(因為一開始就用左右節點分，比從頭遍歷快很多)
         
         */

        /*
            二叉樹
            
            定義
            每個節點只能"最多"有兩個子節點(左、右)

            滿二叉樹
            1. 所有葉節點都在最後一層
            2. 且節點總數為 2^n-1    ex:假設有3層， 2*1+2*2+2*3....-1 (第1層根節點只有一個)

            完全二叉樹
            1. 一個滿二叉樹 + 下一層的節點在最左邊且連續 
         
         
        
        */
       
    }
}