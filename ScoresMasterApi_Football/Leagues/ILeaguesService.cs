namespace ScoresMasterApi_Football.Leagues;

public interface ILeaguesService
{
    Task<List<League>> GetLeagues();
    Task<League> PostLeague(League team);
}
