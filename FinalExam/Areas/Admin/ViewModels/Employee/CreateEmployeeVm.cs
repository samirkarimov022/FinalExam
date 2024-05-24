using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels
{
    public class CreateEmployeeVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
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
