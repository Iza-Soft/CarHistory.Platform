using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Service.History.DataAccess.Authentication;

namespace Service.History.DataAccess.Entities
{
    public class CarEntity : VehicleEntity
    {
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Generation { get; set; } = null!;

        public string Modification { get; set; } = null!;

        public string PlateNumber { get; set; } = null!;
    }
}
