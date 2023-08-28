using Microsoft.AspNetCore.Identity;
using Service.History.DataAccess.Entities;

namespace Service.History.DataAccess.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Forename { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public virtual ICollection<CarEntity> Cars { get; set; } = null!;
    }
}
