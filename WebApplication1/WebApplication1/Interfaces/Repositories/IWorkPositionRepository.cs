using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Models.DbModels;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Interfaces.Repositories
{
    public interface IWorkPositionRepository
    {
        bool AddWorkPositionToHistory(NewEmployeeViewModel employee, int workPositionId);

        bool CreateNewWorkPosition(WorkPositionViewModel newWorkPositionModel);

        bool EditWorkPositionInHistory(EditEmployeeViewModel employeeModel, int employeeId);

        WorkPositionModel FindPositionByName(string workPositionName);

        IEnumerable<WorkPositionModel> GetAllActiveWorkPositions();

        IEnumerable<WorkPositionsHistoryModel> GetHistoryOfPositionsByEmployee(int employeeId);

        bool RemoveWorkPosition(WorkPositionModel workPosition);     

    }
}
