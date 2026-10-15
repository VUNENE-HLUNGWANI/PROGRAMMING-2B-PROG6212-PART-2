using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public int EventID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Distance { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryLevel { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal EntryFee { get; set; }

        public int MaxParticipants { get; set; }

        // Navigation property
        [ForeignKey("EventID")]
        public Event? Event { get; set; }
    }
}