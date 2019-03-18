using Socialite.WebAPI.Authorization.IdentityServer;
using Xunit;
using System.Linq;

namespace Socialite.UnitTests.Authorization
{
    public class ConfigTest
    {
        [Fact]
        public void GetApis_ReturnsList()
        {
            var expectedApiCount = 1;

            var apis = Config.GetApis();

            Assert.NotNull(apis);

            Assert.Equal(expectedApiCount, apis.Count());
        }

        [Fact]
        public void GetIdentityResources_ReturnsList()
        {
            var expectedApiCount = 1;

            var identityResources = Config.GetIdentityResources();

            Assert.NotNull(identityResources);

            Assert.Equal(expectedApiCount, identityResources.Count());
        }

        [Fact]
        public void GetClients_ReturnsList()
        {
            var expectedApiCount = 1;

            var clients = Config.GetClients();

            Assert.NotNull(clients);

            Assert.Equal(expectedApiCount, clients.Count());
        }
    }
}