using SchoolAPI.Models;

namespace SchoolAPI.DTOs
{
    public class StudentDTO: Student
    {
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string ClassName { get; set; }
        public string ClassSection { get; set; }
    }
}
