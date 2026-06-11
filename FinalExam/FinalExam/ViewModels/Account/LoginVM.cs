using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Required field")]
        [StringLength(30, ErrorMessage = "Max 30 characters"),
            MinLength(3, ErrorMessage = "Min 3 characters")]
        [EmailAddress(ErrorMessage = "Must be an email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required field")]
        [StringLength(30, ErrorMessage = "Max 30 characters"),
            MinLength(8, ErrorMessage = "Min 8 characters")]
        public string Password { get; set; }

    }
}
