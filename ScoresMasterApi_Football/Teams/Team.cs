using ScoresMasterApi_Football.Leagues;
using ScoresMasterApi_Football.Players;

namespace ScoresMasterApi_Football.Teams;

public class Team
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
    public required int LeagueId { get; set; }
    public League League { get; set; } = null!;

    public ICollection<Player> Players { get; set; } = new List<Player>();
}
