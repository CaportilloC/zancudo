using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using zancudo.Entidades;
using zancudo.Entidades.Seeding;

namespace zancudo
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedingZancudo.Seed(modelBuilder);

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Disfraz> Disfraces { get; set; }
        public DbSet<TipoDisfraz> TiposDisfraces { get; set; }
        public DbSet<TipoPago> TiposPagos { get; set; }
        public DbSet<ClienteDisfraz> ClientesDisfraces { get; set; }
    }
}
