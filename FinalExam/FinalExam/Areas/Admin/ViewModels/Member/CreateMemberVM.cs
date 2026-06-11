using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalExam.Areas.Admin.ViewModels.Member
{
    public class CreateMemberVM
    {
        [Required(ErrorMessage = "Required field")]
        [StringLength(30, ErrorMessage = "Max 30 characters"),
            MinLength(3, ErrorMessage = "Min 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required field")]
        [StringLength(30, ErrorMessage = "Max 30 characters"),
            MinLength(3, ErrorMessage = "Min 3 characters")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Required field")]
        [StringLength(150, ErrorMessage = "Max 150 characters"),
            MinLength(15, ErrorMessage = "Min 15 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int PositionId { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
