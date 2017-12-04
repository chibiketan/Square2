using System;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Data.Interface;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Ketan.Square2.Service.Authentication.Data
{
    public class UserMongoRepository : BaseMongoRepository<User>, IUserRepository
    {
        public UserMongoRepository(DatabaseConfiguration configuration)
            : base(configuration, "User")
        {
        }
        public async Task CreateUserAsync(User user)
        {
            if (null == user)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // On test si l'utilisateur existe déjà
            var existingUSer = await m_collection.AsQueryable().FirstOrDefaultAsync(r => r._id == user._id);

            if (null != existingUSer)
            {
                throw new ObjectExistsException($"An User with id \"{user._id}\" already exists");
            }

            // On test si l'utilisateur existe déjà
            existingUSer = await m_collection.AsQueryable().FirstOrDefaultAsync(r => r.Login == user.Login);

            if (null != existingUSer)
            {
                throw new ObjectExistsException($"An User with login \"{user.Login}\" already exists");
            }

            StripDates(user);
            await m_collection.InsertOneAsync(user);
        }

        public Task<User> GetUserByLoginAsync(string login)
        {
            if (null == login)
            {
                throw new ArgumentNullException(nameof(login));
            }

            if ("" == login)
            {
                throw new ArgumentException(nameof(login));
            }

            var query = from u in m_collection.AsQueryable()
                        where u.Login == login
                        select u;

            return query.FirstOrDefaultAsync();
        }

        protected override void OnCollectionCreated(IMongoCollection<User> collection)
        {
            // Création d'un index sur le champ Login
            collection.Indexes.CreateOneAsync(Builders<User>.IndexKeys.Ascending(u => u.Login), new CreateIndexOptions { Name = "User_Login", Unique = true }).Wait();
        }

        private void StripDates(User user)
        {
            user.CreationDate = user.CreationDate.StripTick();
            user.ModificationDate = user.ModificationDate.StripTick();
            // mettre à jour les dates pour le rôle associé ?
        }
    }
}
