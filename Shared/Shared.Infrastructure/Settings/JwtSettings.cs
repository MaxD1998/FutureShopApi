namespace Shared.Infrastructure.Settings;

public class JwtSettings
{
    public int ExpireTime { get; init; }

    public string JwtKey { get; init; }
}