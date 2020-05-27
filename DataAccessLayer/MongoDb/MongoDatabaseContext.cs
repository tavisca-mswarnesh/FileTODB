using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.MongoDb
{
    public class MongoDatabaseContext: IDatabaseFactory
    {
        IMongoDatabase database;
        public   MongoDatabaseContext()
        {
            //var connectionString = "mongodb+srv://mattapalliswarnesh:mattapalliswarnesh@cluster0-hx7yn.mongodb.net/test?retryWrites=true&w=majority";
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            database = client.GetDatabase("CustomDB");
            

        }
        public void WriteToDatabase(string fileLoction)
        {
            string fileLocation = "C:/Users/vmattapalli/Desktop/PointOfInterestCoordinatesList.txt";
            string[] lines = File.ReadAllLines(fileLocation);
            var collectionName = fileLocation.Split('/');
            var collection = database.GetCollection<BsonDocument>(collectionName[collectionName.Length - 1].Split('.')[0]);
            
            var keyNames = lines[0].Split('|');
            var documentList = new List<BsonDocument>();

            for (int i = 1; i < lines.Length; i++)
            {
                var row = lines[i].Split("|");
                BsonDocument document = new BsonDocument();
                for (int j = 0; j < row.Length; j++)
                {
                    document.Add(new BsonElement(keyNames[j], row[j]));
                }
                documentList.Add(document);

            }
            collection.InsertMany(documentList);

        }
    }
}