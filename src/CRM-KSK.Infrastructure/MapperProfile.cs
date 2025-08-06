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
            .ForMember(dest => dest.Memberships, opt => opt.Ignore());
        CreateMap<TrainerDto, Trainer>().ReverseMap();
        CreateMap<Training, TrainingDto>()
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer))
            .ForMember(dest => dest.ClientsName, opt => opt.MapFrom(src => src.Clients))
            .ReverseMap();
        CreateMap<Trainer, ScheduleMemberDto>().ReverseMap();
        CreateMap<Client, ScheduleMemberDto>().ReverseMap();
        CreateMap<Schedule, ScheduleDto>().ReverseMap();
        CreateMap<ScheduleFullDto, Training>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TrainingId))
            .ForMember(dest => dest.Trainer, opt => opt.MapFrom(src => src.TrainerName))
            .ForMember(dest => dest.Clients, opt => opt.MapFrom(src => src.ClientsName));
        CreateMap<ScheduleFullDto, Schedule>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ScheduleId));
        CreateMap<BirthdayNotification, BirthdayDto>().ReverseMap();
        CreateMap<Membership, MembershipDto>().ReverseMap();
        CreateMap<ScheduleComment, ScheduleCommentDto>().ReverseMap();
        CreateMap<HorseWork, WorkHorseDto>().ReverseMap();
        CreateMap<Horse, HorseDto>().ReverseMap();
    }
}
