using ScoresMasterApi_Football.Leagues;

namespace ScoresMasterApi_Football.Teams;

public class TeamDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
    public required LeagueMiniDto League { get; set; }

    public static TeamDto FromTeam(Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            LogoUrl = team.LogoUrl,
            League = new LeagueMiniDto
            {
                Id = team.League.Id,
                Name = team.League.Name,
            }
        };
    }
}