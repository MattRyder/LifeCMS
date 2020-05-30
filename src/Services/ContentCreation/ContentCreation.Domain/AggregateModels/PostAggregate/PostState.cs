using System;
using System.Collections.Generic;
using System.Linq;
using LifeCMS.Services.ContentCreation.Domain.Common;
using LifeCMS.Services.ContentCreation.Domain.Exceptions;

namespace LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate
{
    public class PostState : Enumeration
    {
        public static PostState Drafted = new PostState(1, nameof(Drafted).ToLowerInvariant());
        public static PostState Published = new PostState(2, nameof(Published).ToLowerInvariant());

        public PostState(int id, string name) : base(id, name) { }

        public static IEnumerable<PostState> List() => new[] { Drafted, Published };

        public static PostState FromName(string name)
        {
            var state = List().SingleOrDefault(ps => string.Equals(ps.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if(state == null)
            {
                throw new PostDomainException($"Invalid value for PostState \"{name}\". Expected one of: [{string.Join(", ", List().Select(s => s.Name))}]");
            }

            return state;
        }
    }
}
