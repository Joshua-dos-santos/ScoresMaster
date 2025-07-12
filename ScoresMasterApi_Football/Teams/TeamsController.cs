using Microsoft.AspNetCore.Mvc;

namespace ScoresMasterApi_Football.Teams;

[ApiController]
[Route("api/[controller]")]
public class TeamsController(ITeamsService teamsService) : ControllerBase
{
    private readonly ITeamsService _teamsService = teamsService;

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _teamsService.GetTeams();
        return Ok(teams.Select(TeamDto.FromTeam));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid team ID.");
        }
        var team = await _teamsService.GetTeamById(id);
        return Ok(TeamDto.FromTeam(team));
    }
}
