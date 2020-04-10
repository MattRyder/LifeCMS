using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.Infrastructure.Exensions;

namespace Socialite.Infrastructure.EntityConfigurations
{
    public class PhotoEntityTypeConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.SetupEntityModel();

            builder.Property(a => a.Name)
                .IsRequired();

            builder.Property(a => a.Width)
                .IsRequired();

            builder.Property(a => a.Height)
                .IsRequired();

            builder.Property(a => a.Location)
                .IsRequired()
                .HasConversion(
                    locationUri => locationUri.ToString(),
                    locationString => new Uri(locationString)
                );
        }
    }
}