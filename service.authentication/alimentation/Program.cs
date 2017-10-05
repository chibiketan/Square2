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
            var collection = db.GetCollection<MongoDB.Bson.BsonArray>("Roles");
            var search = collection.FindSync(new JsonFilterDefinition<BsonArray>(""));

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
