using ApartmentPlanner.Api.Domain.Entities;
using ApartmentPlanner.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ApartmentPlanner.Api.Application.DTOs;


namespace ApartmentPlanner.Api.Application.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(AppDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // Verificar se o email já está cadastrado
        var userExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
        if (userExists)
            throw new Exception("Email já cadastrado");
        // Criar um usuário temporário para gerar o hash da senha
        var tempUser = new User(request.Name, request.Email, "temp");
        // Gerar o hash da senha usando o PasswordHasher
        var hash = _passwordHasher.HashPassword(tempUser, request.Password);
        var user = new User(request.Name, request.Email, hash);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }
}
