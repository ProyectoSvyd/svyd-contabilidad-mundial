using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadMundial.Models
{
    /// <summary>
    /// Representa las tasas de cambio entre monedas por fecha
    /// </summary>
    public class TasaCambio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string MonedaOrigen { get; set; } = string.Empty; // ISO 4217

        [Required]
        [StringLength(3)]
        public string MonedaDestino { get; set; } = string.Empty; // ISO 4217

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal Tasa { get; set; }

        [StringLength(50)]
        public string? Fuente { get; set; } // Banco Central, API, Manual, etc.

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(100)]
        public string CreadoPor { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa los períodos contables de una empresa
    /// </summary>
    public class PeriodoContable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }

        [Required]
        [StringLength(6)]
        public string Codigo { get; set; } = string.Empty; // YYYYMM

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Estado del período: Abierto, Cerrado, Bloqueado
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Abierto";

        public DateTime? FechaCierre { get; set; }

        [StringLength(100)]
        public string? CerradoPor { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual Empresa Empresa { get; set; } = null!;
    }

    /// <summary>
    /// Representa los centros de costo de una empresa
    /// </summary>
    public class CentroCosto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }

        [ForeignKey(nameof(Sucursal))]
        public int? SucursalId { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [ForeignKey(nameof(CentroPadre))]
        public int? CentroPadreId { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Sucursal? Sucursal { get; set; }
        public virtual CentroCosto? CentroPadre { get; set; }
        public virtual ICollection<CentroCosto> CentrosHijos { get; set; } = new HashSet<CentroCosto>();
    }
}