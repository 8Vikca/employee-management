using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {    
        bool CreateEmployee(NewEmployeeViewModel newEmployee);

        bool EditEmployee(EditEmployeeViewModel editedEmployee);

        EmployeeModel FindEmployeeById(int id);

        IEnumerable<EmployeeModel> GetActiveEmployees();

        IEnumerable<EmployeeModel> GetInActiveEmployees();

        bool MoveEmployeeToArchive(EmployeeModel employee);

        bool RemoveEmployee(EmployeeModel employee);
    }
}
