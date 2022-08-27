using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirLines.Models
{
    public class User
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "Varchar(50)")]
        public string? Email { get; set; }
        [Required]
        [MaxLength(10), MinLength(10)]
        [Column(TypeName = "Varchar(10)")]
        public string?  PanNumber { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [Column(TypeName = "Varchar(10)")]
        public string? Password { get; set; }
        [Required]
        [Column(TypeName = "Varchar(10)")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
        [Required]
        [Column(TypeName = "Varchar(10)")]
        [DefaultValue("Pending")]
        public string? Status { get; set; }
        [Required]
        public int RoleId { get; set; }
       
    }
}
