using System.ComponentModel.DataAnnotations;

namespace CRM_KSK.Core.Enums;

public enum TypeTrainings
{
    [Display(Name = "Неизвестно")]
    Unknown = 0,

    [Display(Name = "60 Спорт")]
    Sport = 1,

    [Display(Name = "60 Лайт")]
    Light = 2,

    [Display(Name = "30М")]
    BabiesHalf = 3,

    [Display(Name = "30+30")]
    BabiesMixed = 4,

    [Display(Name = "60М")]
    Babies = 5,

    [Display(Name = "Иппотерапия")]
    Hippotherapy = 6,

    [Display(Name = "ОФП")]
    PhysicalTraining = 7,

    [Display(Name = "Аренда")]
    Rent = 8,

    [Display(Name = "Частный владелец")]
    Owner = 9,

    [Display(Name = "Вольтижировка")]
    Voltiger = 10,

    [Display(Name = "Группа")]
    Group = 11,

    [Display(Name = "Тренажёры 30")]
    ExerciseMachine30 = 12,

    [Display(Name = "Тренажёры 45")]
    ExerciseMachine45 =13
}
