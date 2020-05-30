using LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
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