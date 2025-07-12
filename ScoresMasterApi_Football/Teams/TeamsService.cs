using Microsoft.EntityFrameworkCore;

namespace ScoresMasterApi_Football.Teams;

public class TeamsService(ScoresMasterDbContext _context) : ITeamsService
{

    public async Task<List<Team>> GetTeams()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<Team> GetTeamById(int id)
    {
        var team = await _context.Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == id);
        return team ?? throw new KeyNotFoundException($"Team with ID {id} not found.");
    }
}
