using System;
using System.Collections.Generic;
using BolosDoJacquinDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinDb.BdContext;

public partial class BolosDoJacquinDbContext : DbContext
{
    public BolosDoJacquinDbContext()
    {
    }

    public BolosDoJacquinDbContext(DbContextOptions<BolosDoJacquinDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avaliacao> Avaliacoes { get; set; }

    public virtual DbSet<Categorias> Categorias { get; set; }

    public virtual DbSet<Produtos> Produtos { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D18S20-1252885\\MSSQLSERVER2;Database=BolosDoJacquinDb;User Id=sa;Password=Senai@134;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasIndex(e => e.ProdutoId, "IX_Avaliacoes_ProdutoId");

            entity.HasIndex(e => e.UsuarioId, "IX_Avaliacoes_UsuarioId");

            entity.HasIndex(e => new { e.UsuarioId, e.ProdutoId }, "UQ_Avaliacoes_Usuario_Produto").IsUnique();

            entity.Property(e => e.Comentario).HasMaxLength(1000);
            entity.Property(e => e.DataAlteracao).HasDefaultValueSql("(sysutcdatetime())", "DF_Avaliacoes_DataAlteracao");
            entity.Property(e => e.DataCriacao).HasDefaultValueSql("(sysutcdatetime())", "DF_Avaliacoes_DataCriacao");
            entity.Property(e => e.MotivoOcultacao).HasMaxLength(300);

            entity.HasOne(d => d.Produto).WithMany(p => p.Avaliacos)
                .HasForeignKey(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacoes_Produtos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Avaliacos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacoes_Usuarios");
        });

        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.HasIndex(e => e.Nome, "UQ_Categorias_Nome").IsUnique();

            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<Produtos>(entity =>
        {
            entity.HasIndex(e => e.CategoriaId, "IX_Produtos_CategoriaId");

            entity.Property(e => e.DescricaoCurta).HasMaxLength(300);
            entity.Property(e => e.Disponibilidade).HasDefaultValue(true, "DF_Produtos_Disponibilidade");
            entity.Property(e => e.ImagemUrl).HasMaxLength(500);
            entity.Property(e => e.Nome).HasMaxLength(150);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produtos_Categorias");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.ToTable("TipoUsuario");

            entity.HasIndex(e => e.Nome, "UQ_TipoUsuario_Nome").IsUnique();

            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasIndex(e => e.TipoUsuarioId, "IX_Usuarios_TipoUsuarioId");

            entity.HasIndex(e => e.Email, "UQ_Usuarios_Email").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCadastro).HasDefaultValueSql("(sysutcdatetime())", "DF_Usuarios_DataCadastro");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nome).HasMaxLength(150);

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_TipoUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
