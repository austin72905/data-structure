using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.TreeLesson
{
    class BinaryTreeDemo1
    {

        public static void Run()
        {
            //創建一個二叉樹
            BinaryTree binaryTree = new BinaryTree();
            //創建需要的節點
            HeroNode hero = new HeroNode(1, "鋼鐵人");
            HeroNode hero2 = new HeroNode(2, "美國隊長");
            HeroNode hero3 = new HeroNode(3, "黑寡婦");
            HeroNode hero4 = new HeroNode(4, "蜘蛛人");
            HeroNode hero5 = new HeroNode(5, "浩克");

            //一般都是遞歸創建，這邊要測試 所以先用手動創建
            hero.left = hero2;
            hero.right = hero3;
            hero3.right = hero4;
            hero3.left = hero5;

            binaryTree.root = hero;
            //測試遍歷-----------------------------------------
            //Console.WriteLine("前序遍歷");
            //binaryTree.preOrder();
            //Console.WriteLine("中序遍歷");
            //binaryTree.infixOrder();
            //Console.WriteLine("後序遍歷");         
            //binaryTree.postOrder();


            //查找-----------------------------------------
            //Console.WriteLine("前序遍歷查找");
            //var resultnode=binaryTree.preOrderSearch(5);
            //if (resultnode != null)
            //{
            //    Console.WriteLine($"找到了，訊息 no = {resultnode.no} name = {resultnode.name} ");
            //}
            //else
            //{
            //    Console.WriteLine("沒有找到編號的英雄");
            //}

            //Console.WriteLine("中序遍歷查找");
            //var resultnode2 = binaryTree.infixOrderSearch(5);
            //if (resultnode2 != null)
            //{
            //    Console.WriteLine($"找到了，訊息 no = {resultnode2.no} name = {resultnode2.name} ");
            //}
            //else
            //{
            //    Console.WriteLine("沒有找到編號的英雄");
            //}

            //Console.WriteLine("後序遍歷查找");
            //var resultnode3 = binaryTree.postOrderSearch(5);
            //if (resultnode3 != null)
            //{
            //    Console.WriteLine($"找到了，訊息 no = {resultnode3.no} name = {resultnode3.name} ");
            //}
            //else
            //{
            //    Console.WriteLine("沒有找到編號的英雄");
            //}

            //刪除-----------------------------------------
            Console.WriteLine("刪除前，前序遍歷");
            binaryTree.preOrder();
            //binaryTree.delNode(5);
            binaryTree.delNode(3);
            Console.WriteLine("刪除後，前序遍歷");
            binaryTree.preOrder();
            
        }

        /*
            前序、中序、後序遍歷 
            
            遍歷
            
            (看父節點輸出的順序)
            前序
            父->左->右

            中序            
            左->父->右

            後序
            左->右->父
        */

        /*
            查找
            1. 分別用 前中後 查找 no.5 的節點
            2. 比較各個方式找的次數
        */

        //定義一個二叉樹
        class BinaryTree
        {
            public HeroNode root { get; set; }

            //前序遍歷
            public void preOrder()
            {
                if (this.root != null)
                {
                    this.root.preOrder();
                }
                else
                {
                    Console.WriteLine("binary tree is empty");
                }
            }

            //中序遍歷
            public void infixOrder()
            {
                if (this.root != null)
                {
                    this.root.infixOrder();
                }
                else
                {
                    Console.WriteLine("binary tree is empty");
                }
            }

            //後序遍歷
            public void postOrder()
            {
                if (this.root != null)
                {
                    this.root.postOrder();
                }
                else
                {
                    Console.WriteLine("binary tree is empty");
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
            public HeroNode(int no,string name)
            {
                this.no = no;
                this.name = name;
            }

            public override string ToString()
            {
                return $"HeroNode [NO :{no} , name: {name}]";
            }

            //前序遍歷
            public void preOrder()
            {
                //先輸出父節點
                Console.WriteLine(this);
                //遞歸左子樹
                if (this.left != null)
                {
                    this.left.preOrder();
                }

                //遞歸右子樹
                if (this.right != null)
                {
                    this.right.preOrder();
                }
            }
            //中序
            public void infixOrder()
            {
                //遞歸左子樹
                if (this.left != null)
                {
                    this.left.infixOrder();
                }
                //輸出父節點
                Console.WriteLine(this);
                
                //遞歸右子樹
                if (this.right != null)
                {
                    this.right.infixOrder();
                }
            }
            //後序

            public void postOrder()
            {
                //遞歸左子樹
                if (this.left != null)
                {
                    this.left.postOrder();
                }

                //遞歸右子樹
                if (this.right != null)
                {
                    this.right.postOrder();
                }

               // 輸出父節點
                Console.WriteLine(this);
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
                    resultNode=this.left.preOrderSearch(no);
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
                    resultNode=this.right.infixOrderSearch(no);
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
                if (this.left!=null && this.left.no == no)
                {
                    this.left = null;
                    return;
                }
                //3. 如果當前節點的右子樹不為空，並且右子節點就是需要刪除的節點，就將this.right =null，並返回(結束遞歸)
                if (this.right!=null && this.right.no == no)
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
