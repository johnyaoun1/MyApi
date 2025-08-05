using MediatR; 

public class CreateDepartmentCommand : IRequest<Department>
{
    public string Name { get; set; }

    public CreateDepartmentCommand(string name)
    {
        Name = name;
    }
}
