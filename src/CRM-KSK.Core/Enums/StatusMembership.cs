using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum StatusMembership : byte
{
    [Display(Name = "Активный")]
    Active = 1,

    [Display(Name = "Закончился")]
    Ended = 2,

    [Display(Name = "Разовый")]
    OneTime = 3
}
