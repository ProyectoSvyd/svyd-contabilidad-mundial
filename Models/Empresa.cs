using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadMundial.Models
{
    /// <summary>
    /// Representa una empresa multinacional en el sistema de contabilidad
    /// </summary>
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string CodigoFiscal { get; set; } = string.Empty;

        [StringLength(3)]
        public string MonedaBase { get; set; } = "USD"; // ISO 4217

        [StringLength(3)]
        public string PaisBase { get; set; } = string.Empty; // ISO 3166-1

        [StringLength(500)]
        public string? Direccion { get; set; }

        [StringLength(50)]
        public string? Telefono { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        public bool Activa { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacion { get; set; }

        // Propiedades de navegación
        public virtual ICollection<Sucursal> Sucursales { get; set; } = new HashSet<Sucursal>();
        public virtual ICollection<CuentaContable> CuentasContables { get; set; } = new HashSet<CuentaContable>();
    }
}