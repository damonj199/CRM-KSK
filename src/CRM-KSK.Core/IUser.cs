using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core;

public interface IUser
{
    Guid Id { get;  }
    string FirstName { get; set; }
    Roles Role { get;  }
    string PasswordHash { get; set; }
}
