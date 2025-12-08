using Gunpla_600.Domain.Entities;

namespace Gunpla_600.Application.Interfaces;

public interface IUserRepository
{
    Task<Users?> GetByEmailAsync(string email);
    Task AddAsync(Users user);
    Task SaveChangesAsync();
}