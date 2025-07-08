namespace SchoolAPI.Model
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        public required string Name { get; set; }
        public required string Subject { get; set; }

        public int SchoolId { get; set; }
        public required School School { get; set; }
    }
}
