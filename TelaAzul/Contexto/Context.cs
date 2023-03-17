using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Contexto
{
    public class Context : DbContext
    {
        public Context() 
        {
            this.Database.EnsureCreated();
        }

        /* DbSets */
        public DbSet<Genero> Categorias { get; set; }
        public DbSet<Filme> Filmes { get; set; }

        /* Conexão com SQL Server */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var Conn = @"Server=NomeServer;
                         DataBase=NomeBanco;
                         integrated security=true";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Conn);
            }
        }

        /* Validações dos campos do Banco  */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Nome).HasMaxLength(100);
            });

            modelBuilder.Entity<Filme>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Titulo).HasMaxLength(50);
                entidade.Property(e => e.Titulo_original).HasMaxLength(50);
                entidade.Property(e => e.Sinopse).HasMaxLength(150);

                /*
                 * Um pra Muitos
                entidade.HasOne(e => e.).WithMany(c => c.)
                .HasConstraintName("FK_").OnDelete(DeleteBehavior.NoAction);
                */

                entidade.HasMany(e => e.Vendas).WithMany(c => c.Filme);
            });
        }
    }
}