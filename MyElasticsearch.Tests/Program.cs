using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            IndexOperate io = new IndexOperate();
            io.Create();
            Console.Read();
        }
    }
}
