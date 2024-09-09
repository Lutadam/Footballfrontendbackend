using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Footballapi.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int PlayerAge { get; set; }
        public string PlayerNationality { get; set; } = string.Empty;
        public int PlayerShirtNumber { get; set; }
        public int PlayerValue { get; set; }
    }

    public class PlayerWithTeamId : Player
    {
        public int TeamId { get; set; }
    }

    public class PlayerWithTeam : Player
    { 
        public required Team Team {  get; set; }    
    }
}