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
        CreateMap<TrainerDto, Trainer>().ReverseMap();
        CreateMap<ScheduleDto, Schedule>()
            .ForMember(dest => dest.Trainer, opt => opt.MapFrom(src => src.Trainer))
            .ForMember(dest => dest.Clients, opt => opt.MapFrom(src => src.Clients));
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(dest => dest.Trainer, opt => opt.MapFrom(src => src.Trainer))
            .ForMember(dest => dest.Clients, opt => opt.MapFrom(src => src.Clients))
            .ReverseMap();
        CreateMap<Trainer, ScheduleMemberDto>().ReverseMap();
        CreateMap<Client, ScheduleMemberDto>().ReverseMap();
    }
}
