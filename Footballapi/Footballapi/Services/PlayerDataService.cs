using Footballapi.Data;
using Footballapi.Models;
using Microsoft.EntityFrameworkCore;

namespace Footballapi.Services
{
    public class PlayerDataService
    {
        private readonly ApplicationDbContext _context;
        public PlayerDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PlayerWithTeamId>> GetPlayersWithTeamIdsAsync()
        {
            var players = await _context.Player.ToListAsync();
            return  players;
        }
        public async Task<List<PlayerWithTeam>> GetPlayerAsync()
        {
            var teams = await _context.Team.ToListAsync();
            var players = await _context.Player.ToListAsync();
            return players.Select(p => new PlayerWithTeam
            {
                PlayerId = p.PlayerId,
                PlayerName = p.PlayerName,
                PlayerAge = p.PlayerAge,
                PlayerNationality = p.PlayerNationality,
                PlayerShirtNumber = p.PlayerShirtNumber,
                PlayerValue = p.PlayerValue,
                Team = teams.FirstOrDefault(t => t.TeamId == p.TeamId)
            }).ToList();
        }

        public async Task<IEnumerable<Player>> PostPlayersAsync(PlayerWithTeamId player)
        {
            _context.Player.Add(player);
            await _context.SaveChangesAsync();
            return await _context.Player.ToListAsync();
        }

        public async Task<IEnumerable<Player>> PutPlayersAsync(PlayerWithTeamId player)
        {
            _context.Player.Update(player);
            await _context.SaveChangesAsync();
            return await _context.Player.ToListAsync();
        }

        public async Task<IEnumerable<Player>> DeletePlayerAsync(PlayerWithTeamId player)
        {
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return await _context.Player.ToListAsync();
        }
    }
}

