using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.Leagues;

public class LeagueDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public string? LogoUrl { get; set; }
    public required List<TeamDto> Teams { get; set; }

    public static LeagueDto FromLeague(League league)
    {
        return new LeagueDto
        {
            Id = league.Id,
            Name = league.Name,
            Country = league.Country,
            LogoUrl = league.LogoUrl,
            Teams = league.Teams.OrderBy(t => t.Id).Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                LogoUrl = t.LogoUrl,
                League = new LeagueMiniDto
                {
                    Id = t.League.Id,
                    Name = t.League.Name,
                }
            }).ToList()
        };
    }
}