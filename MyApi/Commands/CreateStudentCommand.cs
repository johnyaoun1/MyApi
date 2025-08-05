using MediatR;
using MyApi.Models;
using MyApi.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyApi.Commands
{
    public class CreateStudentCommand : IRequest<Student>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Student>
    {
        private readonly StudentService _studentService;

        public CreateStudentCommandHandler(StudentService studentService)
        {
            _studentService = studentService;
        }

        public Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var newStudent = new Student { Name = request.Name, Email = request.Email };
            _studentService.AddStudent(newStudent);
            return Task.FromResult(newStudent);
        }
    }
}