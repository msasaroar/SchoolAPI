namespace SchoolAPI.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        public required string Name { get; set; }
        public required string Subject { get; set; }

        public int SchoolId { get; set; }
        public School? School { get; set; }
    }
}
