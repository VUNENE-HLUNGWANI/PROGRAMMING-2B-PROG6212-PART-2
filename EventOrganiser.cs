using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class EventOrganiser
    {
        [Key, ForeignKey("User")]
        public int EventOrganiserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string OrganisationName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string OrganisationContact { get; set; } = string.Empty;

        // Navigation property
        public User? User { get; set; }
    }
}