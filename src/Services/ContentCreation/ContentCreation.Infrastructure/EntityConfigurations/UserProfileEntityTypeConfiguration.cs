using LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
{
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.SetupEntityModel();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.Name).IsRequired();

            builder
            .OwnsOne(x => x.EmailAddress)
            .Property(x => x.Value)
            .HasColumnName("EmailAddress");
        }
    }
}