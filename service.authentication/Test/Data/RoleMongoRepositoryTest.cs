using System;
using System.Collections.Generic;
using System.Text;
using Ketan.Square2.Service.Authentication.Data;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Driver;
using Xunit;

namespace Ketan.Square2.Service.Authentication.Test.Data
{
    public class RoleMongoRepositoryTest : IDisposable
    {
        private static volatile int s_index;
        private DatabaseConfiguration m_config;

        public RoleMongoRepositoryTest()
        {
            // Création des ressources avant le test
            m_config = new DatabaseConfiguration
            {
                Host = "localhost",
                Name = "unit_test_" + s_index++
            };
        }

        public void Dispose()
        {
            // libération des ressources après le test
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
            var client = new MongoClient(connectionSettings);
            client.DropDatabase(m_config.Name);
        }

        //SujetDuTest_CasDeTest
        [Fact]
        //public void Given_RoleIsNull_When_CreateIsUsed_Then_ExpectArgumentNullException()
        public void Role_FailWhenNullPassedToCreate()
        {
            var repository = new RoleMongoRepository(m_config);

            var e = Assert.Throws<ArgumentNullException>(() => repository.Create(null));

            Assert.Equal("role", e.ParamName);
        }

        [Fact]
        public void Role_SuccessWhenFilledObject()
        {
            var repository = new RoleMongoRepository(m_config);
            var newRole = new Role
            {
                CreationDate = DateTime.Now,
                CreationUser = "TEST",
                Name = "Test",
                _id = Guid.NewGuid()
            };

            repository.Create(newRole);
        }
    }
}
