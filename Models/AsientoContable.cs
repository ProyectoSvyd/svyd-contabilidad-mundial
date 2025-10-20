using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadMundial.Models
{
    /// <summary>
    /// Representa un asiento contable (cabecera)
    /// </summary>
    public class AsientoContable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }

        [Required]
        [ForeignKey(nameof(Sucursal))]
        public int SucursalId { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(500)]
        public string Concepto { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Observaciones { get; set; }

        /// <summary>
        /// Tipo de asiento: Manual, Automatico, Cierre, Apertura
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TipoAsiento { get; set; } = "Manual";

        /// <summary>
        /// Estado del asiento: Borrador, Contabilizado, Anulado
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Borrador";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDebito { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCredito { get; set; } = 0;

        /// <summary>
        /// Período contable (YYYYMM)
        /// </summary>
        [Required]
        [StringLength(6)]
        public string PeriodoContable { get; set; } = string.Empty;

        [StringLength(50)]
        public string? NumeroDocumento { get; set; }

        [StringLength(100)]
        public string? TerceroReferencia { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaContabilizacion { get; set; }

        [Required]
        [StringLength(100)]
        public string CreadoPor { get; set; } = string.Empty;

        [StringLength(100)]
        public string? ContabilizadoPor { get; set; }

        // Propiedades de navegación
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Sucursal Sucursal { get; set; } = null!;
        public virtual ICollection<DetalleAsiento> Detalles { get; set; } = new HashSet<DetalleAsiento>();
    }
}