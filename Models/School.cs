namespace SchoolAPI.Model
{
    public class School
    {
        public int SchoolId { get; set; }

        public required string Name { get; set; }
        public required string Address { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
