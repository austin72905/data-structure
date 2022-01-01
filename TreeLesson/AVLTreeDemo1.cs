using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.TreeLesson
{
    class AVLTreeDemo1
    {
        /*
            平衡二叉樹(AVL)
        
            解決BST有時查詢很慢的問題
            ex: 數列 1,2,3,4,5,6 ，要求創建一個二叉排序樹(BST)

            1. 左子樹全為空，從形式上看起來更像鍊表
            2. 插入速度沒有影響
            3. 查詢速度明顯降低(每次都要依次比較)，不能發揮BST優勢，因為每次還需要比較左子樹，其查詢速度比單鍊表還慢

            =>解決: 使用平衡二叉樹(AVL)



            定義
            1. 平衡二叉樹，又稱平衡二叉搜索樹(AVL)，是在BST上做的優化，可以保證查詢較率較高
            2. 具有以下特點
                (1) 是一顆空樹 or  左右子樹的高度差的絕對值不超過1
                (2) 左右子樹都是一顆平衡二叉樹
                (3) 常見實現方法有
                        a. 紅黑樹
                        b. AVL算法
                        c. 替罪羊樹
                        d. Treap
                        e. 伸展樹
         


           構建平衡二叉樹
                求樹的高度(當左、右子樹高度差絕對值>1，才需要旋轉調整高度)
                
                旋轉
                1. 單旋轉
                     (1) 左旋轉 => 降低右子樹的高度
                            處理流程
                                先搞成一個BST 二叉排序樹
                                1. 創建一個新節點newNode，值 = 當前根節點的值


                                2. 把新節點的左子樹設置為當前節點的左子樹
                                    newNode.left=left

                                3. 把新節點的右子樹設置為當前節點的右子樹的左子樹
                                    newNode.right=right.left

                                4. 把當前節點的值換為右子節點的值
                                    value=right.value

                                5. 把當前節點的右子樹設置為右子樹的右子樹
                                    right=right.right

                                6. 把當前節點的左子樹設置為新節點
                                    left=newNode

                     (2) 右旋轉 => 降低左子樹的高度
                            處理流程
                                先搞成一個BST 二叉排序樹
                                1. 創建一個新節點newNode，值 = 當前根節點的值


                                2. 把新節點的右子樹設置為當前節點的右子樹
                                    newNode.right=right

                                3. 把新節點的左子樹設置為當前節點的左子樹的右子樹
                                    newNode.left=left.right

                                4. 把當前節點的值換為左子節點的值
                                    value=left.value

                                5. 把當前節點的左子樹設置為左子樹的左子樹
                                    left=left.left

                                6. 把當前節點的右子樹設置為新節點
                                    right=newNode

                2. 雙旋轉

                        某些情況下，單旋轉無法達成AVL樹的轉換
                        ex: [10,11,7,6,8,9]

                        問題分析
                        1. 當符合右旋轉的條件時
                        2. 如果當前節點的左子樹的右子樹，它的高度 > 當前節點左子樹的左子樹的高度
                        3. 先對當前節點的左節點進行左旋轉
                        4. 在對當前節點進行右旋轉

            AVL是過度強調平衡，所以調整旋轉過於頻繁；紅黑樹是在其上加了容忍度，減少旋轉調平衡次數，從而提升性能
        */



        public static void Run()
        {
            //showBST();
            //int[] arr = { 4, 3, 6, 5, 7, 8 }; //左旋轉用的陣列
            //int[] arr2 = { 10, 12, 8, 9, 7, 6 }; //右旋轉用的陣列
            //showRotate(arr);
            //showRotate(arr2);

            int[] arr3 = { 10, 11, 7, 6, 8, 9 }; //無法使用單旋轉的情況
            showRotate(arr3);

        }

        public static void showRotate(int[] arr)
        {
            
            //創建AVL 樹
            AVLTree aVLTree = new AVLTree();
            //添加節點
            for (int i = 0; i < arr.Length; i++)
            {
                aVLTree.addToAVL(new Node(arr[i]));

            }

            //中序遍歷
            Console.WriteLine("中序遍歷AVL樹");
            aVLTree.infixOrder();

            //高度
            Console.WriteLine("還沒做旋轉前");
            Console.WriteLine($"樹的高度={aVLTree.GetRoot().height()}");//4

            //左子樹高度
            Console.WriteLine($"左子樹高度={aVLTree.GetRoot().leftHeight()}");//1

            //右子樹高度
            Console.WriteLine($"右子樹高度={aVLTree.GetRoot().rightHeight()}");//3
        }
        public static void showBST()
        {
            int[] arr = { 4, 3, 6, 5, 7, 8 };
            //創建AVL 樹
            AVLTree aVLTree = new AVLTree();
            //添加節點
            for (int i = 0; i < arr.Length; i++)
            {
                aVLTree.add(new Node(arr[i]));

            }

            //中序遍歷
            Console.WriteLine("中序遍歷AVL樹");
            aVLTree.infixOrder();

            //高度
            Console.WriteLine("還沒做旋轉前");
            Console.WriteLine($"樹的高度={aVLTree.GetRoot().height()}");//4

            //左子樹高度
            Console.WriteLine($"左子樹高度={aVLTree.GetRoot().leftHeight()}");//1

            //右子樹高度
            Console.WriteLine($"右子樹高度={aVLTree.GetRoot().rightHeight()}");//3

        }

       

        //創建AVL 樹   (以BST章節的 為基礎)
        class AVLTree
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

            //使用創建avl樹的方式添加節點
            public void addToAVL(Node node)
            {
                if (root == null)
                {
                    root = node;
                }
                else
                {
                    root.addToAVL(node);
                }
            }

            public Node GetRoot()
            {
                return root;
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
                        int minVal = delRightTreeMin(targetNode.right);
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

        //節點 (以BST章節的 為基礎)
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


            //AVL 章節新增的部分 

            //返回當前節點，以該節點為根節點的樹的的高度 (就是看左節點還是右節點誰高，就取誰的高度)
            public int height()
            {
                //+1 因為當前節點自己本身也算一層
                /*
                    不斷往子節點遞歸，如果沒有子樹，就返回0(可以理解成左/右都沒有子樹，回傳給他的是0)，有子樹就往下繼續遞歸
                    不斷遞歸後，最後先執行的是最下面的節點，最下面的節點如果沒有子樹(0)+自己所在的那一層(1)=>返回1給上面那層
                    
                    上面的節點不斷接收到，下面的節點回傳的高度，然後當時的節點在+1(自己的高度)，繼續往上傳
                    最後傳到根節點時，就統計完整個樹的高度
                    Math.Max(左,右) 反正左右子樹，只要取比較高的那邊，就是整個樹的高度了
                */
                var val = value;
                var result = Math.Max(left == null ? 0 : left.height(), right == null ? 0 : right.height()) + 1;
                return result;

                //最精簡的寫法，上面是為了追蹤運算的過程
                //return Math.Max(left == null ? 0 : left.height(), right == null ? 0 : right.height()) + 1;
            }

            //返回左子樹的高度
            public int leftHeight()
            {
                if (left == null)
                {
                    return 0;
                }
                return left.height();
            }

            //返回右子樹的高度
            public int rightHeight()
            {
                if (right == null)
                {
                    return 0;
                }
                return right.height();
            }

            //左旋轉
            private void leftRotate()
            {
                //創建新的節點
                /*
                    
                    1. 創建一個新節點newNode，值 = 當前根節點的值

                    2. 把新節點的左子樹設置為當前節點的左子樹
                       newNode.left=left

                    3. 把新節點的右子樹設置為當前節點的右子樹的左子樹
                       newNode.right=right.left

                    4. 把當前節點的值換為右子節點的值
                       value=right.value

                    5. 把當前節點的右子樹設置為右子樹的右子樹
                       right=right.right

                    6. 把當前節點的左子樹設置為新節點
                       left=newNode 
                */

                Node newNode = new Node(value);

                newNode.left = left;

                newNode.right = right.left;

                value = right.value;

                right = right.right;

                left = newNode;
            }

            //右旋轉
            private void rightRotate()
            {
                /*
                    1. 創建一個新節點newNode，值 = 當前根節點的值


                    2. 把新節點的右子樹設置為當前節點的右子樹
                       newNode.right=right

                    3. 把新節點的左子樹設置為當前節點的左子樹的右子樹
                       newNode.left=left.right

                    4. 把當前節點的值換為左子節點的值
                       value=left.value

                    5. 把當前節點的左子樹設置為左子樹的左子樹
                       left=left.left

                    6. 把當前節點的右子樹設置為新節點
                       right=newNode
 
                */

                Node newNode = new Node(value);

                newNode.right = right;

                newNode.left = left.right;

                value = left.value;

                left = left.left;

                right = newNode;
            }

            //添加節點
            //遞歸形式添加節點，需要滿足平衡二叉排序樹(AVL)
            public void addToAVL(Node node)
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

                //添加完節點後，右子樹-左子樹的高度 >1，就進行左旋轉

                if (rightHeight() - leftHeight() > 1)
                {
                    //需要考慮需要雙旋轉的情況
                    //如果當前節點的右子樹的左子樹> 當前節點右子樹的右子樹的高度
                    if (right != null && right.leftHeight() > right.rightHeight())
                    {
                        //先對當前節點的右節點，進行右旋轉
                        right.rightRotate();
                        leftRotate();
                    }
                    else
                    {
                        leftRotate();
                    }

                    return; //已經平衡進不要再往下跑了
                   
                }

                //添加完節點後，左子樹-右子樹的高度 >1，
                if (leftHeight() - rightHeight() > 1)
                {
                    //需要考慮需要雙旋轉的情況
                    if(left!=null && left.rightHeight()> left.leftHeight())
                    {
                        //先對當前節點的左節點進行左旋轉
                        left.leftRotate();
                        //再對當前節點進行右旋轉
                        rightRotate();
                    }
                    else
                    {
                        //一般情況值皆右旋轉就好
                        rightRotate();
                    }
                    
                }
            }

        }
    }
}
