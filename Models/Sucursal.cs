using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadMundial.Models
{
    /// <summary>
    /// Representa una sucursal de una empresa en diferentes países
    /// </summary>
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(3)]
        public string CodigoPais { get; set; } = string.Empty; // ISO 3166-1 alpha-3

        [Required]
        [StringLength(100)]
        public string Pais { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Ciudad { get; set; }

        [StringLength(500)]
        public string? Direccion { get; set; }

        [StringLength(50)]
        public string? Telefono { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        [StringLength(3)]
        public string MonedaLocal { get; set; } = string.Empty; // ISO 4217

        [StringLength(10)]
        public string? ZonaHoraria { get; set; } // Ej: UTC-05:00

        public bool Activa { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacion { get; set; }

        // Propiedades de navegación
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual ICollection<CuentaContable> CuentasContables { get; set; } = new HashSet<CuentaContable>();
        public virtual ICollection<AsientoContable> AsientosContables { get; set; } = new HashSet<AsientoContable>();
    }
}