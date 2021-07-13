using System.Collections.Generic;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Services
{
    public interface IEmployeeService
    {
        bool ArchiveEmployee(DeleteEmployeeViewModel employee);

        bool CreateEmployee(NewEmployeeViewModel employee);

        bool DeleteEmployee(DeleteEmployeeViewModel employee);

        bool EditEmployee(EditEmployeeViewModel employee);

        List<EmployeeViewModel> GetActiveEmployees();

        List<EmployeeViewModel> GetInactiveEmployees();

    }
}
