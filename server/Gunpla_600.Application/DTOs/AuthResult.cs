namespace Gunpla_600.Application.DTOs;

public class AuthResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }

    public static AuthResult FailResult(string message)
        => new() { Success = false, Message = message };
    public static AuthResult SuccessResult(string message, string? token = null)
        => new() { Success = true, Message = message, Token = token };
}