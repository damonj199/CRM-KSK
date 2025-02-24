using AutoMapper;
using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Application.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<MembershipService> _logger;

    public MembershipService(IMembershipRepository membershipRepository, IMapper mapper, ILogger<MembershipService> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _membershipRepository = membershipRepository;
    }

    public async Task AddMembershipAsync(List<MembershipDto> membershipsDto, CancellationToken token)
    {
        var membership = _mapper.Map<List<Membership>>(membershipsDto);
        await _membershipRepository.AddMembershipAsync(membership, token);
    }

    public async Task<List<MembershipDto>> GetAllMembershipClientAsync(Guid id, CancellationToken token)
    {
        var memberships = await _membershipRepository.GetAllMembershipClientAsync(id, token);
        var membershipsDtos = _mapper.Map<List<MembershipDto>>(memberships);

        return membershipsDtos ?? [];
    }

    public async Task<MembershipDto> GetMembershipByIdAsync(Guid id, CancellationToken token)
    {
        var membership = await _membershipRepository.GetMembershipByIdAsync(id, token);
        var membershipDto = _mapper.Map<MembershipDto>(membership);

        return membershipDto;
    }

    public async Task<List<MembershipDto>> GetExpiringMembershipsAsync(CancellationToken token)
    {
        var memberships = await _membershipRepository.GetExpiringMembershipAsync(token);
        var membershipsDto = _mapper.Map<List<MembershipDto>>(memberships);

        return membershipsDto ?? [];
    }

    public async Task UpdateMembershipAsync(MembershipDto membershipDto, CancellationToken token)
    {
        var membership = _mapper.Map<Membership>(membershipDto);
        await _membershipRepository.UpdateMembershipAsync(membership, token);
    }

    public async Task DeleteMembershipAsync(Guid id, CancellationToken token)
    {
        await _membershipRepository.DeleteMembershipAsync(id, token);
    }
}
