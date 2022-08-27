using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineDll.Entities
{
    public class Airline
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Name Required")]
        [Column(TypeName = "Varchar(50)")]
        public string? Name { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "Name Required")]
        [Column(TypeName = "Varchar(30)")]
        public string? FromCity { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "Name Required")]
        [Column(TypeName = "Varchar(30)")]
        public string? ToCity { get; set; }
        [Required(ErrorMessage = "Fare Required")]
        [Column(TypeName ="int")]
        public int Fare { set; get; }

    }
}
