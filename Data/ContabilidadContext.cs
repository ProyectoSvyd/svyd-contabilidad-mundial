using Microsoft.EntityFrameworkCore;
using ContabilidadMundial.Models;

namespace ContabilidadMundial.Data
{
    /// <summary>
    /// Contexto de base de datos para el sistema de contabilidad mundial
    /// </summary>
    public class ContabilidadContext : DbContext
    {
        public ContabilidadContext(DbContextOptions<ContabilidadContext> options) : base(options)
        {
        }

        // DbSets para las entidades principales
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<CuentaContable> CuentasContables { get; set; }
        public DbSet<AsientoContable> AsientosContables { get; set; }
        public DbSet<DetalleAsiento> DetallesAsientos { get; set; }
        public DbSet<TasaCambio> TasasCambio { get; set; }
        public DbSet<PeriodoContable> PeriodosContables { get; set; }
        public DbSet<CentroCosto> CentrosCosto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Empresa
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasIndex(e => e.CodigoFiscal).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configuración de Sucursal
            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasIndex(e => new { e.EmpresaId, e.Codigo }).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasOne(s => s.Empresa)
                      .WithMany(e => e.Sucursales)
                      .HasForeignKey(s => s.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de CuentaContable
            modelBuilder.Entity<CuentaContable>(entity =>
            {
                entity.HasIndex(e => new { e.EmpresaId, e.Codigo }).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasOne(c => c.Empresa)
                      .WithMany(e => e.CuentasContables)
                      .HasForeignKey(c => c.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Sucursal)
                      .WithMany(s => s.CuentasContables)
                      .HasForeignKey(c => c.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.CuentaPadre)
                      .WithMany(c => c.CuentasHijas)
                      .HasForeignKey(c => c.CuentaPadreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de AsientoContable
            modelBuilder.Entity<AsientoContable>(entity =>
            {
                entity.HasIndex(e => new { e.SucursalId, e.Numero }).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasOne(a => a.Empresa)
                      .WithMany()
                      .HasForeignKey(a => a.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Sucursal)
                      .WithMany(s => s.AsientosContables)
                      .HasForeignKey(a => a.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de DetalleAsiento
            modelBuilder.Entity<DetalleAsiento>(entity =>
            {
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasOne(d => d.AsientoContable)
                      .WithMany(a => a.Detalles)
                      .HasForeignKey(d => d.AsientoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.CuentaContable)
                      .WithMany(c => c.DetallesAsientos)
                      .HasForeignKey(d => d.CuentaContableId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de TasaCambio
            modelBuilder.Entity<TasaCambio>(entity =>
            {
                entity.HasIndex(e => new { e.MonedaOrigen, e.MonedaDestino, e.Fecha }).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configuración de PeriodoContable
            modelBuilder.Entity<PeriodoContable>(entity =>
            {
                entity.HasIndex(e => new { e.EmpresaId, e.Codigo }).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasOne(p => p.Empresa)
                      .WithMany()
                      .HasForeignKey(p => p.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de CentroCosto
            modelBuilder.Entity<CentroCosto>(entity =>
            {
                entity.HasIndex(e => new { e.EmpresaId, e.Codigo }).IsUnique();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasOne(c => c.Empresa)
                      .WithMany()
                      .HasForeignKey(c => c.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Sucursal)
                      .WithMany()
                      .HasForeignKey(c => c.SucursalId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.CentroPadre)
                      .WithMany(c => c.CentrosHijos)
                      .HasForeignKey(c => c.CentroPadreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}