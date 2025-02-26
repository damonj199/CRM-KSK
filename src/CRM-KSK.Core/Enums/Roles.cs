using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum Roles
{
    [Display(Name = "Неизвестно")]
    Unknown,

    [Display(Name = "Администратор")]
    Admin,

    [Display(Name = "Тренер")]
    Trainer
}
