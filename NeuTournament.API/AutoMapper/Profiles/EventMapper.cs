using AutoMapper;
using NeuTournament.Application.DTO;
using NeuTournament.Domain.Entities;

namespace NeuTournament.API.AutoMapper.Profiles
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            CreateMap<Event, EventsUpcomingDTO>();
 
            CreateMap<Event, EventsDTO>();

            CreateMap<Event, EventDTO>();

            CreateMap<CreateEventDTO, Event>();

            CreateMap<Event, CreateEventDTO>();

            CreateMap<Event, EventsDTO>();

            CreateMap<Event, EventUpdateDTO>();

            CreateMap<EventUpdateDTO, Event>();
        }
    }
}
