using Microsoft.EntityFrameworkCore;

namespace ScoresMasterApi_Football.Leagues;

public class LeaguesService(ScoresMasterDbContext _context) : ILeaguesService
{

    public async Task<List<League>> GetLeagues()
    {
        return await _context.Leagues.ToListAsync();
    }

    public async Task<League> PostLeague(League league)
    {
        _context.Leagues.Add(league);
        await _context.SaveChangesAsync();
        return league;
    }
}
