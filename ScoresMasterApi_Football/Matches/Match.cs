using ScoresMasterApi_Football.Teams;
using ScoresMasterApi_Football.Leagues;

namespace ScoresMasterApi_Football.Matches;

public class Match
{
    public int Id { get; set; }

    public required int LeagueId { get; set; }
    public League League { get; set; } = null!;

    public required DateTime Date { get; set; }

    public required int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public required int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public string? Status { get; set; }
}
