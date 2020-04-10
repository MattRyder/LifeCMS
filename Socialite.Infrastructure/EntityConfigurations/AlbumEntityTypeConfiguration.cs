using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.EntityConfigurations
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.SetupEntityModel();

            builder.Property(a => a.Name)
                .IsRequired();

            builder.HasMany(a => a.Photos)
                .WithOne();
        }
    }
}