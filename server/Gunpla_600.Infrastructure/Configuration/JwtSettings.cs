namespace Gunpla_600.Infrastructure.Configuration;

public class JwtSettings
{
    public string SecretKey { get; set; } = null!;
    public int ExpirationInMinutes { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}