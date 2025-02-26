using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core;

public interface IUser
{
    string FirstName { get; set; }
    Roles Role { get;  }
    string PasswordHash { get;  }
}
