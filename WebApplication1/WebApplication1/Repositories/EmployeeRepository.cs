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
        IWorkPositionRepository _workPositionRepository;

        public EmployeeRepository(AppDbContext appdbContext, IWorkPositionRepository workPositionRepository)
        {
            _appDbContext = appdbContext;
            _workPositionRepository = workPositionRepository;
        }

        public IEnumerable<EmployeeModel> GetActiveEmployees() //Ienumerable vs Iqueryable
        {
            try
            {
                var activeEmployee = _appDbContext.Employees.Include(x => x.WorkPosition).Where(x => x.ArchivedDate == null && x.IsDeleted == false);
                return activeEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeModel> GetInActiveEmployees()
        {
            var activeEmployee = _appDbContext.Employees.Include(x => x.WorkPosition).Where(x => x.ArchivedDate != null && x.IsDeleted == false);
            return activeEmployee;
        }

        public bool CreateEmployee(NewEmployeeViewModel newEmployee)
        {
            var findWorkPositionId = _appDbContext.WorkPositions.FirstOrDefault(x => x.WorkPositionName == newEmployee.WorkPositionName);
            if (findWorkPositionId == null)
                return false;

            var employeeModel = new EmployeeModel()
            {
                Name = newEmployee.Name,
                Surname = newEmployee.Surname,
                Address = newEmployee.Address,
                BirthDate = newEmployee.BirthDate.Date,
                OnBoardDate = newEmployee.OnBoardDate.Date,
                Salary = Convert.ToDouble(newEmployee.Salary),
                WorkPositionId = findWorkPositionId.Id,
                ArchivedDate = null,
                IsDeleted = false,
            };

            _appDbContext.Employees.Add(employeeModel);
            _appDbContext.SaveChanges();

            _workPositionRepository.AddWorkPositionToHistory(newEmployee, findWorkPositionId.Id);

            return true;
        }
        public bool EditEmployee(EditEmployeeViewModel editedEmployee)
        {
            var findWorkPositionByName = _appDbContext.WorkPositions.FirstOrDefault(x => x.WorkPositionName == editedEmployee.WorkPositionName);
            var employeeById = _appDbContext.Employees.FirstOrDefault(x => x.Id == editedEmployee.Id);


            if (employeeById == null || findWorkPositionByName == null)
                return false;

            if (employeeById.WorkPositionId != findWorkPositionByName.Id)
                _workPositionRepository.EditWorkPositionInHistory(editedEmployee, employeeById.Id);

            employeeById.Name = editedEmployee.Name;
            employeeById.Surname = editedEmployee.Surname;
            employeeById.Address = editedEmployee.Address;
            employeeById.BirthDate = DateTime.Parse(editedEmployee.BirthDate).Date;
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
            employee.IsDeleted = true;
            _appDbContext.Employees.Update(employee);
            _appDbContext.SaveChanges();

            return true;
        }
        public bool MoveEmployeeToArchive(EmployeeModel employee)
        {
            _appDbContext.Employees.Update(employee);

            var historyWorkPositionById = _appDbContext.WorkPositionsHistory.FirstOrDefault(x => x.EmployeeId == employee.Id && x.EndDate == null);
            historyWorkPositionById.EndDate = DateTime.UtcNow.Date;

            _appDbContext.WorkPositionsHistory.Update(historyWorkPositionById);
            _appDbContext.SaveChanges();

            return true;

        }

    }
}
