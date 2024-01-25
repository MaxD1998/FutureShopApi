namespace Shared.Dtos.Settings;

public class Settings
{
    public ConnectionSettings ConnectionSettings { get; set; }

    public JwtSettings JwtSettings { get; }

    public RefreshTokenSettings RefreshTokenSettings { get; }
}