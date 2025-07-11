using System.Text.Json;
using ScoresMasterApi_Football.Leagues;
using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.ApiServices;

public class FootballApiService: IFootballApiService
{
    private readonly HttpClient _httpClient;

    public FootballApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com/v3/");
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("RAPIDAPI_KEY"));
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
    }

    public async Task<League> FetchLeagueWithTeamsAsync(int leagueId)
{
    var league = await FetchLeagueByIdAsync(leagueId);
    var teams = await FetchTeamsByLeagueIdAsync(leagueId);

    foreach (var team in teams)
    {
        team.LeagueId = league.Id;
        team.League = league;
    }

    league.Teams = teams;
    return league;
}

public async Task<List<Team>> FetchTeamsByLeagueIdAsync(int leagueId)
{
    var response = await _httpClient.GetAsync($"teams?league={leagueId}&season=2024");
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    var parsed = JsonDocument.Parse(json);

    var teams = new List<Team>();

    foreach (var t in parsed.RootElement.GetProperty("response").EnumerateArray())
    {
        var teamInfo = t.GetProperty("team");
        teams.Add(new Team
        {
            Id = 0,
            Name = teamInfo.GetProperty("name").GetString()!,
            LogoUrl = teamInfo.GetProperty("logo").GetString(),
            LeagueId = leagueId
        });
    }

    return teams;
}

    public async Task<League> FetchLeagueByIdAsync(int leagueId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/leagues?id={leagueId}")
        };

        request.Headers.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("RAPIDAPI_KEY"));
        request.Headers.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var parsed = JsonDocument.Parse(content);
        var leagueJson = parsed.RootElement.GetProperty("response")[0].GetProperty("league");
        var country = parsed.RootElement.GetProperty("response")[0].GetProperty("country");

        return new League
        {
            Id = leagueJson.GetProperty("id").GetInt32(),
            Name = leagueJson.GetProperty("name").GetString()!,
            Country = country.GetProperty("name").GetString()!,
            LogoUrl = leagueJson.GetProperty("logo").GetString()
        };
    }
}
