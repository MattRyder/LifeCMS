using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.EntityConfigurations
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