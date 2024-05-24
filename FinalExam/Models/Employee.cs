using FinalExam.Models.Common;

namespace FinalExam.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }     
        public string Description { get; set; }
        public string Image { get; set; }
     

    }
}
