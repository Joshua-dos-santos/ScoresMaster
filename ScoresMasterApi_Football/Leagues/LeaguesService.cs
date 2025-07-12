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

    public async Task<League> GetLeagueById(int id)
    {
        var league = await _context.Leagues
            .Include(l => l.Teams)
            .Include(l => l.Matches)
            .FirstOrDefaultAsync(l => l.Id == id);
        return league ?? throw new KeyNotFoundException($"League with ID {id} not found.");
    }

    public async Task<TeamsCollection> GetTeamsByLeagueId(int leagueId)
    {
        var league = await _context.Leagues
            .Include(l => l.Teams)
            .FirstOrDefaultAsync(l => l.Id == leagueId);

        if (league == null)
        {
            throw new KeyNotFoundException($"League with ID {leagueId} not found.");
        }

        var teams = new TeamsCollection
        {
            TeamsList = league.Teams.OrderBy(t => t.Id).ToList()
        };

        return teams;
    }

    public async Task<League> PostLeague(League league)
    {
        _context.Leagues.Add(league);
        await _context.SaveChangesAsync();
        return league;
    }

    public async Task<League> PutLeague(int id, League league)
    {
        if (id != league.Id)
        {
            throw new ArgumentException("ID mismatch");
        }

        _context.Entry(league).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return league;
    }

    public async Task<bool> DeleteLeagueWithTeamsAsync(int leagueId)
    {
        var league = await _context.Leagues
            .Include(l => l.Matches)
            .Include(l => l.Teams)
                .ThenInclude(t => t.Players)
            .FirstOrDefaultAsync(l => l.Id == leagueId);


        if (league == null) return false;

        _context.Matches.RemoveRange(league.Matches);
        foreach (var team in league.Teams)
        {
            _context.Players.RemoveRange(team.Players);
        }
        _context.Teams.RemoveRange(league.Teams);
        _context.Leagues.Remove(league);
        await _context.SaveChangesAsync();

        return true;
    }
}
