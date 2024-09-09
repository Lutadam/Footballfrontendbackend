using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Footballapi.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string TeamCity { get; set; } = string.Empty;
        public string TeamStadium { get; set; } = string.Empty;
        public string TeamDescription { get; set; } = string.Empty;
        public string? TeamLogo {  get; set; }
    }
}
