using LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
{
    public class NewsletterEntityTypeConfiguration : IEntityTypeConfiguration<Newsletter>
    {
        public void Configure(EntityTypeBuilder<Newsletter> builder)
        {
            builder.SetupEntityModel();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.Name).IsRequired();

            builder.OwnsOne(x => x.Body)
                    .Property(x => x.DesignSource);

            builder.OwnsOne(x => x.Body)
                    .Property(x => x.Html);
        }
    }
}