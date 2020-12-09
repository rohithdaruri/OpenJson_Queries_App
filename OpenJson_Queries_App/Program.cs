using System;

namespace OpenJson_Queries_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OpenJson_Queries_App");
            QueryJson query = new QueryJson();
            query.Filter();
            Console.ReadKey();
        }
    }
}
