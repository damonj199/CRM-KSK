using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum Roles
{
    [Display(Name = "Неизвестно")]
    Unknown = 0,

    [Display(Name = "Администратор")]
    Admin = 1,

    [Display(Name = "Тренер")]
    Trainer = 2
}
