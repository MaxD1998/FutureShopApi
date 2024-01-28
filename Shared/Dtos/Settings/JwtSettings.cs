namespace Shared.Dtos.Settings;

public class JwtSettings
{
    public int ExpireTime { get; init; }

    public string JwtKey { get; init; }
}