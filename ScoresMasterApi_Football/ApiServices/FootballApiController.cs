using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ScoresMasterApi_Football.ApiServices;

[ApiController]
[Route("api/[controller]")]
public class FootballApiController(IFootballApiService _footballService, ScoresMasterDbContext _context) : ControllerBase
{
    [HttpPost("import/league-with-teams")]
public async Task<IActionResult> ImportLeagueWithTeams([FromBody] int leagueId)
{
    var existingLeague = await _context.Leagues
        .Include(l => l.Teams)
        .FirstOrDefaultAsync(l => l.Id == leagueId);

    if (existingLeague != null)
    {
        return Conflict("League already exists in the database.");
    }

    var league = await _footballService.FetchLeagueWithTeamsAsync(leagueId);

    foreach (var team in league.Teams.ToList())
    {
        if (_context.Teams.Any(t => t.Name == team.Name && t.LeagueId == leagueId))
        {
            league.Teams.Remove(team);
        }
    }

    _context.Leagues.Add(league);
    await _context.SaveChangesAsync();

    return Ok(new
    {
        message = $"League '{league.Name}' and {league.Teams.Count} teams imported successfully.",
        league.Id,
        league.Name,
        league.Country
    });
}


    [HttpPost("import/league/{id}")]
    public async Task<IActionResult> ImportLeague(int id)
    {
        try
        {
            var league = await _footballService.FetchLeagueByIdAsync(id);

            var exists = await _context.Leagues.AnyAsync(l => l.Id == league.Id);
            if (exists) return Conflict("League already exists.");

            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();

            return Ok(league);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"⚠️ Fout bij importeren van league: {ex.Message}");
        }
    }

}
