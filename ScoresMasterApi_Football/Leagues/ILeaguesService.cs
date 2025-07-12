using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.Leagues;

public interface ILeaguesService
{
    Task<List<League>> GetLeagues();
    Task<League> GetLeagueById(int id);
    Task<List<Team>> GetTeamsByLeagueId(int leagueId);
    Task<League> PostLeague(League team);
    Task<League> PutLeague(int id, League league);
    Task<bool> DeleteLeagueWithTeamsAsync(int leagueId);
}
