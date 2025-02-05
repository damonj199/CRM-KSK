using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Models;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Infrastructure;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterRequest, Admin>();
        CreateMap<Client, ClientDto>();
        CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.Trainer, opt => opt.Ignore())
            .ForMember(dest => dest.Membership, opt => opt.Ignore());
        CreateMap<TrainerDto, Trainer>();
        CreateMap<Trainer, TrainerDto>();
        CreateMap<ScheduleDto, Schedule>();
        CreateMap<Schedule, ScheduleDto>();
    }
}
