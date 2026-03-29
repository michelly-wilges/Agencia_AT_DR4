using Agencia_AT_DR4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Agencia_AT_DR4.Data
{
    public class AgenciaContext : DbContext
    {
        public AgenciaContext(DbContextOptions<AgenciaContext> options) : base(options) { }

        public DbSet<PaisDestino> PaisesDestino { get; set; }
        public DbSet<CidadeDestino> CidadesDestino { get; set; }
        public DbSet<PacoteTuristico> PacotesTuristicos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tamanhos máximos
            modelBuilder.Entity<PaisDestino>()
                .Property(p => p.Nome).HasMaxLength(100);

            modelBuilder.Entity<PaisDestino>()
                .Property(p => p.Codigo).HasMaxLength(2);

            modelBuilder.Entity<CidadeDestino>()
                .Property(c => c.Nome).HasMaxLength(100);

            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.Titulo).HasMaxLength(200);

            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.Preco).HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nome).HasMaxLength(100);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Email).HasMaxLength(150);

            // Seed Data
            modelBuilder.Entity<PaisDestino>().HasData(
                new PaisDestino { Id = 1, Nome = "Brasil", Codigo = "BR" },
                new PaisDestino { Id = 2, Nome = "Argentina", Codigo = "AR" },
                new PaisDestino { Id = 3, Nome = "Portugal", Codigo = "PT" }
            );

            modelBuilder.Entity<CidadeDestino>().HasData(
                new CidadeDestino { Id = 1, Nome = "Rio de Janeiro", PaisDestinoId = 1 },
                new CidadeDestino { Id = 2, Nome = "Florianópolis", PaisDestinoId = 1 },
                new CidadeDestino { Id = 3, Nome = "Buenos Aires", PaisDestinoId = 2 },
                new CidadeDestino { Id = 4, Nome = "Lisboa", PaisDestinoId = 3 }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nome = "João Silva", Email = "joao@email.com" },
                new Cliente { Id = 2, Nome = "Maria Souza", Email = "maria@email.com" }
            );

            modelBuilder.Entity<PacoteTuristico>().HasData(
                new PacoteTuristico { Id = 1, Titulo = "Carnaval no Rio", DataInicio = new DateTime(2026, 2, 28), CapacidadeMaxima = 20, Preco = 2500.00m },
                new PacoteTuristico { Id = 2, Titulo = "Micareta na Bahia", DataInicio = new DateTime(2026, 12, 10), CapacidadeMaxima = 15, Preco = 1800.00m },
                new PacoteTuristico { Id = 3, Titulo = "Lua de mel em Noronha", DataInicio = new DateTime(2026, 7, 15), CapacidadeMaxima = 10, Preco = 5200.00m }
            );
        }
    }
}