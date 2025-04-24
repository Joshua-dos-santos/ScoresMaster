using Microsoft.AspNetCore.Mvc;

namespace ScoresMasterApi_Football.Teams;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamsService;

    public TeamsController(ITeamsService teamsService)
    {
        _teamsService = teamsService;
    }

    // GET: api/teams
    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _teamsService.GetTeams();
        return Ok(teams);
    }

    // POST: api/teams
    [HttpPost]
    public async Task<IActionResult> PostTeam([FromBody] Team team)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdTeam = await _teamsService.PostTeams(team);
        return CreatedAtAction(nameof(GetTeams), new { id = createdTeam.Id }, createdTeam);
    }
}
