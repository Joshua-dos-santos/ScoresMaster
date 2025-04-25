using ScoresMasterApi_Football.Leagues;

namespace ScoresMasterApi_Football.Teams;

public class TeamDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
    public required int LeagueId { get; set; }

    public static TeamDto FromTeam(Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            LogoUrl = team.LogoUrl,
            LeagueId = team.LeagueId
        };
    }
}