using Microsoft.EntityFrameworkCore;
using ScoresMasterApi_Football.Teams;
using ScoresMasterApi_Football.Leagues;

public class ScoresMasterDbContext : DbContext
{
    public ScoresMasterDbContext(DbContextOptions<ScoresMasterDbContext> options)
        : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
