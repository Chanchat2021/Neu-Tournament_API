using Microsoft.AspNetCore.Mvc;
using NeuTournament.Application.DTO;
using NeuTournament.Application.Services.Interface;

namespace NeuTournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService teamService;
        public TeamsController(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeams(int pagesize = 20, int currentPage = 1)
        {
            if (currentPage < 1) throw new ArgumentOutOfRangeException(nameof(currentPage));
            if (pagesize < 0) throw new ArgumentOutOfRangeException(nameof(pagesize));
            var teams = await teamService.GetAllTeams(pagesize, currentPage);
            return Ok(teams);
        }
        [HttpGet("event/{eventId}")]
        public IActionResult GetTeamsByEventId(int eventId)
        {
            ArgumentNullException.ThrowIfNull(nameof(eventId));
            var result = teamService.GetTeamsByEventId(eventId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamDTO team)
        {
            ArgumentNullException.ThrowIfNull(nameof(team));
            var result = await teamService.CreateTeam(team);
            return StatusCode(201, result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTeam(TeamUpdateDTO team)
        {
            ArgumentNullException.ThrowIfNull(nameof(team));
            var result = await teamService.UpdateTeam(team);
            return StatusCode(200, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var response = await teamService.DeleteTeam(id);
            return StatusCode(200, response);
        }
    }

}

