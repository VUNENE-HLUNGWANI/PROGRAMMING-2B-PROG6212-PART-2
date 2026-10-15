using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class Participant
    {
        [Key, ForeignKey("User")]
        public int ParticipantID { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string EmergencyContactName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string EmergencyContactNumber { get; set; } = string.Empty;

        // Navigation property
        public User? User { get; set; }
    }
}