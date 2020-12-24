using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
{
    public class SubscriberEntityTypeConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.SetupEntityModel();

            builder
                .Property(x => x.AudienceId)
                .IsRequired();

            builder
                .Property(x => x.SubscriberToken)
                .IsRequired();

            builder
                .OwnsOne(x => x.EmailAddress)
                .Property(x => x.Value)
                .IsRequired()
                .HasColumnName("EmailAddress");
        }
    }
}
