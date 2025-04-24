namespace ScoresMasterApi_Football.Teams;

public class Team
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
}
