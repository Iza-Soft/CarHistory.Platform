using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.History.DataAccess.Entities
{
    public class PerformanceEntity: BaseEntity<int>
    {
        public string FuelType { get; set; } = null!;

        public string MaximumSpeed { get; set; } = null!;

        public string EmissionStandart { get; set; } = null!;

        public int VehicleId { get; set; }

        public virtual VehicleEntity Vehicle { get; set; } = null!;
    }
}
