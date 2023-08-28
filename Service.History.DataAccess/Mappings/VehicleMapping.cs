using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.History.DataAccess.Entities;

namespace Service.History.DataAccess.Mappings
{
    public static class VehicleMapping
    {
        public static void Configure(this EntityTypeBuilder<VehicleEntity> modelBuilder)
        {
            modelBuilder.ToTable("Vehicles");
            modelBuilder.Property(x => x.Vin).IsRequired(false);
            modelBuilder.Property(x => x.PowertrainArchitecture).IsRequired(false);
            modelBuilder.Property(x => x.Body).IsRequired(false);
            modelBuilder.Property(x => x.Seats).IsRequired(false);
            modelBuilder.Property(x => x.Doors).IsRequired(false);
            modelBuilder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}
