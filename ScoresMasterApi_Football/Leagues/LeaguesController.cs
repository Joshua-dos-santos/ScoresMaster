using Microsoft.AspNetCore.Mvc;

namespace ScoresMasterApi_Football.Leagues;

[ApiController]
[Route("api/[controller]")]
public class LeaguesController(ILeaguesService leaguesService) : ControllerBase
{
    private readonly ILeaguesService _leaguesService = leaguesService;

    // GET: api/teams
    [HttpGet]
    public async Task<IActionResult> GetLeagues()
    {
        var leagues = await _leaguesService.GetLeagues();
        return Ok(leagues.OrderBy(l => l.Id).Select(l => LeagueDto.FromLeague(l)));
    }

    // POST: api/teams
    [HttpPost]
    public async Task<IActionResult> PostLeague([FromBody] League league)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdTeam = await _leaguesService.PostLeague(league);
        return CreatedAtAction(nameof(GetLeagues), new { id = createdTeam.Id }, createdTeam);
    }
}
