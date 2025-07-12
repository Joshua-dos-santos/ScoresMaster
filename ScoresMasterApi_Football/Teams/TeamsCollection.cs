namespace ScoresMasterApi_Football.Teams;

public class TeamsCollection
{
    public required IEnumerable<Team> TeamsList { get; set; } = new List<Team>();
}