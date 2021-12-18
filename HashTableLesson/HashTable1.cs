using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.HashTableLesson
{
    class HashTable1
    {
        public static void Run()
        {
            //創建哈希表
            HashTableDemo hashTableDemo=new HashTableDemo(7);

            while (true)
            {
                Console.WriteLine("請輸入指令");
                string input = Console.ReadLine();
                bool exit = false;
                switch (input)
                {
                    case "add":
                        Console.WriteLine("輸入id");
                        string id = Console.ReadLine();
                        Console.WriteLine("輸入name");
                        string name = Console.ReadLine();
                        Emp emp = new Emp(Convert.ToInt32(id), name);
                        hashTableDemo.add(emp);
                        break;
                    case "list":
                        hashTableDemo.Display();
                        break;
                    case "find":
                        Console.WriteLine("輸入查找的id");
                        id = Console.ReadLine();
                        hashTableDemo.findEmpById(Convert.ToInt32(id));
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        break;

                }
                if (exit)
                {
                    break;
                }
            }
           
        }
        /*
         
            哈希表(散列)
        
            根據(關鍵碼值)key value 進行直接訪問的數據結構，
            通過關鍵碼值映射到表中的位置進行訪問，增加查找速度
            1. 這個映射的函數 叫 映射函數(決定對像要分配到數組的哪個下標)
            2. 存放紀錄的數組 叫 散列表

            (要自己寫緩存時可用)
            哈希表大多是
            1. 數組+鍊表
            2. 數組+二叉樹
        */

        /*
            以哈希表管理雇員訊息
            (數組+鍊表)
         
        */

        public class HashTableDemo
        {
            //數組管理多個鍊表
            private EmpLinkedList[] empLinkedLists;
            private int size;
            public HashTableDemo(int size)
            {
                this.size = size;
                empLinkedLists = new EmpLinkedList[size];
                //初始化每個鍊表
                for (int i = 0; i < size; i++)
                {
                    empLinkedLists[i] = new EmpLinkedList();
                }
            }

            //添加雇員
            public void add(Emp emp)
            {
                int empListNO = hashFunc(emp.id);
                //將emp 添加到對應的鍊表中
                empLinkedLists[empListNO].add(emp);
            }

            //遍歷所有的鍊表
            public void Display()
            {
                for (int i = 0; i < size; i++)
                {
                    empLinkedLists[i].Display(i);
                }
            }

            //根據id 查找雇員
            public void findEmpById(int id)
            {
                //使用散列
                int empListNO = hashFunc(id);
                Emp emp = empLinkedLists[empListNO].findEmpById(id);
                //找到
                if (emp != null)
                {
                    Console.WriteLine($"在第{empListNO} 鍊表找到，雇員id: {emp.id} name: {emp.name}");
                }
                else
                {
                    Console.WriteLine("查無資料");
                }
            }


            //邊寫一個散列函數(本次用取模法)
            public int hashFunc(int id)
            {
                return id % size;
            }
        }
        //雇員
        public class Emp
        {
            public int id;
            public string name;
            public Emp next;
            public Emp(int id,string name)
            {
                this.id = id;
                this.name = name;
            }
        }

        public class EmpLinkedList
        {
            //頭指針
            private Emp head;

            //加雇員到鍊表
            //假定id 遞增
            public void add(Emp emp)
            {
                //第一個雇員
                if (head == null)
                {
                    head = emp;
                    return;
                }

                Emp curEmp = head;
                while (true)
                {
                    if (curEmp.next == null)
                    {
                        break;

                    }
                    curEmp = curEmp.next;
                }

                curEmp.next = emp;
            }

            public Emp findEmpById(int ID)
            {
                if (head == null)
                {
                    Console.WriteLine("list is empty");
                    return null;
                }

                Emp curEmp = head;

                while (true)
                {
                    //找到
                    if (curEmp.id == ID)
                    {
                        break;
                    }
                    

                    //到底，退出
                    if (curEmp.next == null)
                    {
                        curEmp = null;
                        break;
                    }

                    curEmp = curEmp.next;

                }

                return curEmp;
            }

            //遍歷鍊表的僱員訊息
            public void Display(int no)
            {
                if (head == null)
                {
                    Console.WriteLine($"no.{no} list is empty");
                    return;
                }

                Emp curEmp = head;
                while (true)
                {
                    Console.WriteLine($"id=>{curEmp.id} name =>{curEmp.name}");
                    if (curEmp.next == null)
                    {
                        break;
                    }
                    curEmp = curEmp.next;
                }
            }
        }
    }
}
