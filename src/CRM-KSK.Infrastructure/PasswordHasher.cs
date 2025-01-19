using CRM_KSK.Application.Interfaces;

namespace CRM_KSK.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}
