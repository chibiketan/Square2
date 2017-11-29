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
    public class UserMongoRepositoryTest : BaseMongoRepositoryTest
    {
        private const string COLLECTION_NAME = "User";

        [Fact]
        public async Task CreateUser_FailWhenNullPassedToCreate_async()
        {
            var repository = new UserMongoRepository(m_config);

            var e = await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateUserAsync(null));

            Assert.Equal("user", e.ParamName);
        }

        [Fact]
        public async Task CreateUser_SuccessWhenFilledObject_async()
        {
            var repository = new UserMongoRepository(m_config);
            var newUser = new User
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Login = "Test",
                Password = "toto",
                FirstName = "first",
                LastName = "Name",
                _id = Guid.NewGuid()
            };

            await repository.CreateUserAsync(newUser);

            // check
            var mongoClient = CreateClient();
            var db = mongoClient.GetDatabase(m_config.Name);
            var collection = db.GetCollection<User>(COLLECTION_NAME);

            var testRole = await collection.AsQueryable().FirstOrDefaultAsync(r => r._id == newUser._id);

            Assert.NotNull(testRole);
            Assert.Equal(newUser.Login, testRole.Login);
            Assert.Equal(newUser.Password, testRole.Password);
            Assert.Equal(newUser.FirstName, testRole.FirstName);
            Assert.Equal(newUser.LastName, testRole.LastName);
            Assert.Equal(newUser.CreationUser, testRole.CreationUser);
            Assert.Equal(newUser.CreationDate.Ticks, testRole.CreationDate.Ticks);
        }

        [Fact]
        public async Task CreateUser_FailWhenIdAlreadyExist_async()
        {
            var repository = new UserMongoRepository(m_config);
            var newUser = new User
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Login = "Test",
                Password = "toto",
                FirstName = "first",
                LastName = "Name",
                _id = Guid.NewGuid()
            };
            var newUserDup = new User
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST2",
                Login = "Test2",
                Password = "toto2",
                FirstName = "first2",
                LastName = "Name2",
                _id = newUser._id
            };

            await repository.CreateUserAsync(newUser);
            await Assert.ThrowsAsync<ObjectExistsException>(() =>repository.CreateUserAsync(newUserDup));
        }

        [Fact]
        public async Task CreateUser_FailWhenLoginAlreadyExist_async()
        {
            var repository = new UserMongoRepository(m_config);
            var newUser = new User
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST",
                Login = "Test",
                Password = "toto",
                FirstName = "first",
                LastName = "Name",
                _id = Guid.NewGuid()
            };
            var newUserDup = new User
            {
                CreationDate = DateTime.UtcNow,
                CreationUser = "TEST2",
                Login = newUser.Login,
                Password = "toto2",
                FirstName = "first2",
                LastName = "Name2",
                _id = Guid.NewGuid()
            };

            await repository.CreateUserAsync(newUser);
            await Assert.ThrowsAsync<ObjectExistsException>(() =>repository.CreateUserAsync(newUserDup));
        }

    }
}
