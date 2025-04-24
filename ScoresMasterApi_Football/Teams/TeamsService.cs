using Microsoft.EntityFrameworkCore;

namespace ScoresMasterApi_Football.Teams;

public class TeamsService(ScoresMasterDbContext _context) : ITeamsService
{

    public async Task<List<Team>> GetTeams()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<Team> PostTeams(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }
}
