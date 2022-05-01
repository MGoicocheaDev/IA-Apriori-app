using lib_apriori_net.Data;
using System;

namespace cmd_apriori_net
{
    class Program
    {
        static void Main(string[] args)
        {
            loadData.InitializeProcess();
            Console.ReadKey();
        }
    }
}
