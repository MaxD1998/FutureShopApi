using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;

namespace Shared.Domain.Bases;

public abstract class BaseEntity : IEntity
{
    public DateTime CreateTime { get; protected set; }

    public Guid Id { get; set; }

    public DateTime? ModifyTime { get; protected set; }

    public void MarkCreated()
    {
        if (CreateTime == default)
            CreateTime = DateTime.UtcNow;
    }

    public void MarkModified()
    {
        ModifyTime = DateTime.UtcNow;
    }

    #region Methods

    protected void ValidateNonNegativeProperty(string propertyName, int value)
    {
        if (value < 0)
            throw new PropertyWasNegativeException(propertyName);
    }

    protected void ValidateRequiredLangStringProperty(string propertyName, string value)
        => ValidateRequiredStringProperty(propertyName, value, StringLengthConst.LangString);

    protected void ValidateRequiredLongStringProperty(string propertyName, string value)
        => ValidateRequiredStringProperty(propertyName, value, StringLengthConst.LongString);

    protected void ValidateRequiredProperty(string propertyName, Guid value)
    {
        if (value == Guid.Empty)
            throw new PropertyWasEmptyException(propertyName);
    }

    protected void ValidateRequiredProperty(string propertyName, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new PropertyWasEmptyException(propertyName);
    }

    protected void ValidateShortStringProperty(string propertyName, string value)
        => ValidateLengthStringProperty(propertyName, value, StringLengthConst.ShortString);

    private void ValidateLengthStringProperty(string propertyName, string value, int stringLength)
    {
        if (value.Length > stringLength)
            throw new PropertyWasTooLongException(propertyName, stringLength);
    }

    private void ValidateRequiredStringProperty(string propertyName, string value, int stringLength)
    {
        ValidateRequiredProperty(propertyName, value);
        ValidateLengthStringProperty(propertyName, value, stringLength);
    }

    #endregion Methods
}