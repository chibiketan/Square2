using System;
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

        public void Create(Role role)
        {
            if (null == role)
            {
                throw new ArgumentNullException(nameof(role));
            }
            throw new System.NotImplementedException();
        }

        protected override void OnCollectionCreated(IMongoCollection<Role> collection)
        {
            // Nothing to do
        }
    }
}