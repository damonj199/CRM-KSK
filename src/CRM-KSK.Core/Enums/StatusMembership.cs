using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum StatusMembership
{
    [Display(Name = "Активный")]
    Active,

    [Display(Name = "Закончился")]
    Ended,

    [Display(Name = "Разовый")]
    OneTime
}
