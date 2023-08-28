using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.History.DataAccess.Authentication;

namespace Service.History.DataAccess.Mappings
{
    public static class ApplicationUserMapping
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUser> modelBuilder)
        {
            modelBuilder.HasMany(x => x.Cars).WithOne(y => y.User).HasForeignKey(z => z.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
