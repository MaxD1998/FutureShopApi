using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Bases;
using Shared.Infrastructure.Constants;

namespace Shared.Infrastructure.Bases;

public abstract class BaseTranslationConfig<TEntity> : BaseConfig<TEntity> where TEntity : BaseTranslationEntity
{
    protected override void ConfigureEntity(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.Lang)
            .HasMaxLength(StringLengthConst.LangString)
            .HasColumnOrder(50)
            .IsRequired();

        builder.Property(x => x.Translation)
            .HasColumnOrder(51)
            .IsRequired();
    }
}