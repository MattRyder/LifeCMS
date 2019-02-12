using Bogus;
using Socialite.Domain.AggregateModels.StatusAggregate;

namespace Socialite.UnitTests.Factories
{
    public class StatusFactory
    {
        public static Status Create()
        {
            return new Faker<Status>().CustomInstantiator(
                f => new Status(
                    f.Lorem.Sentence(),
                    f.PickRandom(new[] { "👍", "👌", "🤘" }
                ))
            );
        }
    }
}