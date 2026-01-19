using Gunpla_600.Domain.Entities;

namespace Gunpla_600.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(Users user);
}