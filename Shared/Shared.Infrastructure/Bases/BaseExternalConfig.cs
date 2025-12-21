using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Bases;

namespace Shared.Infrastructure.Bases;

public abstract class BaseExternalConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseExternalEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name.Replace("Entity", string.Empty));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0);

        builder.Property(x => x.ExternalId)
            .HasColumnOrder(1)
            .IsRequired();

        builder.Property(x => x.CreateTime)
            .HasColumnOrder(2)
            .IsRequired();

        builder.Property(x => x.ModifyTime)
            .HasColumnOrder(3);

        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}