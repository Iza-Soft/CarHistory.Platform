using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.History.DataAccess.Entities;

namespace Service.History.DataAccess.Mappings
{
    public static class EngineMapping
    {
        public static void Configure(this EntityTypeBuilder<EngineEntity> modelBuilder)
        {
            modelBuilder.ToTable("Engine");
            modelBuilder.Property(x => x.Power).IsRequired(false);
            modelBuilder.Property(x => x.Torque).IsRequired(false);
            modelBuilder.Property(x => x.Model).IsRequired(false);
            modelBuilder.Property(x => x.PositionOfCylinders).IsRequired(false);
            modelBuilder.Property(x => x.FuelSystem).IsRequired(false);
            modelBuilder.Property(x => x.Aspiration).IsRequired(false);
            modelBuilder.HasOne(x => x.Vehicle).WithOne(x => x.Engine).HasForeignKey<EngineEntity>(x => x.VehicleId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}
