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
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Studio> Studio { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        /* Conexão com SQL Server */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var Conn = @"Server=LYDIA-2;
                         DataBase=TelaAzul;
                         integrated security=true;
                         Trust Server Certificate=true";

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
                //entidade.Property(e => e.Titulo_original).HasMaxLength(50);
                entidade.Property(e => e.Sinopse).HasMaxLength(250);

                // Um pra Muitos
                entidade.HasOne(e => e.Genero).WithMany(c => c.Filmes)
                .HasConstraintName("FK_Genero_Filme").OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Studio>(entidade =>
            {
                entidade.HasKey(e => e.Id); 
            });

            modelBuilder.Entity<Cliente>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Nome).HasMaxLength(50);
                entidade.Property(e => e.Email).HasMaxLength(50);
                entidade.Property(e => e.Senha).HasMaxLength(30);
            });

        }
    }
}