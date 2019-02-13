using Bogus;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Exceptions;
using Socialite.UnitTests.Factories;
using Xunit;

namespace Socialite.UnitTests.Domain
{
    public class StatusAggregateTest
    {
        [Fact]
        public void Constructor_ReturnsStatus_WhenValid()
        {
            var status = StatusFactory.Create();

            Assert.NotNull(status);
        }

        [Fact]
        public void Constructor_ThrowsException_GivenEmptyMood()
        {
            var validStatus = StatusFactory.Create();
            Assert.Throws<StatusDomainException>(() => new Status(string.Empty, validStatus.Text));
        }

        [Fact]
        public void Constructor_ThrowsException_GivenEmptyText()
        {
            var validStatus = StatusFactory.Create();
            Assert.Throws<StatusDomainException>(() => new Status(validStatus.Mood, string.Empty));
        }
    }
}