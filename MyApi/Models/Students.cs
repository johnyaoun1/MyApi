namespace MyApi.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
    }
}
