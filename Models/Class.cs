namespace SchoolAPI.Models
{
    public class Class
    {
        public int ClassId { get; set; }

        public required string Name { get; set; }
        public required string Section { get; set; }

        public int SchoolId { get; set; }
        public School? School { get; set; }

        public ICollection<Student>? Students { get; set; } = new List<Student>();
    }
}
