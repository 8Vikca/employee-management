using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        // alphabetic order
        IEnumerable<EmployeeModel> GetActiveEmployees();
        IEnumerable<EmployeeModel> GetInActiveEmployees();
        EmployeeModel FindEmployeeById(int id);
        bool MoveEmployeeToArchive(EmployeeModel employee);
        bool EditEmployee(EditEmployeeViewModel editedEmployee);
        bool CreateEmployee(NewEmployeeViewModel newEmployee);
        bool RemoveEmployee(EmployeeModel employee);
    }
}
