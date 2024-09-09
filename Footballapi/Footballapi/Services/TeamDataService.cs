using Footballapi.Models;
using Microsoft.EntityFrameworkCore;
using Footballapi.Data;

namespace Footballapi.Services
{
    public class TeamDataService
    {
        private readonly ApplicationDbContext _context;

        public TeamDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return await _context.Team.ToListAsync();
        }

        public async Task<List<Team>> GetTeamsByIdAsync(int id) 
        {
            return await _context.Team.ToListAsync();
        }

        public async Task<IEnumerable<Team>> PostTeamsAsync(Team team)
        {
            _context.Team.Add(team);
            await _context.SaveChangesAsync();
            return await _context.Team.ToListAsync();
        }
        public async Task<IEnumerable<Team>> PutTeamAsync(Team team)
        {
            _context.Team.Update(team);
            await _context.SaveChangesAsync();
            return await _context.Team.ToListAsync();

        }

        public async Task<IEnumerable<Team>> DeleteTeamAsync(Team team)
        {
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return await _context.Team.ToListAsync();
        }
    }
}
