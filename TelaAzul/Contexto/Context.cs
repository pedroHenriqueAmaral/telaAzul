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
        public DbSet<Categoria> Categorias { get; set; }
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
            modelBuilder.Entity<Categoria>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Nome).HasMaxLength(100);
            });

            modelBuilder.Entity<Filme>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Descricao).HasMaxLength(150);
                
                /* Relacionamento Filme-Categoria  */
                entidade.HasOne(e => e.Categoria).WithMany(c => c.Filme)
                .HasConstraintName("FK_Filme_Categoria").OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}