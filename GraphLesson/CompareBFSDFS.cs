using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpOperation.GraphLesson
{
    class CompareBFSDFS
    {
        /*
            廣度優先與深度優先差異

            訪問節點的順序會不同
            見 圖 廣與深比較

        */

        public static void Run()
        {
            DepthFirstSearch.RunComp();
            Console.WriteLine();
            BroadFirstSearch.RunComp();
        }
    }
}
