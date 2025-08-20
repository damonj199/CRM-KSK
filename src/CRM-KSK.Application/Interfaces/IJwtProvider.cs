using CRM_KSK.Core;

namespace CRM_KSK.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(IUser user);
}
