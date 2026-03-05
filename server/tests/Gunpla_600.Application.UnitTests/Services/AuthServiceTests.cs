using Gunpla_600.Application.Services;
using Gunpla_600.Application.DTOs;
using Gunpla_600.Application.Interfaces;
using Gunpla_600.Application.Interfaces.Security;
using Gunpla_600.Domain.Entities;
using Moq;
using FluentAssertions;

namespace Gunpla_600.Application.UnitTests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _authService = new AuthService(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object
            );
    }

    [Fact]
    public void RegisterAsync_ValidRequest_Should_Create_User_Successfully()    
    {
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            Password = "Password!123"
        };

        _userRepositroryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync((Users?)null);

        _var hashedPassword  = "hashed_Password!123";

        _passwordHasherMock
            .Setup(x => x.Hash(request.Password))
            .Returns(hashedPassword);
        
        var result = _authService.RegisterAsync(request).Result;

        result.Should.BeNotNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Account created successfully.");

        _userRepositoryMock.Verify(
            x => x.Hash(request.Password),
            Times.Once,
            "The password should be hashed before being stored."
        );

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.Is<Users>(u =>
                u.FirstName == request.FirstName &&
                u.LastName == request.LastName &&
                u.Email == request.Email &&
                u.PasswordHash == hashedPassword
            )),
            Times.Once,
            "A new user should be added to the repository with the correct details."
        );

        _userRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once,
            "Changes should be saved to the repository after adding a new user."
        )
    }

    [Fact]
    public void RegisterAsync_ExistingEmail_Should_Return_Failure()
    {
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@mail.com",
            Password = "Password!123"
        };
        
        var existingUser = new Users
        {
            FirstName = "Existing",
            LastName = "User",
            Email = request.Email,
            PasswordHash = "hashed_password"
        };

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync(existingUser);

        var result = _authService.RegisterAsync(request).Result;
        
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Email already exists.");

        _userRepositoryMock.Verify(
            x => x.GetByEmailAsync(request.Email),
            Times.Once,
            "The repository should be queried to check for existing email."
        );

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Users>()),
            Times.Never,
            "No new user should be added if the email already exists."
        );

        _passwordHasherMock.Verify(
            x => x.Hash(request.Password),
            Times.Never,
            "The password should not be hashed if the email already exists."
        );
        
        _userRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never,
            "Changes should not be saved if the email already exists."
        );
    }

    [Theory]
    [InlineData("John", "Doe", "johndoe@mail.com", "Password!123")]
    [InlineData("Jane", "Smith", "janesmith@mail.com", "Password!123")]
    [InlineData("Alice", "Johnson", "alicejohnson@mail.com", "Password!123")]
    public void RegisterAsync_MultipleValidRequests_Should_Create_Users_Successfully(
        string firstName, string lastName, string email, string password
        )
    {
        var request = new RegisterRequest
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync((Users?)null);

        var hashedPassword  = $"hashed_{password}";

        _passwordHasherMock
            .Setup(x => x.Hash(request.Password))
            .Returns(hashedPassword);
        
        var result = _authService.RegisterAsync(request).Result;

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Account created successfully.");

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.Is<Users>(u =>
                u.FirstName == request.FirstName &&
                u.LastName == request.LastName &&
                u.Email == request.Email &&
                u.PasswordHash == hashedPassword
            )),
            Times.Once,
            "A new user should be added to the repository with the correct details."
        );

        _userRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once,
            "Changes should be saved to the repository after adding a new user."
        );
    }
}