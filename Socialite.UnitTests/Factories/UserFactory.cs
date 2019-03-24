using System;
using System.Collections.Generic;
using Bogus;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Domain.AggregateModels.UsersAggregate;

namespace Socialite.UnitTests.Factories
{
    public class UserFactory : FactoryBase<User>
    {
        public static User Create()
        {
            return new Faker<User>()
                .CustomInstantiator(f => new User(
                    f.Internet.ExampleEmail(),
                    f.Name.FullName()
                ));
        }

        public static IEnumerable<User> CreateList() => MakeList(Create);
    }
}