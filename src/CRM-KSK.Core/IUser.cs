using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core;

public interface IUser
{
    Roles Role { get;  }
    string PasswordHash { get;  }
}
