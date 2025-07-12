using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ScoresMasterApi_Football.Leagues;
using ScoresMasterApi_Football.Matches;
using ScoresMasterApi_Football.Players;
using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.ApiServices;

public class FootballApiService : IFootballApiService
{
    private readonly HttpClient _httpClient;
    private readonly ScoresMasterDbContext _context;

    public FootballApiService(HttpClient httpClient, IConfiguration config, ScoresMasterDbContext context)
    {
        _httpClient = httpClient;
        _context = context;

        _httpClient.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com/v3/");
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("RAPIDAPI_KEY"));
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
    }

    public async Task<League> FetchLeagueWithTeamsAsync(int leagueId)
    {
        var league = await FetchLeagueByIdAsync(leagueId);
        var teams = await FetchTeamsByLeagueIdAsync(leagueId);
        var matches = await FetchMatchesByLeagueIdAsync(leagueId);

        foreach (var team in teams)
        {
            team.LeagueId = league.Id;
            team.League = league;
        }

        foreach (var match in matches)
        {
            match.League = league;
        }

        league.Teams = teams;
        league.Matches = matches;

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
            var teamId = teamInfo.GetProperty("id").GetInt32();

            var team = new Team
            {
                Id = teamId,
                Name = teamInfo.GetProperty("name").GetString()!,
                LogoUrl = teamInfo.GetProperty("logo").GetString(),
                LeagueId = leagueId
            };

            var players = await FetchPlayersByTeamIdAsync(teamId);
            foreach (var player in players)
            {
                player.Team = team;
            }

            team.Players = players;
            teams.Add(team);
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

    public async Task<List<Match>> FetchMatchesByLeagueIdAsync(int leagueId)
    {
        var response = await _httpClient.GetAsync($"fixtures?league={leagueId}&season=2024");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var parsed = JsonDocument.Parse(json);

        var matches = new List<Match>();

        foreach (var item in parsed.RootElement.GetProperty("response").EnumerateArray())
        {
            var fixture = item.GetProperty("fixture");
            var teams = item.GetProperty("teams");
            var goals = item.GetProperty("goals");

            matches.Add(new Match
            {
                Id = 0,
                LeagueId = leagueId,
                Date = fixture.GetProperty("date").GetDateTime(),

                HomeTeamId = teams.GetProperty("home").GetProperty("id").GetInt32(),
                AwayTeamId = teams.GetProperty("away").GetProperty("id").GetInt32(),

                HomeScore = goals.GetProperty("home").ValueKind == JsonValueKind.Null ? null : goals.GetProperty("home").GetInt32(),
                AwayScore = goals.GetProperty("away").ValueKind == JsonValueKind.Null ? null : goals.GetProperty("away").GetInt32(),
                Status = fixture.GetProperty("status").GetProperty("short").GetString()
            });
        }

        return matches;
    }

    public async Task<List<Player>> FetchPlayersByTeamIdAsync(int teamId)
    {
        var response = await _httpClient.GetAsync($"players?team={teamId}&season=2024");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var parsed = JsonDocument.Parse(json);

        var players = new List<Player>();

        foreach (var item in parsed.RootElement.GetProperty("response").EnumerateArray())
        {
            var playerJson = item.GetProperty("player");
            var statisticsJson = item.GetProperty("statistics")[0];

            var name = playerJson.GetProperty("name").GetString()!;
            var age = playerJson.GetProperty("age").GetInt32();
            var position = statisticsJson.GetProperty("games").GetProperty("position").GetString()!;
            var nationality = playerJson.GetProperty("nationality").GetString() ?? "Unknown";

            players.Add(new Player
            {
                Id = 0,
                Name = name,
                Age = age,
                Position = position,
                Nationality = nationality,
                TeamId = teamId
            });
        }

        return players;
    }
}
