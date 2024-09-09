using Microsoft.EntityFrameworkCore;
using Footballapi.Models;

namespace Footballapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for your entities
        public DbSet<Team> Team { get; set; }
        public DbSet<PlayerWithTeamId> Player { get; set; }

        internal Task UpdateTeamAsync(Team team)
        {
            throw new NotImplementedException();
        }
    }
}
