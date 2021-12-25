using System;

namespace CsharpOperation.TreeLesson
{
    class BinarySortTreeDemo1
    {
        /*
            二叉排序樹(Binary Sort(Search) Tree) (BST)
        
            排序解決方案
            數組
                (1)數組未排序
                        優點: 直接在數組尾巴添加，速度快
                        缺點: 查找速度慢
                (2)數組排序
                        優點: 可以使用二分查找，查找速度快
                        缺點: 為了保證數組有序，在添加數據時，找到插入位置後，需要整體移動，速度慢
            
            鍊表
                優點: 添加數據快，不用整體移動
                缺點: 查找很慢

            => 可以選擇二叉排序樹

            為什麼二叉排序樹比較快?

            定義
            對於二叉排序樹(BST)，任何一個非葉子節點， 該節點> 左子節點 &&  該節點 < 右子節點

            P.S. 如果有相同值，可以將該節點放在左子節點或右子節點


            二叉樹的刪除，有多種情況要考慮 (參考二叉排序樹筆記)
            1. 刪除葉節點  (如 2,5,9,12)
            2. 刪除只有一個子樹的節點  (如 1)
            3. 刪除有兩個子樹的節點  (如 7,3,10)
            
            刪除的思路
            1. 刪除葉節點
                (1) 找到該節點 targetNode
                (2) 找到該節點 targetNode 的 父節點 parent
                (3) 判斷 targetNode 是 parent 的 左子節點 還是 右子節點
                (4) 根據前面的情況來對應刪除
                    a. 若為左子節點 : parent.left = null
                    b. 若為右子節點 : parent.right = null


            2. 刪除只有一個子樹的節點
                (1) 找到要刪除的節點 targetNode
                (2) 找到該節點 targetNode 的 父節點 parent
                (3) 確定 targetNode 的子節點 是  左子節點 還是 右子節點
                (4) 判斷 targetNode 是 parent 的 左子節點 還是 右子節點
                (5) 根據前面的情況來對應刪除
                    A. 若 targetNode 有的是 左子節點
                        a. targetNode 為 parent 左子節點 : parent.left = targetNode.left
                        b. targetNode 為 parent 右子節點 : parent.right = targetNode.left

                    B. 若 targetNode 有的是 右子節點
                        a. targetNode 為 parent 左子節點 : parent.left = targetNode.right
                        b. targetNode 為 parent 右子節點 : parent.right = targetNode.right


            
            3. 刪除有兩個子樹的節點
                (1) 找到要刪除的節點 targetNode
                (2) 找到該節點 targetNode 的 父節點 parent
                (3) 從 targetNode 的右子樹找到最小的節點
                (4) 用一個臨時便量temp，將最小節點的值保存
                (5) 刪除 該最小節點
                (6) targetNode.value =temp  (用從右子樹找到最小的值來填補原本的值)

            刪除節點的注意事項
            1. 可能會遇到 要刪除的是根節點，而他只有一個子樹 (如果刪的是根節點，他的parent =null ，沒做判斷會空指針異常)

         
        */

        public static void Run()
        {
            //遍歷lesson
            //int[] arr = { 7, 3, 10, 12, 5, 1, 9 };

            //BinarySortTree binarySortTree = new BinarySortTree();
            ////添加節點到二叉排序樹
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    binarySortTree.add(new Node(arr[i]));
            //}

            ////中序遍歷二叉排序樹 (中序遍歷剛好會是升序)
            //Console.WriteLine("中序遍歷二叉排序樹");
            //binarySortTree.infixOrder();


            //刪除lesson
            int[] arr2 = { 7, 3, 10, 12, 5, 1, 9, 2 };

            BinarySortTree binarySortTree2 = new BinarySortTree();
            //添加節點到二叉排序樹
            for (int i = 0; i < arr2.Length; i++)
            {
                binarySortTree2.add(new Node(arr2[i]));
            }

            //中序遍歷二叉排序樹 (中序遍歷剛好會是升序)
            Console.WriteLine("中序遍歷二叉排序樹");
            binarySortTree2.infixOrder();

            //測試1. 刪除葉子節點
            //binarySortTree2.delNode(2);
            //binarySortTree2.delNode(5);
            //binarySortTree2.delNode(9);
            //binarySortTree2.delNode(12);
            //Console.WriteLine("刪除節點後");
            //binarySortTree2.infixOrder();

            //測試2. 刪除只有一個子樹的節點
            //binarySortTree2.delNode(1);
            //Console.WriteLine("刪除節點後");
            //binarySortTree2.infixOrder();

            //測試3. 刪除有兩個子樹的節點
            binarySortTree2.delNode(7);          
            Console.WriteLine("刪除節點後");
            binarySortTree2.infixOrder();

            //綜合測試
            //binarySortTree2.delNode(7);
            //binarySortTree2.delNode(2);
            //binarySortTree2.delNode(5);
            //binarySortTree2.delNode(9);
            //binarySortTree2.delNode(12);
            //Console.WriteLine("刪除節點後");
            //binarySortTree2.infixOrder();

        }

        //創建樹
        class BinarySortTree
        {
            private Node root;
            public void add(Node node)
            {
                if (root == null)
                {
                    root = node;
                }
                else
                {
                    root.add(node);
                }
            }

            public void infixOrder()
            {
                if (root != null)
                {
                    root.infixOrder();
                }
                else
                {
                    Console.WriteLine("二叉樹為空，無法遍歷");
                }
            }

            //查找要刪除的節點
            public Node search(int value)
            {
                if (root == null)
                {
                    return null;
                }
                else
                {
                    return root.search(value);
                }
            }

            //查找要刪除節點的父節點
            public Node searchParent(int value)
            {
                if (root == null)
                {
                    return null;
                }
                else
                {
                    return root.searchParent(value);
                }
            }

            //刪除節點
            public void delNode(int value)
            {
                if (root == null)
                {
                    return;
                }
                else
                {
                    //(1) 找到該節點 targetNode
                    Node targetNode = search(value);
                    //如果沒找到
                    if (targetNode == null)
                    {
                        return;
                    }

                    //如果找到的targetNode沒有父節點 (當前二叉排序樹只有一個節點，刪除的剛好就是跟節點)
                    if (root.left == null && root.right == null)
                    {
                        root = null;
                        return;
                    }

                    //(2) 找到該節點 targetNode 的 父節點 parent
                    Node parent = searchParent(value);

                    //如果1.刪除的是葉節點
                    if (targetNode.left == null && targetNode.right == null)
                    {
                        //(3) 判斷 targetNode 是 parent 的 左子節點 還是 右子節點
                        if (parent.left != null && parent.left.value == value) //a. 若為左子節點 : parent.left = null
                        {
                            parent.left = null;

                        }
                        else if (parent.right != null && parent.right.value == value)// b. 若為右子節點 : parent.right = null
                        {
                            parent.right = null;
                        }
                    }
                    else if (targetNode.left != null && targetNode.right != null) //刪除的節點有左右子樹
                    {
                        // 從 targetNode 的右子樹找到最小的節點
                        int minVal=delRightTreeMin(targetNode.right);
                        targetNode.value = minVal;
                    }
                    else
                    {
                        /*
                            這裡個else 是
                            把  1. 刪除葉節點
                                3. 刪除有兩個子樹的節點

                                這兩種情況排除
                                剩下的就是 
                                刪除只有一個子樹的節點 的情況
                        */

                        // A. 若 targetNode 有的是 左子節點
                        if (targetNode.left != null)
                        {
                            //可能會遇到 要刪除的是根節點，而他只有一個子樹 (如果刪的是根節點，他的parent =null ，沒做判斷會空指針異常)
                            //ex: 10(root)->1(left) 
                            if (parent != null)
                            {
                                //a. targetNode 為 parent 左子節點 : parent.left = targetNode.left
                                if (parent.left.value == value)
                                {
                                    parent.left = targetNode.left;
                                }
                                else//b. targetNode 為 parent 右子節點 : parent.right = targetNode.left
                                {
                                    parent.right = targetNode.left;
                                }
                            }
                            else
                            {
                                root = targetNode.left; //遇到 要刪除的是根節點，而他只有一個左子樹，就讓他直接指向左子樹就能刪除根節點了
                            }
                        }
                        else// B. 若 targetNode 有的是 右子節點
                        {
                            //可能會遇到 要刪除的是根節點，而他只有一個子樹 (如果刪的是根節點，他的parent =null ，沒做判斷會空指針異常)
                            if (parent != null)
                            {
                                //a. targetNode 為 parent 左子節點 : parent.left = targetNode.right

                                if (parent.left.value == value)
                                {
                                    parent.left = targetNode.right;
                                }
                                else //b.targetNode 為 parent 右子節點 : parent.right = targetNode.right
                                {
                                    parent.right = targetNode.right;
                                }
                            }
                            else
                            {

                                root = targetNode.right; //遇到 要刪除的是根節點，而他只有一個右子樹，就讓他直接指向右子樹就能刪除根節點了
                            }
                               
                        }
                    }
                }
            }


            /// <summary>
            /// 1.返回最小值
            /// 2. 刪除最小值的該節點
            /// </summary>
            /// <param name="node">二叉排序樹的根節點</param>
            /// <returns>以node 為 根節點 的二叉排序樹的最小值</returns>
            public int delRightTreeMin(Node node)
            {
                Node target = node;

                //循環查找左節點，直到找到最小值
                while (target.left != null)
                {
                    target = target.left;
                }
                //這時target 指向最小節點
                delNode(target.value);
                return target.value;
            }
        }


        //節點
        class Node
        {
            public int value;

            public Node left;
            public Node right;

            //構造器
            public Node(int val)
            {
                value = val;
            }


            public override string ToString()
            {
                return $"Node [value={value}]";
            }

            //添加節點
            //遞歸形式添加節點，需要滿足二叉排序樹
            public void add(Node node)
            {
                if (node == null)
                {
                    return;
                }

                //判斷傳入節點的值，和當前子樹的根節點的大小關係
                if (node.value < this.value)
                {
                    //當前左子節點為空
                    if (this.left == null)
                    {
                        this.left = node;
                    }
                    else
                    {
                        //不為空就遞歸調用
                        this.left.add(node);
                    }


                }
                else//添加的值>當前節點的值
                {
                    if (this.right == null)
                    {
                        this.right = node;
                    }
                    else
                    {
                        //不為空就遞歸調用
                        this.right.add(node);
                    }
                }
            }


            //中序遍歷一下
            public void infixOrder()
            {
                if (this.left != null)
                {
                    this.left.infixOrder();
                }

                Console.WriteLine(this);

                if (this.right != null)
                {
                    this.right.infixOrder();
                }
            }


            /// <summary>
            /// 查找要刪除的節點
            /// </summary>
            /// <param name="value">希望刪除的節點的值</param>
            /// <returns>如果找到就返回該節點，否則返回null</returns>
            public Node search(int value)
            {

                if (value == this.value) //找到該節點
                {
                    return this;
                }
                else if (value < this.value) //查找的值 < 當前節點，向左子樹遞歸查找
                {
                    if (this.left == null)
                    {
                        return null;
                    }

                    return this.left.search(value);
                }
                else // 查找的值 >= 當前節點，向右子樹遞歸查找
                {
                    if (this.right == null)
                    {
                        return null;
                    }

                    return this.right.search(value);
                }
            }

            /// <summary>
            /// 查找要刪除節點的父節點
            /// </summary>
            /// <param name="value">希望刪除的節點的值</param>
            /// <returns>如果找到要刪除節點的父節點，否則返回null</returns>
            public Node searchParent(int value)
            {
                //如果當前節點就是要刪除的節點的父節點，就返回
                if ((this.left != null && this.left.value == value) || (this.right != null && this.right.value == value))
                {
                    return this;
                }
                else
                {
                    //如果查找的值 < 當前節點的值 且 當前節點的左子節點 不為空
                    //往左子樹遞歸查找
                    if (value < this.value && this.left != null)
                    {
                        return this.left.searchParent(value);

                    }
                    else if (value >= this.value && this.right != null)
                    {
                        //往右子樹遞歸查找
                        return this.right.searchParent(value);
                    }
                    else
                    {
                        return null; //沒有父節點
                    }
                }
            }

        }
    }
}
