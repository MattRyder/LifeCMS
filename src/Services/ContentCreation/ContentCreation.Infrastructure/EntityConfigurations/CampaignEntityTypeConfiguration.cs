using LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate;
using LifeCMS.Services.ContentCreation.Infrastructure.Exensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeCMS.Services.ContentCreation.Infrastructure.EntityConfigurations
{
    public class CampaignEntityTypeConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.SetupEntityModel();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.NewsletterTemplateId)
                .IsRequired();

            builder.Property(c => c.UserProfileId)
                .IsRequired();

            builder.Property(c => c.ScheduledDate)
                .IsRequired();

            builder.OwnsOne(c => c.Subject, onb =>
            {
                onb.Property(s => s.PreviewText);
                onb.Property(s => s.SubjectLineText).IsRequired();
            });
        }
    }
}