using Microsoft.AspNetCore.Mvc;
using NeuTournament.Application.DTO;
using NeuTournament.Application.Services.Interface;

namespace NeuTournament.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService teamMemberService;
        public TeamMembersController(ITeamMemberService teamMemberService)
        {
            this.teamMemberService = teamMemberService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeamMembers(int pagesize = 20, int currentPage = 1)
        {
            if (currentPage < 1) throw new ArgumentOutOfRangeException(nameof(currentPage));
            if (pagesize < 0) throw new ArgumentOutOfRangeException(nameof(pagesize));
            var teamMembers = await teamMemberService.GetAllTeamMembers(pagesize, currentPage);
            return Ok(teamMembers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMemberById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var result = await teamMemberService.GetTeamMemberById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeamMember(CreateTeamMemberDTO teamMember)
        {
            ArgumentNullException.ThrowIfNull(nameof(teamMember), "Cannot pass null value");
            var memberData = await teamMemberService.AddTeamMember(teamMember);
            return StatusCode(201, memberData);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTeamMember(TeamMemberDTO teamMember)
        {
            ArgumentNullException.ThrowIfNull(nameof(teamMember), "Cannot pass null value");
            var result = await teamMemberService.UpdateTeamMember(teamMember);
            return StatusCode(200, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid Id : {id}");
            }
            var response = await teamMemberService.DeleteTeamMember(id);
            return StatusCode(200, response);
        }
    }
}

