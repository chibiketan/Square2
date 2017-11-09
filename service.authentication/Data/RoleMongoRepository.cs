using System;
using Ketan.Square2.Service.Authentication.Data.Interface;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;

namespace Ketan.Square2.Service.Authentication.Data
{
    public class RoleMongoRepository : BaseMongoRepository, IRoleRepository
    {
        public RoleMongoRepository(DatabaseConfiguration config)
            : base(config)
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
    }
}