using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Admin admin);
}
