using Gunpla_600.Application.Interfaces;
using Gunpla_600.Domain.Entities;
using Gunpla_600.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gunpla_600.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Users?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(Users user)
        => await _context.Users.AddAsync(user);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
