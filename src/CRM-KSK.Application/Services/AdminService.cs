using AutoMapper;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;

    public AdminService(IAdminRepository adminRepository, IPasswordHasher passwordHasher, IMapper mapper, IJwtProvider jwtProvider)
    {
        _adminRepository = adminRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(RegisterRequest register, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Generate(register.Password);
        var mapping = _mapper.Map<Admin>(register);

        var admin = await _adminRepository.AddAdmin(Guid.NewGuid(), mapping.FirstName, mapping.LastName, mapping.Email, hashedPassword, cancellationToken);
    }

    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        var user = _adminRepository.GetByEmail(email);
        if (user == null)
        {
            throw new Exception("Неверный логин или пароль! Попробуйте еще раз");
        }
        var result = _passwordHasher.Verify(password, user.Result.PasswordHash);
        if(result == false)
        {
            throw new Exception("Неверный логин или пароль! Попробуйте еще раз");
        }

        var token = _jwtProvider.GenerateToken(user.Result);

        return token;
    }
}
