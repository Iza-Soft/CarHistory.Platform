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
    public static class CarMapping
    {
        public static void Configure(this EntityTypeBuilder<CarEntity> modelBuilder)
        {
            modelBuilder.Property(x => x.Brand).HasColumnName("Brand");
            modelBuilder.Property(x => x.Model).HasColumnName("Model");
            modelBuilder.Property(x => x.Generation).HasColumnName("Generation");
            modelBuilder.Property(x => x.Modification).HasColumnName("Modification");
            modelBuilder.Property(x => x.PlateNumber).HasColumnName("PlateNumber");
            modelBuilder.HasBaseType<VehicleEntity>();
        }
    }
}
