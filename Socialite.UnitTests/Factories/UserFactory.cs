using Bogus;
using Socialite.Domain.AggregateModels.UsersAggregate;

namespace Socialite.UnitTests.Factories
{
    public class UserFactory
    {
        public static User Create()
        {
            return new Faker<User>()
                .CustomInstantiator(f => new User(
                    f.Internet.ExampleEmail(),
                    f.Name.FullName()
                ));
        }
    }
}