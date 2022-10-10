using Microsoft.AspNetCore.Mvc;
using NeuTournament.Application.Services;
using NeuTournament.Application.DTO;

namespace NeuTournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService eventService;
        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        [Route("Upcoming")]
        [HttpGet]
        public async Task<IActionResult> UpcomingEvents()
        {
            var events = await eventService.GetAllUpcomingEvents();
            return Ok(events);
        }
        [Route("historical")]
        [HttpGet]
        public async Task<IActionResult> historicalEvents()
        {
            var events = await eventService.GetAllHistoricalEvents();
            return Ok(events);
        }
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await eventService.GetEvents();
            return Ok(events);
        }
        [HttpGet("disabled")]
        public async Task<IActionResult> GetDisabledEvents()
        {
            var events = await eventService.GetDisabledEvents();
            return Ok(events);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var result = await eventService.GetEventById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvents(CreateEventDTO createEvent)
        {
            ArgumentNullException.ThrowIfNull(nameof(createEvent));
            if (createEvent.StartDate > createEvent.EndDate)
                return BadRequest("Start Date cannot be greater than End Date");
            else
            {
                var result = await eventService.CreateEvent(createEvent);
                return StatusCode(201, result);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventUpdateDTO eventDto)
        {
            ArgumentNullException.ThrowIfNull(nameof(eventDto));
            if (eventDto.StartDate > eventDto.EndDate)
            {
                return BadRequest("Start Date cannot be greater than End Date");
            }
            var result = await eventService.UpdateEvent(eventDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var response = await eventService.DeleteEvent(id);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableEvent(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var response = await eventService.DisableEvent(id);
            return Ok(response);
        }
        [Route("Banner")]

        [HttpGet]
        public async Task<IActionResult> GetBannerByID(int id)
        { 
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var result = await eventService.GetBannerById(id);
            return Ok(result);
        }
    }
}
