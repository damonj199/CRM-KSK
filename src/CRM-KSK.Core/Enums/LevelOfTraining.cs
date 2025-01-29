using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum LevelOfTraining
{
    [Display(Name = "не задано")]
    Unknown,

    [Display(Name = "Начинающий")]
    Newbie,

    [Display(Name = "Продвинутый")]
    Advanced,

    [Display(Name = "Профи")]
    Pro
}
