using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.History.DataAccess.Authentication;
using Service.History.DataAccess.Entities;
using Service.History.DataAccess.Mappings;

namespace Service.History.DataAccess
{
    /*https://stackoverflow.com/questions/24079133/what-is-the-difference-between-dbset-and-virtual-dbset*/
    public class ServiceHistoryContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ServiceHistoryContext(DbContextOptions<ServiceHistoryContext> options) : base(options) { }

        public virtual DbSet<VehicleEntity> Vehicles { get; set; } = null!;

        public virtual DbSet<EngineEntity> Engines { get; set; } = null!;

        public virtual DbSet<PerformanceEntity> Performances { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().Configure();
            modelBuilder.Entity<VehicleEntity>().Configure();
            modelBuilder.Entity<CarEntity>().Configure();
            modelBuilder.Entity<EngineEntity>().Configure();
            modelBuilder.Entity<PerformanceEntity>().Configure();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
