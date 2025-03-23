
using Microsoft.AspNetCore.Mvc;

namespace ScoresMasters_FootballApi.Teams;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTeams()
    {
        return Ok("Teams");
    }
}