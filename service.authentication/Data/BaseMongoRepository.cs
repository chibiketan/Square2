using Ketan.Square2.Service.Authentication.Model.Configuration;

namespace Ketan.Square2.Service.Authentication.Data
{
    public class BaseMongoRepository
    {
        protected DatabaseConfiguration m_config { private get; set; }

        public BaseMongoRepository(DatabaseConfiguration config)
        {
            m_config = config;
        }
    }
}