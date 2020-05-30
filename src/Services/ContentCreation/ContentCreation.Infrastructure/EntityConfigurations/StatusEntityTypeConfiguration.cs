using LifeCMS.Services.ContentCreation.Domain.AggregateModels.StatusAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.SetupEntityModel();

            builder.Property(s => s.Mood)
                .HasMaxLength(50);

            builder.Property(s => s.Text)
                .HasMaxLength(300);
        }
    }
}