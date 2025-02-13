using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum TypeTrainings
{
    [Display(Name = "Неизвестно")]
    Unknown,

    [Display(Name = "60 Спорт")]
    Sport,

    [Display(Name = "60 Лайт")]
    Light,

    [Display(Name = "30М")]
    BabiesHalf,

    [Display(Name = "30+30")]
    BabiesMixed,

    [Display(Name = "60М")]
    Babies,

    [Display(Name = "Иппотерапия")]
    Hippotherapy,

    [Display(Name = "ОФП")]
    PhysicalTraining,

    [Display(Name = "Аренда")]
    Rent,

    [Display(Name = "Частный владелец")]
    Owner,

    [Display(Name = "Вольтижировка")]
    Voltiger,

    [Display(Name = "Группа")]
    Group
}
