using ERP_System.Model;
using Microsoft.EntityFrameworkCore;

namespace ERP_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SolicitacaoDeCargo> Solicitacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolicitacaoDeCargo>()
                .Property(s => s.Status)
                .HasConversion<string>();
        }

    }
}
