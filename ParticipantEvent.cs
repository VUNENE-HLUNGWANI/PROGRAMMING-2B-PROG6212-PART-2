using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("PARTICIPANT_EVENT")]
    public class ParticipantEvent
    {
        [Key]
        public int ParticipantEventID { get; set; }

        public int EventID { get; set; }
        public int CategoryID { get; set; }
        public int ParticipantID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Bib { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegistrationStatus { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("EventID")]
        public Event? Event { get; set; }

        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }

        [ForeignKey("ParticipantID")]
        public Participant? Participant { get; set; }
    }
}