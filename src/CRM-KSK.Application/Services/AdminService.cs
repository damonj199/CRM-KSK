using AutoMapper;
using CRM_KSK.Application.Configurations;
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

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest register, CancellationToken cancellationToken)
    {
        var exisitingAdmin = await _adminRepository.GetByEmail(register.Email);

        if (exisitingAdmin != null)
            return RegistrationResult.Failure("Пользователь с таким Email уже зарегистрирован");

        var hashedPassword = _passwordHasher.Generate(register.Password);
        var mapping = _mapper.Map<Admin>(register);

        var admin = await _adminRepository.AddAdmin(Guid.NewGuid(), mapping.FirstName, mapping.LastName, mapping.Email, hashedPassword, cancellationToken);

        return RegistrationResult.Success();
    }

    public async Task<LoginResult> LoginAsync(LoginRequest login, CancellationToken cancellationToken)
    {
        var user = await _adminRepository.GetByEmail(login.Email);

        if (user == null)
            return LoginResult.Failure("Неверный логин или пароль! Попробуйте еще раз");

        var result = _passwordHasher.Verify(login.Password, user.PasswordHash);
        
        if(result == false)
            return LoginResult.Failure("Неверный логин или пароль! Попробуйте еще раз");

        var token = _jwtProvider.GenerateToken(user);

        return LoginResult.Success(token);
    }

}
