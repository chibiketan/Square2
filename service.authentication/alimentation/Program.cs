using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace alimentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionSettings = new MongoDB.Driver.MongoClientSettings();

            connectionSettings.Server = new MongoServerAddress("localhost");
            var connection = new MongoDB.Driver.MongoClient(connectionSettings);
            var db = connection.GetDatabase("test");
            var collection = db.GetCollection<MongoDB.Bson.BsonDocument>("Roles");
            var search = collection.Find(new BsonDocument()).ToCursorAsync().Result;

            search.MoveNext();
            foreach (var el in search.Current)
            {
                Console.WriteLine(el.ToString());
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
