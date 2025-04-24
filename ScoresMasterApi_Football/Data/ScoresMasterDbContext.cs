using Microsoft.EntityFrameworkCore;
using ScoresMasterApi_Football.Teams;

public class ScoresMasterDbContext : DbContext
{
    public ScoresMasterDbContext(DbContextOptions<ScoresMasterDbContext> options)
        : base(options) { }

    public DbSet<Team> Teams { get; set; }
    // Add more: Matches, Players, Standings, etc.
}
