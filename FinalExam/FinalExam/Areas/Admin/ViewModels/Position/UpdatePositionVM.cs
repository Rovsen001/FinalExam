using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels.Position
{
    public class UpdatePositionVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        [StringLength(30, ErrorMessage = "Max 30 characters"),
            MinLength(3, ErrorMessage = "Min 3 characters")]
        public string Name { get; set; }
    }
}
