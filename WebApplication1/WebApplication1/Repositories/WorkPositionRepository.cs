using System.Collections.Generic;
using System.Linq;
using WebApplication1.Interfaces.Repositories;
using WebApplication1.Models;
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

        public string GetWorkPositionById(int id)
        {
            var workPosition = _appDbContext.WorkPositions.FirstOrDefault(x => x.Id == id).WorkPositionName;
            return workPosition;
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

        public WorkPositionModel FindPositionById(int id)
        {
            var workPositionById = _appDbContext.WorkPositions.FirstOrDefault(position => position.Id == id);
            return workPositionById;
        }


        public bool RemoveWorkPosition(WorkPositionModel workPosition)
        {
            workPosition.IsActive = false;
            _appDbContext.WorkPositions.Update(workPosition);
            _appDbContext.SaveChanges();
            return true;
        }
    }
}
