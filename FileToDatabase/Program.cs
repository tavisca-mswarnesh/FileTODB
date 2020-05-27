using DataAccessLayer;
using DataAccessLayer.MongoDb;
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
            IDatabaseFactory databaseStructure=null;
            Console.WriteLine("Select a database(enter the number):\n1.MYSQL\n2.MONGODB\nEnter:");
            var option = Console.Read();
            switch (option)
            {
                case 1:
                    databaseStructure = new SQLDatabaseContext();
                    break;
                case 2:
                    databaseStructure = new MongoDatabaseContext();
                    break;
                default :
                    Console.WriteLine("Invalid input");
                    return;
                
            }
            databaseStructure.WriteToDatabase(fileLoction);
            long ticks = sw.ElapsedTicks;
            Console.WriteLine(ticks);
            Console.Read();

        }
    }
}
