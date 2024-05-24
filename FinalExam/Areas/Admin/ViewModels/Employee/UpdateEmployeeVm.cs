using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels
{
    public class UpdateEmployeeVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public string FbLink { get; set; }
        [Required]
        public string TwLink { get; set; }
        [Required]
        public string InstaLink { get; set; }
        [Required]
        public string LinkedinLink { get; set; }
    }
}
