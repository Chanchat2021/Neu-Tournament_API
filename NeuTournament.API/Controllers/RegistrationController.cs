using Microsoft.AspNetCore.Mvc;
using NeuTournament.Application.DTO;
using NeuTournament.Application.Services.Interface;
using NeuTournament.Domain.Entities;

namespace NeuTournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegistrations(int pagesize = 20, int currentPage = 1)
        {
            if (currentPage < 1) throw new ArgumentOutOfRangeException(nameof(currentPage));
            if (pagesize < 0) throw new ArgumentOutOfRangeException(nameof(pagesize));

            var registration = await registrationService.GetAllRegistrations(pagesize, currentPage);
            return Ok(registration);
        }

        [HttpGet("event/{eventId}/user/{emailId}")]
        public async Task<bool> CheckUserRegistration(int eventId, string emailId)
        {
            ArgumentNullException.ThrowIfNull(nameof(emailId));
            if (eventId < 1)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {eventId}");
            }
            var result = await registrationService.CheckUserRegistration(eventId, emailId);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistrationById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var result = await registrationService.GetRegistrationtbyId(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegistration(CreateRegistrationDTO registration)
        {
            ArgumentNullException.ThrowIfNull(nameof(registration), "Cannot pass null value");
            var result = await registrationService.CreateRegistration(registration);
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRegistration(RegistrationDTO registrationDto)
        {
            ArgumentNullException.ThrowIfNull(nameof(registrationDto), "Cannot pass null value");
            var result = await registrationService.UpdateRegistration(registrationDto);
            return StatusCode(200, result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var response = await registrationService.DeleteRegistration(id);
            return StatusCode(200, response);
        }

    }
}
