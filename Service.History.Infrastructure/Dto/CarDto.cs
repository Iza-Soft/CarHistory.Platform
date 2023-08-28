using Service.History.DataAccess.Authentication;
using Service.History.DataAccess.Entities;

namespace Service.History.Infrastructure.Dto
{
    public class CarDto : BaseDto<int>
    {
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Generation { get; set; } = null!;

        public string Modification { get; set; } = null!;

        public string PlateNumber { get; set; } = null!;

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
