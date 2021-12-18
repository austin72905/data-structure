using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.LinkedListLesson
{
    class LinkedList1
    {
        public static void Run()
        {
            //SingleLinkedlist singleLinkedlist = new SingleLinkedlist();
            //singleLinkedlist.add(new AphbetNode(1,"A"));
            //singleLinkedlist.add(new AphbetNode(2, "B"));
            //singleLinkedlist.add(new AphbetNode(3, "C"));
            //singleLinkedlist.add(new AphbetNode(4, "D"));
            //singleLinkedlist.showNodeList();

            //按編號添加順序
            SingleLinkedlistByOrder singleLinkedlistByOrder = new SingleLinkedlistByOrder();
            singleLinkedlistByOrder.add(new AphbetNode(1, "A"));
            singleLinkedlistByOrder.add(new AphbetNode(1, "A"));
            singleLinkedlistByOrder.add(new AphbetNode(5, "E"));
            singleLinkedlistByOrder.add(new AphbetNode(2, "B"));
            singleLinkedlistByOrder.add(new AphbetNode(3, "C"));
            singleLinkedlistByOrder.showNodeList();
            singleLinkedlistByOrder.update(3, "Case");
            singleLinkedlistByOrder.showNodeList();
            singleLinkedlistByOrder.update(6, "Case");

            singleLinkedlistByOrder.delete(3);
            singleLinkedlistByOrder.showNodeList();
        }
        /*
            鏈結串列
        
            1. 非連續記憶體
            2. 分為有頭節點；沒有頭節點
            3. 有data區、next 區

            單鍊表(帶頭節點)
            1. head : 不存放任何數據，單純指向下一個節點
            2. next : 指向下一個節點
            3. 當next 指向null，代表後面沒有結點了
           
        */

        /*
            需求1
            A~Z 的字母 要插入鍊表
            1. 插在鍊表尾

            需求2
            1. 案排序(no)插入(插入指定位置)，如果no 已存在，提示新增失敗
            2. 可以修改跟刪除
        */

        //創建單戀表
        class SingleLinkedlist
        {
            //head 不存放任何數據
            private AphbetNode head=new AphbetNode(0,"");

            //添加節點到鍊表到尾部
            public void add(AphbetNode nextnode)
            {
                //指針
                AphbetNode temp = head;

                //當下一個節點是null時，代表到鍊表的尾部，將元素新增到尾部
                //沒有到尾部就指向下個節點
                while (true)
                {
                    if(temp.next == null)
                    {
                        break;
                    }

                    temp = temp.next;
                }

                //跳出循環後代表到尾部了，在尾部新增新元素
                temp.next = nextnode;
            }

            public void showNodeList()
            {
                //指針
                AphbetNode temp = head.next; //頭節點沒數據，不用打印

                if (head.next == null)
                {
                    Console.WriteLine("鍊表為空");
                    return;
                }

                while (true)
                {
                    //輸出節點資訊
                    Console.Write(temp.ToString());
                    if (temp.next == null)
                    {
                        break;
                        
                    }
                    
                    temp = temp.next;
                }
            }
        }

        class SingleLinkedlistByOrder
        {
            //head 不存放任何數據
            private AphbetNode head = new AphbetNode(0, "");

            //添加節點到鍊表到尾部
            public void add(AphbetNode nextnode)
            {
                //指針
                AphbetNode temp = head;

                bool noIsExist = false;

                //當下一個節點是null時，代表到鍊表的尾部
                //找到要插入的位置，no來比
                //沒有到尾部就指向下個節點
                while (true)
                {
                    if (temp.next == null)
                    {
                        break;
                    }

                    
                    if(temp.next.no> nextnode.no)
                    {
                        //代表找到插入的位置了
                        break;
                    }else if(temp.next.no == nextnode.no)
                    {
                        Console.WriteLine("編號已存在，無法添加");
                        noIsExist = true;
                        break;
                    }

                    temp = temp.next;
                }

                if (!noIsExist)
                {
                    nextnode.next = temp.next;
                    temp.next = nextnode;
                }

                             
            }

            //修改
            public void update(int no,string updateName)
            {
                //指針
                AphbetNode temp = head.next; //頭節點沒數據，不用比較

                // 輸入的 no 是否存在
                bool noIsExist = false;
                if (head.next == null)
                {
                    Console.WriteLine("鍊表為空");
                    return;
                }

                while (true)
                {
                    if(temp.next == null)
                    {
                        break;
                    }

                    //找到要修改的位置了
                    if (temp.no == no)
                    {
                        noIsExist = true;
                        break;
                    }

                    temp = temp.next;
                }

                if (noIsExist)
                {
                    //修改
                    temp.name = updateName;                   
                    return;
                }

                Console.WriteLine($"輸入的no {no}不存在");



            }

            //刪除
            public void delete(int no)
            {
                //指針
                AphbetNode temp = head;
                // 輸入的 no 是否存在
                bool noIsExist = false;
                if (head.next == null)
                {
                    Console.WriteLine("鍊表為空");
                    return;
                }

                while (true)
                {
                    if (temp.next == null)
                    {
                        break;
                    }

                    //上面next 是null 就會跳出了，這邊不用擔心temp.next 會null  
                    //代表找到了
                    if (temp.next.no == no)
                    {
                        noIsExist = true;
                        break;
                    }

                    temp = temp.next;
                }

                if (noIsExist)
                {
                    //跳過下一個節點，沒有指向他就會被GC
                    //他有沒有指向別人沒差
                    temp.next = temp.next.next;
                    return;
                }

                Console.WriteLine($"查無節點no {no}");
            }

            public void showNodeList()
            {
                //指針
                AphbetNode temp = head.next; //頭節點沒數據，不用打印

                if (head.next == null)
                {
                    Console.WriteLine("鍊表為空");
                    return;
                }

                while (true)
                {
                    //輸出節點資訊
                    Console.Write(temp.ToString());
                    if (temp.next == null)
                    {
                        break;

                    }

                    temp = temp.next;
                }
                Console.WriteLine();
            }
        }
        class AphbetNode
        {
            public int no;
            public string name;

            public AphbetNode(int no,string name)
            {
                this.no = no;
                this.name = name;
            }

            //指向下一個節點
            public AphbetNode next;

            public override string ToString()
            {
                return $"[ no : {no} , name : {name}]==>";
            }
        }
    }
}
