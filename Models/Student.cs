namespace SchoolAPI.Model
{
    public class Student
    {
        public int StudentId { get; set; }

        public required string Name { get; set; }
        public int Age { get; set; }

        public int SchoolId { get; set; }
        public required School School { get; set; }

        public int ClassId { get; set; }
        public required Class Class { get; set; }
    }
}
