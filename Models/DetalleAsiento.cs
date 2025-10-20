using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadMundial.Models
{
    /// <summary>
    /// Representa el detalle de un asiento contable (líneas del asiento)
    /// </summary>
    public class DetalleAsiento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(AsientoContable))]
        public int AsientoId { get; set; }

        [Required]
        [ForeignKey(nameof(CuentaContable))]
        public int CuentaContableId { get; set; }

        /// <summary>
        /// Número de línea dentro del asiento
        /// </summary>
        public int NumeroLinea { get; set; }

        [StringLength(500)]
        public string? Concepto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Debito { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Credito { get; set; } = 0;

        /// <summary>
        /// Moneda del movimiento (si es diferente a la base)
        /// </summary>
        [StringLength(3)]
        public string? Moneda { get; set; }

        /// <summary>
        /// Tasa de cambio aplicada
        /// </summary>
        [Column(TypeName = "decimal(18,6)")]
        public decimal? TasaCambio { get; set; }

        /// <summary>
        /// Valor en moneda extranjera (débito)
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DebitoMonedaExtranjera { get; set; }

        /// <summary>
        /// Valor en moneda extranjera (crédito)
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CreditoMonedaExtranjera { get; set; }

        [StringLength(50)]
        public string? CentroCosto { get; set; }

        [StringLength(50)]
        public string? DocumentoReferencia { get; set; }

        [StringLength(200)]
        public string? TerceroReferencia { get; set; }

        public DateTime? FechaDocumento { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual AsientoContable AsientoContable { get; set; } = null!;
        public virtual CuentaContable CuentaContable { get; set; } = null!;
    }
}