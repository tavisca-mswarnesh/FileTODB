using DataAccessLayer;
using DataAccessLayer.SQLDb;
using System;
using System.Diagnostics;

namespace FileToDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("Enter File location:");
            var fileLoction=Console.ReadLine();
            IDatabaseFactory databaseStructure;
            Console.WriteLine("Select a database(enter the number):\n1.MYSQL\n2.MONGODB\nEnter:");
            var option = Console.Read();
            databaseStructure = new SQLDatabaseContext();
            databaseStructure.WriteToDatabase(fileLoction);
            long ticks = sw.ElapsedTicks;
            Console.WriteLine(ticks);
            Console.Read();

        }
    }
}
