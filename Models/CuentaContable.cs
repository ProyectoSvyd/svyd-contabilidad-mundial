using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadMundial.Models
{
    /// <summary>
    /// Representa una cuenta contable en el plan de cuentas
    /// </summary>
    public class CuentaContable
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

        /// <summary>
        /// Tipo de cuenta: Activo, Pasivo, Patrimonio, Ingresos, Gastos
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TipoCuenta { get; set; } = string.Empty;

        /// <summary>
        /// Naturaleza de la cuenta: Deudora, Acreedora
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Naturaleza { get; set; } = string.Empty;

        /// <summary>
        /// Nivel en la jerarquía del plan de cuentas (1=Mayor, 2=Sub-mayor, etc.)
        /// </summary>
        public int Nivel { get; set; } = 1;

        /// <summary>
        /// ID de la cuenta padre en la jerarquía
        /// </summary>
        [ForeignKey(nameof(CuentaPadre))]
        public int? CuentaPadreId { get; set; }

        /// <summary>
        /// Indica si la cuenta acepta movimientos directos
        /// </summary>
        public bool AceptaMovimientos { get; set; } = true;

        /// <summary>
        /// Indica si la cuenta requiere centro de costos
        /// </summary>
        public bool RequiereCentroCosto { get; set; } = false;

        /// <summary>
        /// Moneda de la cuenta (para cuentas en moneda extranjera)
        /// </summary>
        [StringLength(3)]
        public string? Moneda { get; set; }

        public bool Activa { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacion { get; set; }

        // Propiedades de navegación
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Sucursal? Sucursal { get; set; }
        public virtual CuentaContable? CuentaPadre { get; set; }
        public virtual ICollection<CuentaContable> CuentasHijas { get; set; } = new HashSet<CuentaContable>();
        public virtual ICollection<DetalleAsiento> DetallesAsientos { get; set; } = new HashSet<DetalleAsiento>();
    }
}