using Microsoft.EntityFrameworkCore;
using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.Leagues;

public class LeaguesService(ScoresMasterDbContext _context) : ILeaguesService
{

    public async Task<List<League>> GetLeagues()
    {
        var leagues = await _context.Leagues.ToListAsync();

        return leagues;
    }

    public async Task<League> PostLeague(League league)
    {
        _context.Leagues.Add(league);
        await _context.SaveChangesAsync();
        return league;
    }
}
