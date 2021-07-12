using System.Collections.Generic;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Services
{
    public interface IEmployeeService
    {
        // alphabetic order
        List<EmployeeViewModel> GetActiveEmployees();
        List<EmployeeViewModel> GetInactiveEmployees();
        bool CreateEmployee(NewEmployeeViewModel employee);
        bool DeleteEmployee(DeleteEmployeeViewModel employee);
        bool EditEmployee(EditEmployeeViewModel employee);
        bool ArchiveEmployee(DeleteEmployeeViewModel employee);

    }
}
