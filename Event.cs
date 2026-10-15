using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("EVENT")]
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        public int EventOrganiserID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(150)]
        public string Location { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string Routemap { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string EventType { get; set; } = string.Empty;

        public DateTime RegistrationDeadline { get; set; }

        [MaxLength(500)]
        public string? BannerImageUri { get; set; }

        // Navigation properties
        [ForeignKey("EventOrganiserID")]
        public EventOrganiser? EventOrganiser { get; set; }

        // This collection resolves the CS1061 error in the controller!
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}