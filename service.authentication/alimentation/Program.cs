using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Ketan.Square2.Service.Authentication.Alimentation
{
    class Program
    {
        private static IConfigurationRoot s_configuration;
        private const string COLLECTION_ROLE = "Role";
        private const string COLLECTION_USER = "User";
        private const string ROLE_ADMIN = "Admin";
        private const string ROLE_SELLER = "Seller";
        private const string ROLE_CUSTOMER = "Customer";
        private const string CREATION_USER = "System";

        private static IMongoDatabase GetMongoDatabase(DatabaseConfiguration dbConfig)
        {
            var databaseName = dbConfig.Name;
            var connectionSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress(dbConfig.Host),
                ApplicationName = typeof(Program).FullName
            };

            // Si les identifiants sont fournis, on les utilise
            var user = dbConfig.User;
            var pass = dbConfig.Pass;

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
            try
            {
                WriteCreationMessage("Creating \"Role\" collection.");
                // On vérifie si la collection existe déjà
                if (await CheckIfCollectionExist(db, COLLECTION_ROLE))
                {
                    WriteCreationStatus(CreationStatus.None);
                    return;
                }

                // on initialise la collection
                await db.CreateCollectionAsync(COLLECTION_ROLE);
                var collection = db.GetCollection<Role>(COLLECTION_ROLE);
                var roles = new[]
                {
                    new Role
                    {
                        _id = Guid.NewGuid(),
                        Name = ROLE_ADMIN,
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                    new Role
                    {
                        _id = Guid.NewGuid(),
                        Name = ROLE_SELLER,
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                    new Role
                    {
                        _id = Guid.NewGuid(),
                        Name = ROLE_CUSTOMER,
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

        private static async Task<Dictionary<string, Role>> GetAllRoles(IMongoDatabase db)
        {
            var result = new Dictionary<string, Role>();
            var collection = db.GetCollection<Role>(COLLECTION_ROLE);
            using (var search = collection.Find(r => true).ToCursor())
            {

                while (await search.MoveNextAsync())
                {
                    foreach (var el in search.Current)
                    {
                        result.Add(el.Name, el);
                    }
                }
            } // !using search

            return result;
        }

        private static async Task CreateUserCollection(IMongoDatabase db, Dictionary<string, Role> roleDictionary)
        {
            try
            {
                WriteCreationMessage("Creating \"User\" collection.");
                // On vérifie si la collection existe déjà
                if (await CheckIfCollectionExist(db, COLLECTION_USER))
                {
                    WriteCreationStatus(CreationStatus.None);
                    return;
                }

                // on initialise la collection
                await db.CreateCollectionAsync(COLLECTION_USER);
                var collection = db.GetCollection<User>(COLLECTION_USER);
                var users = new[]
                {
                    new User
                    {
                        _id = Guid.NewGuid(),
                        Login = "Administrator",
                        Password = CryptPassword("Administrator"), // TODO generate + hash password
                        FirstName = "Nicolas",
                        LastName = "Administrator",
                        Role = roleDictionary[ROLE_ADMIN],
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                    new User
                    {
                        _id = Guid.NewGuid(),
                        Login = "Seller",
                        Password = CryptPassword("Seller"), // TODO generate + hash password
                        FirstName = "Lucas",
                        LastName = "Seller",
                        Role = roleDictionary[ROLE_SELLER],
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                    new User
                    {
                        _id = Guid.NewGuid(),
                        Login = "Customer",
                        Password = CryptPassword("Customer"), // TODO generate + hash password
                        FirstName = "Chris",
                        LastName = "Customer",
                        Role = roleDictionary[ROLE_CUSTOMER],
                        CreationDate = DateTime.Now,
                        CreationUser = CREATION_USER
                    },
                };
                await collection.InsertManyAsync(users);

                // create index
                await collection.Indexes.CreateOneAsync(Builders<User>.IndexKeys.Ascending(u => u.Login), new CreateIndexOptions{Name = "User_Login", Unique = true});

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

        private static void Main(string[] args)
        {
            // set console output to utf8
            Console.OutputEncoding = Encoding.UTF8;
            // Construction de la configuration, on utilise l'emplacement du binaire pour trouver le fichier de config
            s_configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();

            var dbConfig = new DatabaseConfiguration();
            s_configuration.GetSection("db").Bind(dbConfig);

            // création du client mongodb
            var db = GetMongoDatabase(dbConfig);

            // création des rôles
            CreateRoleCollection(db).Wait();
            // récupération des rôles
            var roleDictionary = GetAllRoles(db).Result;

            // création des utilisateurs par défaut
            CreateUserCollection(db, roleDictionary).Wait();


            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static string CryptPassword(string password)
        {
            return password;
        }
    }

    enum CreationStatus
    {
        Ok,
        Ko,
        None
    }
}
