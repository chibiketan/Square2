using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ketan.Square2.Service.Authentication.Data
{
    public abstract class BaseMongoRepository<T>
    {
        private DatabaseConfiguration m_config { get; }
        private MongoClient m_client { get; }
        private IMongoDatabase m_database { get; }
        protected string m_collectionName { get; }
        protected IMongoCollection<T> m_collection { get; }

        public BaseMongoRepository(DatabaseConfiguration config, string collectionName)
        {
            m_config = config;
            m_collectionName = collectionName;
            m_client = CreateClient();
            m_database = m_client.GetDatabase(m_config.Name);
            m_collection = GetCollection();
        }



        private MongoClient CreateClient()
        {
            var databaseName = m_config.Name;
            var connectionSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress(m_config.Host),
                ApplicationName = GetType().FullName
            };

            // Si les identifiants sont fournis, on les utilise
            var user = m_config.User;
            var pass = m_config.Pass;

            if (!string.IsNullOrEmpty(user))
            {
                connectionSettings.Credentials = new[] { MongoCredential.CreateCredential(databaseName, user, pass) };
            }

            // Création de la connexion
            return new MongoClient(connectionSettings);
        }

        private IMongoCollection<T> GetCollection()
        {
            var option = new ListCollectionsOptions()
            {
                Filter = new BsonDocument("name", m_collectionName)
            };

            bool isCollectionCreated = false;
            var collectionResult = m_database.ListCollections(option);

            if (!collectionResult.Any())
            {
                // La collection n'existe pas
                m_database.CreateCollection(m_collectionName);
                isCollectionCreated = true;
            }

            var collection = m_database.GetCollection<T>(m_collectionName);

            if (isCollectionCreated)
            {
                OnCollectionCreated(collection);
            }

            return collection;
        }

        protected abstract void OnCollectionCreated(IMongoCollection<T> collection);
    }
}