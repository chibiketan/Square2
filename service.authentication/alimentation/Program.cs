using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using MongoDB.Bson;
using MongoDB.Driver;

namespace alimentation
{
    class Program
    {
        private static IConfigurationRoot s_configuration;
        private const string CREATION_USER = "System";

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

        private static async Task<bool> CheckIfCollectionExist(IMongoDatabase database, string collectionName)
        {
            var option = new ListCollectionsOptions()
            {
                Filter = new BsonDocument("name", collectionName)
            };

            var collection = await database.ListCollectionsAsync(option);
            
            return await collection.AnyAsync();
        }

        private static void WriteCreationStatus(CreationStatus status)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            switch (status)
            {
                case CreationStatus.Ok:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("OK");
                    break;
                case CreationStatus.Ko:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("KO");
                    break;
                case CreationStatus.None:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("--");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid value");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.ForegroundColor = oldColor;
        }

        private static void WriteCreationMessage(string message, int maxLength = 80)
        {
            message = message ?? "";
            if (message.Length > maxLength)
            {
                message = message.Substring(0, maxLength);
            }

            Console.Write(message);
            Console.Write(new string(' ', maxLength - message.Length));
        }

        private static async Task CreateRoleCollection(IMongoDatabase db)
        {
            const string collectionName = "Role";
            try
            {
                WriteCreationMessage("Creating \"Role\" collection.");
                // On vérifie si la collection existe déjà
                if (await CheckIfCollectionExist(db, collectionName))
                {
                    WriteCreationStatus(CreationStatus.None);
                    return;
                }

                // on initialise la collection
                await db.CreateCollectionAsync(collectionName);
                var collection = db.GetCollection<Role>(collectionName);
                var roles = new[]
                {
                    new Role
                    {
                        Name = "Admin",
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                    new Role
                    {
                        Name = "Seller",
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                    new Role
                    {
                        Name = "Customer",
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    }
                };
                await collection.InsertManyAsync(roles);
                WriteCreationStatus(CreationStatus.Ok);
            }
            catch (Exception e)
            {
                WriteCreationStatus(CreationStatus.Ko);
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            // set console output to utf8
            Console.OutputEncoding = Encoding.UTF8;
            // Construction de la configuration, on utilise l'emplacement du binaire pour trouver le fichier de config
            s_configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();

            // création du client mongodb
            var db = GetMongoDatabase();

            // création des rôles
            CreateRoleCollection(db).Wait();

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

    enum CreationStatus
    {
        Ok,
        Ko,
        None
    }
}
