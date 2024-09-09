using Footballapi.Models;
using Footballapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Footballapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamDataService _teamDataService;
        private readonly ILogger<TeamsController> _logger;
        public TeamsController(TeamDataService teamDataService, ILogger<TeamsController> logger)
        {
            _teamDataService = teamDataService;
            _logger = logger;
        }

        [HttpGet("GetTeams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsAsync()
        {
            _logger.LogInformation("Fetching all teams.");

            var entities = await _teamDataService.GetTeamsAsync();
            
            return Ok(entities);
        }

        [HttpPost("SaveTeam")]
        public async Task<ActionResult<IEnumerable<Team>>> PostTeamsAsync(Team team)
        {
            // Check if team is null, if yes return bad request
            if (team == null)
            {
                _logger.LogError("Team is null and returns a bad request.");

                return BadRequest("Team is null.");
            }

            // Create a new team and save to DB
            _logger.LogInformation("Creating new team with provided data.");

            var entities = await _teamDataService.PostTeamsAsync(team);

            _logger.LogInformation("Team has been created successfully.");

            return Ok(entities);
        }

        [HttpPut("UpdateTeam")]
        public async Task<ActionResult<Team>> PutTeamAsync(Team updatedTeam, TeamDataService _teamDataService)
        {
            // Retrieve the existing team
            var existingTeams = await _teamDataService.GetTeamsAsync();
            var existingTeam = existingTeams.Find(team => team.TeamId == updatedTeam.TeamId);
            {
                if (existingTeam == null)
                {
                    _logger.LogError("Requested team does not exist.");
                    return NotFound();
                }

                existingTeam.TeamName = updatedTeam.TeamName;
                existingTeam.TeamCity = updatedTeam.TeamCity;
                existingTeam.TeamStadium = updatedTeam.TeamStadium;
                existingTeam.TeamDescription = updatedTeam.TeamDescription;
                existingTeam.TeamLogo = updatedTeam.TeamLogo;

                // Save existing team with provided data
                _logger.LogInformation("Updating existing team with provided data.");
                await _teamDataService.PutTeamAsync(existingTeam);

                _logger.LogInformation("Team has been updated successfully.");

                return Ok(existingTeams);
            }

        }

        [HttpDelete("DeleteTeam")]
        public async Task<IActionResult> DeleteTeamAsync(int teamId)
        {
            var existingTeams = await _teamDataService.GetTeamsAsync();
            var teamToDelete = existingTeams.Find(team => team.TeamId == teamId);

            if (teamToDelete == null)
            {
                _logger.LogError("Requested team does not exist.");
                return NotFound();
            }

            _logger.LogInformation("Deleting existing team with provided team Id.");

            try
            {
                await _teamDataService.DeleteTeamAsync(teamToDelete);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _logger.LogInformation("Team has been deleted successfully.");

            return NoContent();
        }
    }
}


