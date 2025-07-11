using ScoresMasterApi_Football.Leagues;
using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.ApiServices;

public interface IFootballApiService
{
    Task<League> FetchLeagueWithTeamsAsync(int leagueId);
    Task<List<Team>> FetchTeamsByLeagueIdAsync(int leagueId);
    Task<League> FetchLeagueByIdAsync(int leagueId);
}
