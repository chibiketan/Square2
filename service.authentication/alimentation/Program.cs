using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using MongoDB.Bson;
using MongoDB.Driver;

namespace alimentation
{
    class Program
    {
        private static IConfigurationRoot s_configuration;

        private static IMongoDatabase GetMongoDatabase()
        {
            var databaseName = s_configuration["db:name"];
            var connectionSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress(s_configuration["db:host"]),
                ApplicationName = typeof(Program).FullName
            };

            // Si les identifiants sont fournis, on les utilise
            var user = s_configuration["db:user"];
            var pass = s_configuration["db:pass"];

            if (!string.IsNullOrEmpty(user))
            {
                connectionSettings.Credentials = new []{ MongoCredential.CreateCredential(databaseName, user, pass) };
            }

            // Création de la connexion
            var connection = new MongoClient(connectionSettings);

            return connection.GetDatabase(databaseName);
        }

        static void Main(string[] args)
        {
            // Construction de la configuration, on utilise l'emplacement du binaire pour trouver le fichier de config
            s_configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();

            // création du client mongodb
            var db = GetMongoDatabase();

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
