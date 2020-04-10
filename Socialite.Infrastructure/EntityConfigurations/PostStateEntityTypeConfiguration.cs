using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Socialite.Domain.AggregateModels.PostAggregate;

namespace Socialite.Infrastructure.EntityConfigurations
{
    public class PostStateEntityTypeConfiguration : IEntityTypeConfiguration<PostState>
    {
        public void Configure(EntityTypeBuilder<PostState> builder)
        {
            var postStateSeedData = PostState.List().ToArray();

            builder.HasData(postStateSeedData);
        }
    }
}