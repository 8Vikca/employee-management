using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Interfaces.Repositories;
using WebApplication1.Interfaces.Services;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Services
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        IWorkPositionRepository _workPositionRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IWorkPositionRepository workPositionRepository)
        {
            _employeeRepository = employeeRepository;
            _workPositionRepository = workPositionRepository;
        }

        public List<EmployeeViewModel> GetActiveEmployees()
        {
            var employeeRepository = _employeeRepository.GetActiveEmployees().ToList();
            var activeEmployeesList = ModelToViewModel(employeeRepository);
            return activeEmployeesList;
        }

        public List<EmployeeViewModel> GetInactiveEmployees()
        {
            var employeeRepository = _employeeRepository.GetInActiveEmployees().ToList();
            var inactiveEmployeesList = ModelToViewModel(employeeRepository);
            return inactiveEmployeesList;
        }
        private List<EmployeeViewModel> ModelToViewModel(List<EmployeeModel> employeeModel)
        {
            var employeesList = new List<EmployeeViewModel>();

            foreach (var item in employeeModel)
            {
                var employee = new EmployeeViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Address = item.Address,
                    BirthDate = item.BirthDate,
                    Salary = item.Salary,
                    WorkPositionName = item.WorkPosition.WorkPositionName,
                    ArchivedDate = item.ArchivedDate
                };
                employeesList.Add(employee);
            }
            return employeesList;
        }

        public bool CreateEmployee(NewEmployeeViewModel employee)
        {
            try
            {
                _employeeRepository.CreateEmployee(employee);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool EditEmployee(EditEmployeeViewModel employee)
        {
            try
            {
                _employeeRepository.EditEmployee(employee);         
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteEmployee(DeleteEmployeeViewModel employee)
        {
            try
            {
                var employeeById = _employeeRepository.FindEmployeeById(employee.Id);
                if (employeeById == null)
                {
                    return false;
                }
                _employeeRepository.RemoveEmployee(employeeById);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ArchiveEmployee(DeleteEmployeeViewModel employee)
        {
            try
            {
                var employeeById = _employeeRepository.FindEmployeeById(employee.Id);
                if (employeeById == null)
                {
                    return false;
                }
                employeeById.ArchivedDate = DateTime.Parse(employee.ArchivedDate).Date;
                _employeeRepository.MoveEmployeeToArchive(employeeById);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
