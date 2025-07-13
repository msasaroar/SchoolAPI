namespace SchoolAPI.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        public required string Name { get; set; }
        public int Age { get; set; }

        public int SchoolId { get; set; }
        public int ClassId { get; set; }

        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string ClassName { get; set; }
        public string ClassSection { get; set; }
    }
}
