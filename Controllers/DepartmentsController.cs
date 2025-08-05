using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/departments")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDepartmentService _departmentService;

    // Inject MediatR and IDepartmentService into the controller
    public DepartmentsController(IMediator mediator, IDepartmentService departmentService)
    {
        _mediator = mediator;
        _departmentService = departmentService;
    }

    // POST api/departments
    [HttpPost]
    public async Task<ActionResult<Department>> AddDepartment([FromBody] CreateDepartmentCommand command)
    {
        // Send the CreateDepartmentCommand to the handler via MediatR
        var department = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDepartments), new { id = department.Id }, department);
    }

    // Get all departments (same as before)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        var departments = await _departmentService.GetDepartmentsAsync();
        return Ok(departments);
    }
}
