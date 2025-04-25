using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ScoresMasterApi_Football.ApiServices;

[ApiController]
[Route("api/[controller]")]
public class FootballApiController(IFootballApiService _footballService, ScoresMasterDbContext _context) : ControllerBase
{
    [HttpPost("import/eredivisie-teams")]
    public async Task<IActionResult> ImportEredivisieTeams()
    {
        // Zoek de Eredivisie league in je eigen DB
        var eredivisie = await _context.Leagues.FirstOrDefaultAsync(l => l.Name == "Eredivisie");

        if (eredivisie == null)
            return NotFound("Eredivisie league not found in local database.");

        // Haal teams op van de API
        var teams = await _footballService.FetchEredivisieTeamsAsync(88);

        // Optioneel: check op dubbele teams
        foreach (var team in teams)
        {
            if (!_context.Teams.Any(t => t.Name == team.Name && t.LeagueId == 88))
            {
                _context.Teams.Add(team);
            }
        }

        await _context.SaveChangesAsync();
        return Ok(new { added = teams.Count });
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
