using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Username or Email must be entered")]
        [MaxLength(25,ErrorMessage ="Max length is 25")]
        [MinLength(5, ErrorMessage = "Min length is 5")]
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Password must be entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
