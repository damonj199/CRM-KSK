using AutoMapper;
using CRM_KSK.Application.Configurations;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Application.Models;
using CRM_KSK.Core;
using CRM_KSK.Core.Entities;
using Serilog;

namespace CRM_KSK.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher,
            IMapper mapper, IJwtProvider jwtProvider, ITrainerRepository trainerRepository)
    {
        _authRepository = authRepository;
        _trainerRepository = trainerRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest register, CancellationToken cancellationToken)
    {
        var exisitingAdmin = await _authRepository.GetByPhone(register.Phone, cancellationToken);

        if (exisitingAdmin != null)
            return RegistrationResult.Failure("Пользователь с таким ноиером уже зарегистрирован");

        var hashedPassword = _passwordHasher.Generate(register.Password);
        var mapping = _mapper.Map<Admin>(register);

        var admin = await _authRepository.AddAdmin(Guid.NewGuid(), mapping.FirstName, mapping.LastName,
                                                    mapping.Phone, hashedPassword, cancellationToken);

        return RegistrationResult.Success();
    }

    public async Task<LoginResult> LoginAsync(LoginRequest login, CancellationToken cancellationToken)
    {
        IUser user;

        user = await _authRepository.GetByPhone(login.Phone, cancellationToken);

        if(user == null)
            user = await _trainerRepository.GetTrainerByPhone(login.Phone, cancellationToken);
        
        if (user == null)
            return LoginResult.Failure("Неверный логин или пароль! Попробуйте еще раз");

        var result = _passwordHasher.Verify(login.Password, user.PasswordHash);
        
        if (result == false)
            return LoginResult.Failure("Неверный логин или пароль! Попробуйте еще раз");

        var token = _jwtProvider.GenerateToken(user);

        return LoginResult.Success(token);
    }

    public async Task<string> UpdatePasswordAsync(UpdatePasswordDto update, CancellationToken token)
    {
        IUser user;

        user = await _trainerRepository.GetTrainerByIdAsync(update.Id, token);
        if(user != null)
        {
            var result = _passwordHasher.Verify(update.OldPassword, user.PasswordHash);
            if (result)
            {
                var pasHash = _passwordHasher.Generate(update.NewPassword);
                user.PasswordHash = pasHash;
                await _trainerRepository.UpdatePasswordAsync((Trainer)user, token);
                return "Успешно";
            }
            else
            {
                return "Ошибка смены пароля";
            }
        }
        else if (user == null)
        {
            user = await _authRepository.GetAdminByIdAsync(update.Id, token);
            var result = _passwordHasher.Verify(update.OldPassword, user.PasswordHash);

            if (result)
            {
                var pasHash = _passwordHasher.Generate(update.NewPassword);
                user.PasswordHash = pasHash;
                await _authRepository.UpdatePasswordAsync((Admin)user, token);
                return "Успешно";
            }
            else
            {
                return "Ошибка смены пароля";
            }
        }
        return "Неизвествая ошибка";
    }
}
