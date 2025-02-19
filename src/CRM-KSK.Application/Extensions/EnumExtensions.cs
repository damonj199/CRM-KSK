﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CRM_KSK.Application.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? value.ToString();
    }
}
