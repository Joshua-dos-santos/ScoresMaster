using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.Leagues;

public class League
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public string? LogoUrl { get; set; }
}
