namespace ScoresMasterApi_Football.Teams;

public interface ITeamsService
{
    Task<List<Team>> GetTeams();
    Task<Team> PostTeams(Team team);
}
