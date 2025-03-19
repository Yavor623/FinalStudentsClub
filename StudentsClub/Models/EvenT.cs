using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsClub.Models
{
    public class EvenT
    {
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string NameOfEvent { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateOnly DateOfEvent { get; set; }
        [AllowNull]
        [StringLength(150)]
        
        public string Description { get; set; }
        [ForeignKey("UserId")]
        
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
