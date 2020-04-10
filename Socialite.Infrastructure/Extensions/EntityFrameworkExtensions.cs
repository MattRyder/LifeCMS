using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Socialite.Domain.Common;

namespace Socialite.Infrastructure.Exensions
{
    internal static class EntityFrameworkExtensions
    {
        public static void SetupEntityModel<T>(this EntityTypeBuilder<T> entityModel)
            where T : BaseEntity
        {
            const string DATETIME_NOW_FUNC = "CURRENT_TIMESTAMP(6)";

            entityModel.Ignore(m => m.Events);

            entityModel.HasKey(m => m.Id);

            entityModel.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            entityModel.Property(m => m.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql(DATETIME_NOW_FUNC)
                .ValueGeneratedOnAdd();

            entityModel.Property(m => m.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql(DATETIME_NOW_FUNC)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}