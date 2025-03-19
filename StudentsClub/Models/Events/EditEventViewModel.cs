using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StudentsClub.Models.Events
{
    public class EditEventViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string NameOfEvent { get; set; }

        [DataType(DataType.DateTime)]
        public DateOnly DateOfEvent { get; set; }

        [AllowNull]
        [StringLength(150)]
        public string Description { get; set; }

        public string UserId { get; set; }

    }
}
