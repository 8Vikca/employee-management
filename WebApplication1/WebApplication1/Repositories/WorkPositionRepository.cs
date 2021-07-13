using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Interfaces.Repositories;
using WebApplication1.Models;
using WebApplication1.Models.DbModels;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repositories
{
    public class WorkPositionRepository: IWorkPositionRepository
    {
        private readonly AppDbContext _appDbContext;
        

        public WorkPositionRepository(AppDbContext appdbContext)
        {
            _appDbContext = appdbContext;
        }

        public IEnumerable<WorkPositionModel> GetAllActiveWorkPositions()
        {
            var activeWorkPositions = _appDbContext.WorkPositions.Where(x => x.IsActive);
            return activeWorkPositions;
        }

        public bool CreateNewWorkPosition(WorkPositionViewModel newWorkPositionModel)
        {
            var isAlreadyCreated = _appDbContext.WorkPositions.FirstOrDefault(x => x.WorkPositionName == newWorkPositionModel.WorkPositionName);
            if (isAlreadyCreated != null)
                return false;

            var workPositionModel = new WorkPositionModel()
            {
                WorkPositionName = newWorkPositionModel.WorkPositionName,
                IsActive = true,
            };

            _appDbContext.WorkPositions.Add(workPositionModel);
            _appDbContext.SaveChanges();

            return true;
        }

        public WorkPositionModel FindPositionByName(string workPositionName)
        {
            var workPositionById = _appDbContext.WorkPositions.FirstOrDefault(position => position.WorkPositionName == workPositionName);
            return workPositionById;
        }


        public bool RemoveWorkPosition(WorkPositionModel workPosition) 
        {
            workPosition.IsActive = false;

            _appDbContext.WorkPositions.Update(workPosition);
            _appDbContext.SaveChanges();

            return true;
        }

        public bool AddWorkPositionToHistory(NewEmployeeViewModel employeeModel, int workPositionId)
        {
            var workPositionsHistoryModel = new WorkPositionsHistoryModel
            {
                EmployeeId = _appDbContext.Employees.FirstOrDefault(x => x.Name == employeeModel.Name).Id,
                StartDate = employeeModel.OnBoardDate,
                EndDate = null,
                WorkPositionId = workPositionId
            };

            _appDbContext.WorkPositionsHistory.Add(workPositionsHistoryModel);
            _appDbContext.SaveChanges();

            return true;
        }
        public bool EditWorkPositionInHistory(EditEmployeeViewModel editedEmployeeModel, int employeeId)
        {
            var historyWorkPositionById = _appDbContext.WorkPositionsHistory.FirstOrDefault(x => x.EmployeeId == employeeId && x.EndDate == null);
            historyWorkPositionById.EndDate = DateTime.UtcNow.Date;


            var workPositionsHistoryModel = new WorkPositionsHistoryModel
            {
                EmployeeId = employeeId,
                StartDate = DateTime.UtcNow.Date,
                EndDate = null,
                WorkPositionId = _appDbContext.WorkPositions.FirstOrDefault(x => x.WorkPositionName == editedEmployeeModel.WorkPositionName).Id
            };

            _appDbContext.WorkPositionsHistory.Update(historyWorkPositionById);
            _appDbContext.WorkPositionsHistory.Add(workPositionsHistoryModel);
            _appDbContext.SaveChanges();

            return true;
        }
        public IEnumerable<WorkPositionsHistoryModel> GetHistoryOfPositionsByEmployee(int employeeId)
        {
            return _appDbContext.WorkPositionsHistory.Include(x => x.WorkPosition).Where(x => x.EmployeeId == employeeId);
        }
    }
}
