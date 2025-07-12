using ScoresMasterApi_Football.Teams;

namespace ScoresMasterApi_Football.Players;

public class Player
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }
    public required string Position { get; set; }

    public required string Nationality { get; set; }

    public required int TeamId { get; set; }
    public Team Team { get; set; } = null!;
}