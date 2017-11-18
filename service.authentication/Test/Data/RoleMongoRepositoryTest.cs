using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Data;
using Ketan.Square2.Service.Authentication.Data.Interface;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
        public async Task CreateRole_FailWhenNullPassedToCreate_async()
        {
            var repository = new RoleMongoRepository(m_config);

            var e = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateAsync(null));

            Assert.Equal("role", e.ParamName);
        }

        [Fact]
        public async Task CreateRole_SuccessWhenFilledObject_async()
        {
            var repository = new RoleMongoRepository(m_config);
            var newRole = new Role
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Name = "Test",
                _id = Guid.NewGuid()
            };

            await repository.CreateAsync(newRole);

            // check
            var mongoClient = CreateClient();
            var db = mongoClient.GetDatabase(m_config.Name);
            var collection = db.GetCollection<Role>("Role");

            var testRole = await collection.AsQueryable().FirstOrDefaultAsync(r => r._id == newRole._id);

            Assert.NotNull(testRole);
            Assert.Equal(newRole.Name, testRole.Name);
            Assert.Equal(newRole.CreationUser, testRole.CreationUser);
            Assert.Equal(newRole.CreationDate.Ticks, testRole.CreationDate.Ticks);
        }

        [Fact]
        public async Task CreateRole_FailWhenIdAlreadyExist_async()
        {
            var repository = new RoleMongoRepository(m_config);
            var newRole = new Role
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Name = "Test",
                _id = Guid.NewGuid()
            };
            var newRoleDup = new Role
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST2",
                Name = "Test2",
                _id = newRole._id
            };

            await repository.CreateAsync(newRole);
            await Assert.ThrowsAsync<ObjectExistsException>(() =>repository.CreateAsync(newRoleDup));
        }

        [Fact]
        public async Task CreateRole_FailWhenNameAlreadyExist_async()
        {
            var repository = new RoleMongoRepository(m_config);
            var newRole = new Role
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Name = "Test",
                _id = Guid.NewGuid()
            };
            var newRoleDup = new Role
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST2",
                Name = newRole.Name,
                _id = Guid.NewGuid()
            };

            await repository.CreateAsync(newRole);
            await Assert.ThrowsAsync<ObjectExistsException>(() =>repository.CreateAsync(newRoleDup));
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
    }
}
