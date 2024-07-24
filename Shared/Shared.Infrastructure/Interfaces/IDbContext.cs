namespace Shared.Infrastructure.Interfaces;

public interface IDbContext
{
    public bool IsConnected { get; set; }
}