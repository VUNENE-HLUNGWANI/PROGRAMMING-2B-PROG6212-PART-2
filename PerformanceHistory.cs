using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class PerformanceHistory
    {
        [Key]
        public int PerformanceHistoryID { get; set; }

        public int ParticipantID { get; set; }

        public TimeSpan? BestTimeRecord { get; set; }

        public int TotalWins { get; set; } = 0;

        public int? BestPositionHeld { get; set; }

        [MaxLength(100)]
        public string? LastRace { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? AveragePosition { get; set; }

        // Navigation property
        [ForeignKey("ParticipantID")]
        public Participant? Participant { get; set; }
    }
}
