using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }

        public int ParticipantEventID { get; set; }

        public TimeSpan? Time { get; set; }

        public int? Position { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = string.Empty;

        public DateTime RecordedDate { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("ParticipantEventID")]
        public ParticipantEvent? ParticipantEvent { get; set; }
    }
}
