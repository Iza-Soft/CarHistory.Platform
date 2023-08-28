using Service.History.DataAccess.Authentication;

namespace Service.History.DataAccess.Entities
{
    public abstract class VehicleEntity : BaseEntity<int>
    {
        public string Vin { get; set; } = null!;

        public int StartOfProduction { get; set; }

        public int? EndOfProduction { get; set; }

        public string PowertrainArchitecture { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string Seats { get; set; } = null!;

        public string Doors { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual EngineEntity Engine { get; set; } = null!;

        public virtual PerformanceEntity Performance { get; set; } = null!;
    }
}
