using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Name must be entered")]
        [MaxLength(25, ErrorMessage = "Max length is 25")]
        [MinLength(3, ErrorMessage = "Min length is 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname must be entered")]
        [MaxLength(25, ErrorMessage = "Max length is 25")]
        [MinLength(3, ErrorMessage = "Min length is 8")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Username must be entered")]
        [MaxLength(25, ErrorMessage = "Max length is 25")]
        [MinLength(3, ErrorMessage = "Min length is 5")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email must be entered")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword must be entered")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Password be same")]
        public string ConfirmPassword { get; set; }
    }
}
