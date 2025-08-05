using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartmentsAsync();
}

public class DepartmentService : IDepartmentService
{
    public Task<IEnumerable<Department>> GetDepartmentsAsync()
    {
        return Task.FromResult<IEnumerable<Department>>(new List<Department>());
    }
}
