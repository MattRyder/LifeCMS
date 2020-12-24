using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
{
    public class AudienceEntityTypeConfiguration : IEntityTypeConfiguration<Audience>
    {
        public void Configure(EntityTypeBuilder<Audience> builder)
        {
            builder.SetupEntityModel();

            builder.Property(audience => audience.UserId)
                .IsRequired();

            builder.Property(audience => audience.Name)
                .IsRequired();

            builder.HasMany(a => a.Subscribers)
                .WithOne()
                .HasForeignKey(subscriber => subscriber.AudienceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
