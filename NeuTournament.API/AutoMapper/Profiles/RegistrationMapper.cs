using AutoMapper;
using NeuTournament.Application.DTO;
using NeuTournament.Domain.Entities;

namespace NeuTournament.API.AutoMapper.Profiles
{
    public class RegistrationMapper : Profile
    {
        public RegistrationMapper()
        {
            CreateMap<Registration, CreateRegistrationDTO>();

            CreateMap<CreateRegistrationDTO, Registration>();

            CreateMap<Registration, RegistrationDTO>();

            CreateMap<RegistrationDTO, Registration>();
        }
    }
}
