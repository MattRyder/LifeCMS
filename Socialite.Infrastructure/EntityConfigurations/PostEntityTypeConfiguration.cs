using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.EntityConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.SetupEntityModel();

            builder.Property(p => p.Text)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(140);

            builder.Property<int>("StateId")
                .IsRequired();

            builder.HasOne(p => p.State)
                .WithMany()
                .HasForeignKey("StateId");
        }
    }
}