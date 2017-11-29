using System;
using System.Collections.Generic;
using System.Text;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Driver;

namespace Ketan.Square2.Service.Authentication.Test.Data
{
    public class BaseMongoRepositoryTest : IDisposable
    {
        private static volatile int s_index;
        protected readonly DatabaseConfiguration m_config;
        public BaseMongoRepositoryTest()
        {
            // Création des ressources avant le test
            m_config = new DatabaseConfiguration
            {
                Host = "localhost",
                Name = $"unit_test_{typeof(BaseMongoRepositoryTest).Name}_{s_index++}"
            };
        }

        public void Dispose()
        {
            // Création de la connexion
            var client = CreateClient();
            client.DropDatabase(m_config.Name);
        }

        protected MongoClient CreateClient()
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
    }
}
