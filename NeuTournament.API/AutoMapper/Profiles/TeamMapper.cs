using AutoMapper;
using NeuTournament.Application.DTO;
using NeuTournament.Domain.Entities;

namespace NeuTournament.API.AutoMapper.Profiles
{
    public class TeamMapper : Profile
    {
        public TeamMapper()
        {
            CreateMap<Team, TeamDTO>();

            CreateMap<CreateTeamDTO, Team>();

            CreateMap<Team, CreateTeamDTO>();

            CreateMap<Team, TeamUpdateDTO>();

            CreateMap<TeamUpdateDTO, Team>();
        }
    }
}
