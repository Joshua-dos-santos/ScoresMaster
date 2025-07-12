namespace ScoresMasterApi_Football.Teams;

public class TeamsDto
{
    public required ICollection<Team> TeamsCollection { get; set; } = new List<Team>();

    public static TeamsDto FromTeamsCollection(TeamsCollection teams)
    {
        return new TeamsDto
        {
            TeamsCollection = teams.TeamsList.ToList()
        };
    }
}

