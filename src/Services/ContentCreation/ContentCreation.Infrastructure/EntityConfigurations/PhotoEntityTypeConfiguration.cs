using System;
using LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
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
