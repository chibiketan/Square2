using System;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Data.Interface;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Driver;

namespace Ketan.Square2.Service.Authentication.Data
{
    public class RoleMongoRepository : BaseMongoRepository<Role>, IRoleRepository
    {
        public RoleMongoRepository(DatabaseConfiguration config)
            : base(config, "Role")
        {
            
        }

        public Task CreateAsync(Role role)
        {
            if (null == role)
            {
                throw new ArgumentNullException(nameof(role));
            }

            StripDates(role);

            return m_collection.InsertOneAsync(role);
        }

        protected override void OnCollectionCreated(IMongoCollection<Role> collection)
        {
            // Nothing to do
        }

        private void StripDates(Role role)
        {
            role.CreationDate = role.CreationDate.StripTick();
            role.ModificationDate = role.ModificationDate.StripTick();
        }
    }
}