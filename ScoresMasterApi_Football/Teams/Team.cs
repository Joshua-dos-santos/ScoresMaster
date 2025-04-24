using ScoresMasterApi_Football.Leagues;

namespace ScoresMasterApi_Football.Teams;

public class Team
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
    public required int LeagueId { get; set; }
    public League? League { get; set; }
}
