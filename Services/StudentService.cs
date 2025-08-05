using MyApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyApi.Services
{
    public class StudentService
    {
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        public List<Student> GetAll() => _students;
        public Student? GetById(long id) => _students.FirstOrDefault(s => s.Id == id);
        public List<Student> GetByName(string name) => _students.Where(s => s.Name != null && s.Name.Contains(name)).ToList();
        public void AddStudent(Student student) => _students.Add(student);
        public void UpdateStudent(long id, Student student)
        {
            var existingStudent = GetById(id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
            }
        }
        public void DeleteStudent(long id) => _students.RemoveAll(s => s.Id == id);
    }
}
