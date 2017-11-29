using System;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Data.Interface;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;

namespace Ketan.Square2.Service.Authentication.Data
{
    public class UserMongoRepository : IUserRepository
    {
        private readonly DatabaseConfiguration m_configuration;

        public UserMongoRepository(DatabaseConfiguration configuration)
        {
            m_configuration = configuration;
        }
        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
