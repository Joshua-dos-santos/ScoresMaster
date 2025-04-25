using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.Leagues;

public class LeagueDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public string? LogoUrl { get; set; }

    public static LeagueDto FromLeague(League league)
    {
        return new LeagueDto
        {
            Id = league.Id,
            Name = league.Name,
            Country = league.Country,
            LogoUrl = league.LogoUrl,
        };
    }
}