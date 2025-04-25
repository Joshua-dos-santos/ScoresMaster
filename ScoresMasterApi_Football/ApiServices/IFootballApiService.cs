using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.ApiServices;

public interface IFootballApiService
{
    Task<List<Team>> FetchEredivisieTeamsAsync(int localLeagueId);
}
