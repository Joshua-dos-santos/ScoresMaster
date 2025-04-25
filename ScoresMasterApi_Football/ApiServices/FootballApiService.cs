using System.Text.Json;
using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.ApiServices;

public class FootballApiService: IFootballApiService
{
    private readonly HttpClient _httpClient;

    public FootballApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com/v3/");
        var apiKey = Environment.GetEnvironmentVariable("RAPIDAPI_KEY");
        Console.WriteLine($"API key gevonden: {(string.IsNullOrWhiteSpace(apiKey) ? "NIET gevonden" : "✔️ aanwezig")}");

        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("RAPIDAPI_KEY"));
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
    }

    public async Task<List<Team>> FetchEredivisieTeamsAsync(int localLeagueId)
    {
        var response = await _httpClient.GetAsync("teams?league=88&season=2024");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var parsed = JsonDocument.Parse(json);
        var teams = new List<Team>();

        foreach (var t in parsed.RootElement.GetProperty("response").EnumerateArray())
        {
            var teamInfo = t.GetProperty("team");
            teams.Add(new Team
            {
                Id = 0, // wordt gegenereerd door DB
                Name = teamInfo.GetProperty("name").GetString(),
                LogoUrl = teamInfo.GetProperty("logo").GetString(),
                LeagueId = localLeagueId
            });
        }

        return teams;
    }
}
