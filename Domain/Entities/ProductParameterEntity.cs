using Shared.Bases;

namespace Domain.Entities;

public class ProductParameterEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    #endregion Related Data
}