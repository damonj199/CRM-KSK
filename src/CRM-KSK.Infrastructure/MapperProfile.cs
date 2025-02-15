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
            .ForMember(dest => dest.Membership, opt => opt.Ignore());
        CreateMap<TrainerDto, Trainer>().ReverseMap();
        CreateMap<Training, TrainingDto>()
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer))
            .ForMember(dest => dest.ClientsName, opt => opt.MapFrom(src => src.Clients))
            .ReverseMap();
        CreateMap<Trainer, ScheduleMemberDto>().ReverseMap();
        CreateMap<Client, ScheduleMemberDto>().ReverseMap();
        CreateMap<Schedule, ScheduleDto>().ReverseMap();
    }
}
