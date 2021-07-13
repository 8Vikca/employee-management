using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DbModels;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<EmployeeModel> Employees { get; set;}
        public DbSet<WorkPositionModel> WorkPositions { get; set; }
        public DbSet<WorkPositionsHistoryModel> WorkPositionsHistory { get; set; }
    }
}
