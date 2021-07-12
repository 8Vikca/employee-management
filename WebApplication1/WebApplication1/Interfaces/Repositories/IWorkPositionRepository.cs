using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Models.DbModels;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Repositories
{
    public interface IWorkPositionRepository
    {
        IEnumerable<WorkPositionModel> GetAllActiveWorkPositions();
        bool CreateNewWorkPosition(WorkPositionViewModel newWorkPositionModel);
        WorkPositionModel FindPositionByName(string workPositionName);
        bool RemoveWorkPosition(WorkPositionModel workPosition);
        bool AddWorkPositionToHistory(NewEmployeeViewModel employee, int workPositionId);
        bool EditWorkPositionInHistory(EditEmployeeViewModel employeeModel, int employeeId);
        IEnumerable<WorkPositionsHistoryModel> GetHistoryOfPositionsByEmployee(int employeeId);
    }
}
