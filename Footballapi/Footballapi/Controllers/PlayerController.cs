using Footballapi.Models;
using Footballapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Footballapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController(PlayerDataService playerDataService, ILogger<PlayerController> logger) : ControllerBase
    {
        private readonly PlayerDataService _playerDataService = playerDataService;
        private readonly ILogger<PlayerController> _logger = logger;

        [HttpGet("GetPlayers")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersAsync()
        {
            _logger.LogInformation("Fetching all players.");

            var entities = await _playerDataService.GetPlayerAsync();

            return Ok(entities);
        }
        
        [HttpPost("Saveplayer")]
        public async Task<ActionResult<IEnumerable<Player>>> PostPlayersAsync(PlayerWithTeamId player)
        {
            // Check if player is null, if yes return bad request
            if (player == null)
            {
                _logger.LogError("player is null and returns a bad request.");

                return BadRequest("player is null.");
            }

            // Create a new player and save to DB
            _logger.LogInformation("Creating new player with provided data.");

            var entities = await _playerDataService.PostPlayersAsync(player);

            _logger.LogInformation("Player has been created successfully.");

            return Ok(entities);
        }

        [HttpPut("UpdatePlayer")]
        public async Task<ActionResult<Player>> PutPlayerAsync(PlayerWithTeamId updatedPlayer, PlayerDataService _playerDataService)
        {
            // Retrieve the existing team
            var existingPlayers = await _playerDataService.GetPlayersWithTeamIdsAsync();
            var existingPlayer = existingPlayers.Find(player => player.PlayerId == updatedPlayer.PlayerId);
            {
                if (existingPlayer == null)
                {
                    _logger.LogError("Requested player does not exist.");
                    return NotFound();
                }

                existingPlayer.PlayerName = updatedPlayer.PlayerName;
                existingPlayer.PlayerAge = updatedPlayer.PlayerAge;
                existingPlayer.PlayerNationality = updatedPlayer.PlayerNationality;
                existingPlayer.PlayerShirtNumber = updatedPlayer.PlayerShirtNumber;
                existingPlayer.PlayerValue = updatedPlayer.PlayerValue;
                existingPlayer.TeamId = updatedPlayer.TeamId;


                // Save existing team with provided data
                _logger.LogInformation("Updating existing player with provided data.");
                await _playerDataService.PutPlayersAsync(existingPlayer);

                _logger.LogInformation("Player has been updated successfully.");

                return Ok(existingPlayer);
            }

        }

        [HttpDelete("DeletePlayer")]
        public async Task<IActionResult> DeletePlayerAsync(int PlayerId)
        {
            var existingTeams = await _playerDataService.GetPlayersWithTeamIdsAsync();
            var playerToDelete = existingTeams.Find(player => player.PlayerId == PlayerId);

            if (playerToDelete == null)
            {
                _logger.LogError("Requested player does not exist.");
                return NotFound();
            }

            _logger.LogInformation("Deleting existing player with provided team Id.");
            await _playerDataService.DeletePlayerAsync(playerToDelete);

            _logger.LogInformation("Player has been deleted successfully.");

            return NoContent();
        }
    }
}
