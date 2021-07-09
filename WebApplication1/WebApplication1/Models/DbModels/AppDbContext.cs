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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WorkPositionModel>().HasData(new WorkPositionModel { Id = 1, WorkPositionName = "Tester", IsActive = true });
            modelBuilder.Entity<WorkPositionModel>().HasData(new WorkPositionModel { Id = 2, WorkPositionName = "Programmer", IsActive = true });
            modelBuilder.Entity<WorkPositionModel>().HasData(new WorkPositionModel { Id = 3, WorkPositionName = "Support", IsActive = true });
            modelBuilder.Entity<WorkPositionModel>().HasData(new WorkPositionModel { Id = 4, WorkPositionName = "Analyst", IsActive = true });
            modelBuilder.Entity<WorkPositionModel>().HasData(new WorkPositionModel { Id = 5, WorkPositionName = "Businessman", IsActive = true });
            modelBuilder.Entity<WorkPositionModel>().HasData(new WorkPositionModel { Id = 6, WorkPositionName = "Other", IsActive = true });
        }
       
    }
}
