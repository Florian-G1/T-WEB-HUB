using Gunpla_600.Application.Interfaces.Security;
using Microsoft.AspNetCore.Identity;

namespace Gunpla_600.Infrastructure.Services.Security;

public class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password)
        => _hasher.HashPassword(null!, password);

    public bool Verify(string hash, string password)
        => _hasher.VerifyHashedPassword(null!, hash, password)
            == PasswordVerificationResult.Success;
}