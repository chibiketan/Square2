using System;
using System.Collections.Generic;
using System.Text;
using Ketan.Square2.Service.Authentication.Data;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using Xunit;

namespace Ketan.Square2.Service.Authentication.Test.Data
{
    public class RoleMongoRepositoryTest : IDisposable
    {
        private DatabaseConfiguration m_config;

        public RoleMongoRepositoryTest()
        {
            // Création des ressources avant le test
            m_config = new DatabaseConfiguration
            {
                Host = "localhost",
                Name = "unit_test"
            };
        }

        public void Dispose()
        {
            // libération des ressources après le test
        }

        [Fact]
        public void Given_CallCreation_When_UserIsNull_Then_ExpectException()
        {
            var repository = new RoleMongoRepository(m_config);

            var e = Assert.Throws<ArgumentNullException>(() => repository.Create(null));

            Assert.Equal("user", e.ParamName);
        }
    }
}
