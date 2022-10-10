using AutoMapper;
using NeuTournament.Application.DTO;
using NeuTournament.Domain.Entities;

namespace NeuTournament.API.AutoMapper.Profiles
{
    public class TeamMemberMapper : Profile
    {
        public TeamMemberMapper()
        {
            CreateMap<TeamMember, TeamMemberDTO>();

            CreateMap<TeamMemberDTO, TeamMember>();

            CreateMap<TeamMember, CreateTeamMemberDTO>();

            CreateMap<CreateTeamMemberDTO, TeamMember>();
        }
    }
}
