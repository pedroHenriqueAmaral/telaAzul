using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Contexto
{
    public class Context : DbContext
    {
        public Context() 
        {
            // Cria o banco, atualiza se já existir
            this.Database.EnsureCreated();
        }

        /* DbSets */
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<ComprasFilmes> ComprasFilmes { get; set; }
        //---
        public DbSet<Studio> Studio { get; set; }
        public DbSet<Ingresso> Ingresso { get; set; }
        public DbSet<Sala> Sala { get; set; }
        
        /* Conexão com SQL Server */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var Conn = @"Server=LYDIA-1;
                         DataBase=telaAzulLv10;
                         integrated security=true;
                         Trust Server Certificate=true";

            /*
            var ConnRemote = @"Server=ServerName;
                               DataBase=Banco;
                               user id = usuário;
                               password = senha";
            */
                                
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
                entidade.Property(e => e.Sinopse).HasMaxLength(250);

                // Relacionamento com Gênero
                entidade.HasOne(e => e.Genero)
                .WithMany(c => c.Filme)
                .HasConstraintName("FK_Filme_Genero")
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Cliente>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Nome).HasMaxLength(50);
                entidade.Property(e => e.Email).HasMaxLength(50);
                entidade.Property(e => e.Senha).HasMaxLength(30);
            });

            modelBuilder.Entity<Funcionario>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Nome).HasMaxLength(50);
                entidade.Property(e => e.Email).HasMaxLength(50);
                entidade.Property(e => e.Senha).HasMaxLength(30);
            });

            modelBuilder.Entity<Status>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Descricao).HasMaxLength(100);
            });

            modelBuilder.Entity<Compra>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Valor).HasPrecision(8,2);

                // Relacionamento com Status
                entidade.HasOne(e => e.Status) // "Lado de Um" Prop
                .WithMany(c => c.Compra) // "Lado de Muitos"
                .HasForeignKey(e => e.IdStatus)
                .HasConstraintName("FK_Compras_Status") // Nome do Relacionamento
                .OnDelete(DeleteBehavior.NoAction);
            });

            // Relação Muitos pra Muitos entre Filme e Compra
            modelBuilder.Entity<ComprasFilmes>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Valor).HasPrecision(8, 2);

                // Relacionamento com Filme
                entidade.HasOne(e => e.Filme)
                .WithMany(c => c.ComprasFilmes)
                .HasForeignKey(e => e.FilmeId)
                .HasConstraintName("FK_ComprasFilmes_Filme")
                .OnDelete(DeleteBehavior.NoAction);

                // Relacionamento com Compra
                entidade.HasOne(e => e.Compra)
                .WithMany(c => c.ComprasFilmes)
                .HasForeignKey(e => e.CompraId)
                .HasConstraintName("FK_ComprasFilmes_Compra")
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Studio>(entidade =>
            {
                entidade.HasKey(e => e.Id);
                entidade.Property(e => e.Nome).HasMaxLength(50);
            });
        }
    }
}
