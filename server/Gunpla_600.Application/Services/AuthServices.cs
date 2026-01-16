using Gunpla_600.Application.DTOs;
using Gunpla_600.Application.Interfaces;
using Gunpla_600.Application.Interfaces.Security;
using Gunpla_600.Domain.Entities;

namespace Gunpla_600.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    private AuthResult WrongLoginResponse()
        => AuthResult.FailResult("Email or password is incorrect.");

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing != null)
        {
            return AuthResult.FailResult("Email is already in use.");
        };

        var user = new Users 
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password)
        };


        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return AuthResult.SuccessResult("Account created successfully.");
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return WrongLoginResponse();
        };

        var result = _passwordHasher.Verify(
            user.PasswordHash,
            request.Password
            );

        return result ? AuthResult.SuccessResult("Login successful.") : WrongLoginResponse();
    }
}