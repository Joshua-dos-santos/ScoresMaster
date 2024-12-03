using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using ScoresMasterApi.Models;

namespace ScoresMasterApi.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private static readonly List<Team> Teams = new List<Team>();

        [HttpGet("GetTeamByName")]
        public IActionResult GetTeamByName(string name)
        {
            var team = Teams.FirstOrDefault(t => t.TeamName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (team == null)
                return NotFound(new { Message = "Team not found." });

            return Ok(team);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTeamById(int id)
        {
            var team = Teams.FirstOrDefault(t => t.TeamId == id);
            if (team == null)
                return NotFound(new { Message = "Team not found." });

            return Ok(team);
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team newTeam)
        {
            if (Teams.Any(t => t.TeamId == newTeam.TeamId))
                return Conflict(new { Message = "A team with the same ID already exists." });

            Teams.Add(newTeam);
            return CreatedAtAction(nameof(GetTeamById), new { id = newTeam.TeamId }, newTeam);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTeam(int id, [FromBody] Team updatedTeam)
        {
            var team = Teams.FirstOrDefault(t => t.TeamId == id);
            if (team == null)
                return NotFound(new { Message = "Team not found." });

            team.TeamName = updatedTeam.TeamName;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTeam(int id)
        {
            var team = Teams.FirstOrDefault(t => t.TeamId == id);
            if (team == null)
                return NotFound(new { Message = "Team not found." });

            Teams.Remove(team);
            return NoContent();
        }
    }
}
