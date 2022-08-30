using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineWebApp.Models
{
    public class AirlineViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required, MaxLength(30)]
        public string? FromCity { get; set; }
        [Required, MaxLength(30)]
        public string? ToCity { get; set; }
        [Required]
        public int Fare { get; set; }
    }
}
