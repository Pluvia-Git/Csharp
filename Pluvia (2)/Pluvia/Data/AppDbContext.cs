using Microsoft.EntityFrameworkCore;
using Pluvia.Models;

namespace Pluvia.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Clima> Climas { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento 1:N entre Endereco e Usuario
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Endereco)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.EnderecoId);

            // Relacionamento 1:N entre Endereco e Clima
            modelBuilder.Entity<Clima>()
                .HasOne(c => c.Endereco)
                .WithMany(e => e.Climas)
                .HasForeignKey(c => c.EnderecoId);

            // Relacionamento 1:N entre Endereco e Alerta
            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Endereco)
                .WithMany(e => e.Alertas)
                .HasForeignKey(a => a.EnderecoId);

            // Relacionamento 1:N entre Usuario e Alerta
            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Alertas)
                .HasForeignKey(a => a.UsuarioId);

            // (Opcional) Seeds de exemplo – remova se não quiser dados fixos
            modelBuilder.Entity<Endereco>().HasData(
                new Endereco
                {
                    Id = 1,
                    Cidade = "São Paulo",
                    Bairro = "Centro",
                    Cep = "01000-000",
                    Estado = "SP",
                    Logradouro = "Rua Exemplo",
                    Latitude = -23.5505f,
                    Longitude = -46.6333f
                }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "João da Silva",
                    Email = "joao@email.com",
                    Cpf = "12345678900",
                    Senha = "senha123",
                    EnderecoId = 1
                }
            );
        }
    }
}
