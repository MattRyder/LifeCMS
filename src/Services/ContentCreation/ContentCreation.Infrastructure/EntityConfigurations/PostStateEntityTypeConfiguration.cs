using System.Linq;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
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