using System.Collections.Generic;
using Bogus;
using Person = Socialite.Domain.AggregateModels.PersonAggregate.Person;

namespace Socialite.UnitTests.Factories
{
    public class PersonFactory : FactoryBase<Person>
    {
        public static Person Create()
        {
            return new Faker<Person>()
                .CustomInstantiator(f => new Person(
                    f.Internet.ExampleEmail(),
                    f.Internet.Password()
                ));
        }

        public static IEnumerable<Person> CreateList() => MakeList(Create);
    }
}