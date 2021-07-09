using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Interfaces.Repositories;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appdbContext)
        {
            _appDbContext = appdbContext;
        }

        public IEnumerable<EmployeeModel> GetActiveEmployees() //Ienumerable vs Iqueryable
        {
            try
            {
                var activeEmployee = _appDbContext.Employees.Where(x => x.DeletedDate == null);
                return activeEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeModel> GetInActiveEmployees()
        {
            var activeEmployee = _appDbContext.Employees.Where(x => x.DeletedDate != null);
            return activeEmployee;
        }

        public bool CreateEmployee(NewEmployeeViewModel newEmployee)
        {
            var findWorkPositionByName = _appDbContext.WorkPositions.FirstOrDefault(x => x.WorkPositionName == newEmployee.WorkPositionName);
            if (findWorkPositionByName == null)
                return false;

            var employeeModel = new EmployeeModel()
            {
                Name = newEmployee.Name,
                Surname = newEmployee.Surname,
                Address = newEmployee.Address,
                BirthDate = newEmployee.BirthDate,
                OnBoardDate = newEmployee.OnBoardDate,
                Salary = Convert.ToDouble(newEmployee.Salary),
                WorkPositionId = findWorkPositionByName.Id,
                DeletedDate = null
            };

            _appDbContext.Employees.Add(employeeModel);
            _appDbContext.SaveChanges();

            return true;
        }
        public bool EditEmployee(EditEmployeeViewModel editedEmployee)
        {
            var findWorkPositionByName = _appDbContext.WorkPositions.FirstOrDefault(x => x.WorkPositionName == editedEmployee.WorkPositionName);
            var employeeById = _appDbContext.Employees.FirstOrDefault(x => x.Id == editedEmployee.Id);

            if (employeeById == null || findWorkPositionByName == null)
                return false;

            employeeById.Name = editedEmployee.Name;
            employeeById.Surname = editedEmployee.Surname;
            employeeById.Address = editedEmployee.Address;
            employeeById.BirthDate = DateTime.Parse(editedEmployee.BirthDate);
            employeeById.Salary = Convert.ToDouble(editedEmployee.Salary);
            employeeById.WorkPositionId = findWorkPositionByName.Id;

            _appDbContext.Employees.Update(employeeById);
            _appDbContext.SaveChanges();

            return true;
        }

        public EmployeeModel FindEmployeeById(int id)
        {
            var employeeById = _appDbContext.Employees.FirstOrDefault(employee => employee.Id == id);
            return employeeById;
        }

        public bool RemoveEmployee(EmployeeModel employee)
        {
            _appDbContext.Employees.Remove(employee);
            return true;
        }
    }
}
