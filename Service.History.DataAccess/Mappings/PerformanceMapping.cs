using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.History.DataAccess.Entities;

namespace Service.History.DataAccess.Mappings
{
    public static class PerformanceMapping
    {
        public static void Configure(this EntityTypeBuilder<PerformanceEntity> modelBuilder)
        {
            modelBuilder.ToTable("Performance");
            modelBuilder.Property(x => x.FuelType).IsRequired(false);
            modelBuilder.Property(x => x.MaximumSpeed).IsRequired(false);
            modelBuilder.Property(x => x.EmissionStandart).IsRequired(false);
            modelBuilder.HasOne(x => x.Vehicle).WithOne(x => x.Performance).HasForeignKey<PerformanceEntity>(x => x.VehicleId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}
