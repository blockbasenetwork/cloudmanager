using System.Collections.Generic;

namespace BlockBase.Dapps.CloudManager.Utils
{
    class Program
    {   
        class obj
        {
            public int MyProperty { get; set; }
        }
        static void Main(string[] args)
        {
        


            var tempList = new List<obj> { new obj { MyProperty = 2} };
            tempList.ForEach(it =>  it.MyProperty=3);
            var x = 1;
        }
    }
}
