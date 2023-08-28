using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.History.DataAccess.Entities
{
    public class EngineEntity : BaseEntity<int>
    {
        public string Power { get; set; } = null!;

        public string Torque { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int EngineDisplacement { get; set; }

        public int NumberOfCylinders { get; set; }

        public string PositionOfCylinders { get; set; } = null!;

        public int NumberOfValvesPerCylinder { get; set; }

        public string FuelSystem { get; set; } = null!;

        public string Aspiration { get; set; } = null!;

        public int VehicleId { get; set; }

        public virtual VehicleEntity Vehicle { get; set; } = null!;
    }
}
