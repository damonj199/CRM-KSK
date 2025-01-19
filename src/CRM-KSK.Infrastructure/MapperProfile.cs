using AutoMapper;
using CRM_KSK.Application.Models;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Infrastructure;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterRequest, Admin>();
    }
}
