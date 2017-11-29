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
    public class RoleMongoRepositoryTest : BaseMongoRepositoryTest
    {
        private const string COLLECTION_NAME = "Role";

        //SujetDuTest_CasDeTest
        [Fact]
        //public void Given_RoleIsNull_When_CreateIsUsed_Then_ExpectArgumentNullException()
        public async Task CreateRole_FailWhenNullPassedToCreate_async()
        {
            var repository = new RoleMongoRepository(m_config);

            var e = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateRoleAsync(null));

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

            await repository.CreateRoleAsync(newRole);

            // check
            var mongoClient = CreateClient();
            var db = mongoClient.GetDatabase(m_config.Name);
            var collection = db.GetCollection<Role>(COLLECTION_NAME);

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

            await repository.CreateRoleAsync(newRole);
            await Assert.ThrowsAsync<ObjectExistsException>(() =>repository.CreateRoleAsync(newRoleDup));
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

            await repository.CreateRoleAsync(newRole);
            await Assert.ThrowsAsync<ObjectExistsException>(() =>repository.CreateRoleAsync(newRoleDup));
        }

        [Fact]
        public async Task GetAllRole_SuccessWithoutData_async()
        {
            var repository = new RoleMongoRepository(m_config);
            var newRole = new Role
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Name = "Test",
                _id = Guid.NewGuid()
            };

            var resultList = await repository.GetAllRoleAsync();

            // check
            Assert.Empty(resultList);
        }

        [Fact]
        public async Task GetAllRole_SuccessWithData_async()
        {
            var repository = new RoleMongoRepository(m_config);
            var newRoleList = new List<Role>
            {
                new Role
                {
                    CreationDate = DateTime.UtcNow.StripTick(),
                    CreationUser = "TEST",
                    Name = "Test",
                    _id = Guid.NewGuid()
                },
                new Role
                {
                    CreationDate = DateTime.UtcNow.StripTick(),
                    CreationUser = "TEST1",
                    Name = "Test1",
                    _id = Guid.NewGuid()
                },
                new Role
                {
                    CreationDate = DateTime.UtcNow.StripTick(),
                    CreationUser = "TEST2",
                    Name = "Test2",
                    _id = Guid.NewGuid()
                }
            };

            var mongoClient = CreateClient();
            var db = mongoClient.GetDatabase(m_config.Name);
            var collection = db.GetCollection<Role>(COLLECTION_NAME);

            await collection.InsertManyAsync(newRoleList);

            // do
            var resultList = await repository.GetAllRoleAsync();

            // check
            Assert.Equal(newRoleList.Count, resultList.Count);
            for (var i = 0; i < newRoleList.Count; ++i)
            {
                Assert.Equal(newRoleList[i]._id, resultList[i]._id);
                Assert.Equal(newRoleList[i].Name, resultList[i].Name);
                Assert.Equal(newRoleList[i].CreationUser, resultList[i].CreationUser);
                Assert.Equal(newRoleList[i].CreationDate.Ticks, resultList[i].CreationDate.Ticks);
            }
        }

        [Fact]
        public async Task GetRoleByName_WithNullAsParameter_async()
        {
            // prepare
            var repository = new RoleMongoRepository(m_config);

            // do
            // check
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.GetRoleByNameAsync(null));
        }

        [Fact]
        public async Task GetRoleByName_WithEmptyStringAsParameter_async()
        {
            // prepare
            var repository = new RoleMongoRepository(m_config);

            // do
            // check
            await Assert.ThrowsAsync<ArgumentException>(() => repository.GetRoleByNameAsync(""));
        }

        [Fact]
        public async Task GetRoleByName_WithExistingRole_async()
        {
            // prepare
            var repository = new RoleMongoRepository(m_config);
            const string roleName = "MyRole";
            var role = new Role
            {
                _id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow.StripTick(),
                ModificationUser = "A user",
                Name = roleName
            };
            var mongoClient = CreateClient();
            var db = mongoClient.GetDatabase(m_config.Name);
            var collection = db.GetCollection<Role>(COLLECTION_NAME);

            await collection.InsertOneAsync(role);

            // do
            var dbRole = await repository.GetRoleByNameAsync(roleName);

            // check
            Assert.NotNull(dbRole);
            Assert.Equal(role.Name, dbRole.Name);
            Assert.Equal(role.CreationDate, dbRole.CreationDate);
            Assert.Equal(role.CreationUser, dbRole.CreationUser);
            Assert.Equal(role._id, dbRole._id);
        }

        [Fact]
        public async Task GetRoleByName_WithRoleNotExisting_async()
        {
            var repository = new RoleMongoRepository(m_config);
            const string roleName = "MyRole2";
            var role = new Role
            {
                _id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                ModificationUser = "A user",
                Name = roleName
            };
            var mongoClient = CreateClient();
            var db = mongoClient.GetDatabase(m_config.Name);
            var collection = db.GetCollection<Role>(COLLECTION_NAME);

            await collection.InsertOneAsync(role);

            // do
            var dbRole = await repository.GetRoleByNameAsync("NotExistingRole");

            // check
            Assert.Null(dbRole);
        }
    }
}
