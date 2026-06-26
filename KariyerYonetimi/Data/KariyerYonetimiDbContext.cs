using Microsoft.EntityFrameworkCore;

namespace KariyerYonetimi.Data
{
    public class KariyerYonetimiDbContext:DbContext
    {
        public KariyerYonetimiDbContext(DbContextOptions<KariyerYonetimiDbContext> options) : base(options)
        {
        }
        public DbSet<KariyerYonetimi.Models.Personel> Personeller { get; set; }

        public DbSet<KariyerYonetimi.Models.Departman> Departmanlar { get; set; }
    }
}
