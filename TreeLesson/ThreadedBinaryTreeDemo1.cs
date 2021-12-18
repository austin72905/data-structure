using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.TreeLesson
{
    class ThreadedBinaryTreeDemo1
    {
        /*
            線索二叉樹
            
            二叉樹 會有 n+1 個空指針 (leaf)  (公式 2n-(n-1) = n+1)
            把節點的空指針，指向該節點指向前一個 or 後一個節點 (這些指針稱為線索)

            見圖 線索二叉樹
            圖中  是中序遍歷 把 有空指針的節點  
                  其 
                  1. 左指針 指向前一個節點
                  2. 右指針指向下一個節點
            
            ->難點於 ， 有些指針的左右指針可能是指向子樹、有些是只向前、後節點 (這邊要在代碼裡判斷)

            可分成 (會因為遍歷方式，影響指向的前or後節點)
            1. 前序線索二叉樹
            2. 中序線索二叉樹
            3. 後序線索二叉樹

            遍歷
            無法再使用遞歸遍歷了，會進入死循環(各節點指向有變化)
            需要新的方式遍歷(可以用一般的線性遍歷，比原本的遞歸效率還要高)
            次序會跟線索化的順序一致 (如用中序線索，排出結果就跟原本的中序遍歷一樣)
        */

        public static void Run()
        {
            HeroNode root = new HeroNode(1,"a");
            HeroNode node2 = new HeroNode(3, "b");
            HeroNode node3 = new HeroNode(6, "c");
            HeroNode node4 = new HeroNode(8, "d");
            HeroNode node5 = new HeroNode(10, "e");
            HeroNode node6 = new HeroNode(14, "f");

            //二叉樹
            root.left = node2;
            root.right = node3;
            node2.left = node4;
            node2.right = node5;
            node3.left = node6;

            //測試線索化
            ThreadedBinaryTree threadedBinaryTree = new ThreadedBinaryTree();
            threadedBinaryTree.root = root;
            threadedBinaryTree.threadedNodes();

            //測試 以節點10測試
            HeroNode leftNode = node5.left;
            Console.WriteLine($" 節點10的前一個節點是 {leftNode}" );
            HeroNode rightNode = node5.right;
            Console.WriteLine($" 節點10的後一個節點是 {rightNode}");

            //遍歷
            Console.WriteLine("使用線性遍歷，線索化二叉樹");
            threadedBinaryTree.threadedList();

        }

        //定義一個線索二叉樹
        class ThreadedBinaryTree
        {
            public HeroNode root { get; set; }

            //指向前一個節點的指針
            private HeroNode pre = null;

            public void threadedNodes()
            {
                threadedNodes(root);
            }
            /// <summary>
            /// 線索化 (中序遍歷)方法
            /// </summary>
            /// <param name="node">當前需要線索化的節點</param>
            public void threadedNodes(HeroNode node)
            {
                if (node == null)
                {
                    return;
                }

                /*
                    因為是中序遍歷
                    1. 先線索化左子樹
                    2. 當前節點
                    3. 右子樹

                    因為要旨向前一個節點
                    所以需要兩個指針
                    一個只向當前node、一個只向前一個node
                */

                //先線索化左子樹
                threadedNodes(node.left);

                //當前節點
                //以節點8為例
                //先處理前一個節點
                if (node.left == null)
                {
                    //左指針指向前一個節點
                    node.left = pre; //以 圖 8 這個節點為例，他的左指針指向前一個節點，當前pre 剛好是null 合理
                    //修改當前節點左指針的類型
                    node.leftType = 1;
                }

                //處理後一個節點 (他會在node 指到節點3時處理，pre 此時指向節點8)
                if (pre!=null && pre.right == null)
                {
                    //讓前一個節點的右指針指向當前節點
                    pre.right = node;
                    pre.rightType= 1;
                }

                //讓前一個節點，隨時保持在當前節點的前一個位置
                pre = node;

                //線索化右子樹
                threadedNodes(node.right);
            }

            //遍歷線索化二叉樹
            public void threadedList()
            {
                //定義一個變量，存放當前遍歷的節點
                HeroNode node = root;

                while (node != null)
                {
                    //會先找到節點8
                    while (node.leftType == 0)
                    {
                        node = node.left;
                    }

                    Console.WriteLine(node);

                    //如果當前節點的右指針，指向的是後一個節點，就一直輸出
                    while (node.rightType == 1)
                    {
                        node = node.right;
                        Console.WriteLine(node);
                    }

                    //替換遍歷的節點
                    node = node.right;
                }
            }

            
            //查找----------
            ////前序遍歷查找
            public HeroNode preOrderSearch(int no)
            {
                if (root != null)
                {
                    return root.preOrderSearch(no);
                }
                return null;
            }
            //中序遍歷查找
            public HeroNode infixOrderSearch(int no)
            {
                if (root != null)
                {
                    return root.infixOrderSearch(no);
                }
                return null;
            }

            //後序遍歷查找
            public HeroNode postOrderSearch(int no)
            {
                if (root != null)
                {
                    return root.postOrderSearch(no);
                }
                return null;
            }


            //刪除-------------------------
            //情境1. 刪除非葉節點時，整個子樹刪除

            public void delNode(int no)
            {
                if (root != null)
                {
                    //樹本身就只有一個root節點，就將二叉樹=null
                    if (root.no == no)
                    {
                        root = null;
                    }
                    else
                    {
                        //開始執行遞歸刪除
                        root.delNode(no);
                    }
                }
                else
                {
                    Console.WriteLine("tree is empty,can not delete");
                }
            }

        }

        //節點
        class HeroNode
        {
            public int no { get; set; }
            public string name { get; set; }
            public HeroNode left { get; set; }
            public HeroNode right { get; set; }

            //新增屬性，判斷指針是指向子樹還是前後節點
            /*
                規定
                1. leftType == 0 代表指向左子樹, 1 指向前一個節點
                2. rightType == 0 代表指向右子樹, 1 指向下一個節點
            */
            public int leftType { get; set; }
            public int rightType { get; set; }
            public HeroNode(int no, string name)
            {
                this.no = no;
                this.name = name;
            }

            public override string ToString()
            {
                return $"HeroNode [NO :{no} , name: {name}]";
            }

           
            //查找------------------------------

            //前序遍歷查找
            public HeroNode preOrderSearch(int no)
            {
                Console.WriteLine("進行前序遍歷查找~~");//要在比較的前面寫，打印幾次代表找指定節點時需要幾次，不同遍歷方式次數不同
                if (this.no == no)
                {
                    return this;
                }

                HeroNode resultNode = null;
                if (this.left != null)
                {
                    resultNode = this.left.preOrderSearch(no);
                }

                // 說明我們在左子樹找到了
                if (resultNode != null)
                {
                    return resultNode;
                }

                //如果左子樹沒找到，就向右遞歸
                if (this.right != null)
                {
                    resultNode = this.right.preOrderSearch(no);
                }

                return resultNode;

            }

            //中序遍歷查找
            public HeroNode infixOrderSearch(int no)
            {


                HeroNode resultNode = null;
                if (this.left != null)
                {
                    resultNode = this.left.infixOrderSearch(no);
                }

                if (resultNode != null)
                {
                    return resultNode;
                }
                Console.WriteLine("進行中序遍歷查找~~");//要在比較的前面寫，打印幾次代表找指定節點時需要幾次，不同遍歷方式次數不同
                if (this.no == no)
                {
                    return this;
                }

                if (this.right != null)
                {
                    resultNode = this.right.infixOrderSearch(no);
                }

                return resultNode;
            }

            //後序遍歷查找
            public HeroNode postOrderSearch(int no)
            {

                HeroNode resultNode = null;
                if (this.left != null)
                {
                    resultNode = this.left.postOrderSearch(no);
                }

                if (resultNode != null)
                {
                    return resultNode;
                }

                if (this.right != null)
                {
                    resultNode = this.right.postOrderSearch(no);
                }

                if (resultNode != null)
                {
                    return resultNode;
                }
                Console.WriteLine("進行後序遍歷查找~~");//要在比較的前面寫，打印幾次代表找指定節點時需要幾次，不同遍歷方式次數不同
                if (this.no == no)
                {
                    return this;
                }

                return resultNode;
            }

            //刪除-------------------------------


            //情境1. 刪除非葉節點時，整個子樹刪除
            /*
                % 樹本身就只有一個root節點，就將二叉樹=null
                
                1. 單向，只能判斷當前節點的子節點是否需要刪除
                    (無法判斷當前節點是否是需要刪除的節點，單向，無法找到父節點) 
                
                2. 如果當前節點的左子樹不為空，並且左子節點就是需要刪除的節點，就將this.left =null，並返回(結束遞歸)
                3. 如果當前節點的右子樹不為空，並且右子節點就是需要刪除的節點，就將this.right =null，並返回(結束遞歸)
                4. 步驟2、3都沒有刪除節點，就向左子樹進行遞歸刪除(不用返回，因為可能遞歸刪除沒有成功，要讓它繼續往右子樹遞歸)
                5. 步驟4都沒有刪除節點，就向右子樹進行遞歸刪除
                
            */

            public void delNode(int no)
            {
                //2. 如果當前節點的左子樹不為空，並且左子節點就是需要刪除的節點，就將this.left =null，並返回(結束遞歸)
                if (this.left != null && this.left.no == no)
                {
                    this.left = null;
                    return;
                }
                //3. 如果當前節點的右子樹不為空，並且右子節點就是需要刪除的節點，就將this.right =null，並返回(結束遞歸)
                if (this.right != null && this.right.no == no)
                {
                    this.right = null;
                    return;
                }
                //4. 步驟2、3都沒有刪除節點，就向左子樹進行遞歸刪除
                if (this.left != null)
                {
                    this.left.delNode(no);
                }
                //5. 步驟4都沒有刪除節點，就向右子樹進行遞歸刪除
                if (this.right != null)
                {
                    this.right.delNode(no);
                }
            }

            //情境2. 刪除非葉節點時，不希望整個子樹刪除，而是按造以下規則(課後練習)
            /*
                1. 如果該非葉節點A只有一個子節點B，則用該子節點B代替節點A
                2. 如果該非葉節點A有左子節點B和右子節點C，則讓"左"子節點B替代節點A
                3. 後面講二叉排序樹時，會補充，但這個情境沒有強制要求要排序，只是讓左子節點代替
            */

        }
    }
}
