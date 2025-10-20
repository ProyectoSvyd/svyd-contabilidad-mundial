using ContabilidadMundial.Data;
using ContabilidadMundial.Models;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadMundial.Services
{
    public class DataSeederService
    {
        private readonly ContabilidadContext _context;

        public DataSeederService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task SeedInitialDataAsync()
        {
            // Verificar si ya existen datos
            if (await _context.Empresas.AnyAsync())
                return;

            // Crear empresa de ejemplo
            var empresa = new Empresa
            {
                Nombre = "Corporación Global SVYD",
                CodigoFiscal = "SVYD-2025-001",
                MonedaBase = "USD",
                PaisBase = "USA",
                Direccion = "123 Global Street, New York, NY 10001",
                Telefono = "+1-555-123-4567",
                Email = "contacto@svydglobal.com",
                Activa = true,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            // Crear sucursales
            var sucursales = new List<Sucursal>
            {
                new Sucursal
                {
                    EmpresaId = empresa.Id,
                    Nombre = "Oficina Central USA",
                    Codigo = "USA-NYC",
                    CodigoPais = "USA",
                    Pais = "Estados Unidos",
                    Ciudad = "Nueva York",
                    Direccion = "123 Global Street, New York, NY 10001",
                    Telefono = "+1-555-123-4567",
                    Email = "usa@svydglobal.com",
                    MonedaLocal = "USD",
                    ZonaHoraria = "EST",
                    Activa = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Sucursal
                {
                    EmpresaId = empresa.Id,
                    Nombre = "Sucursal Colombia",
                    Codigo = "COL-BOG",
                    CodigoPais = "COL",
                    Pais = "Colombia",
                    Ciudad = "Bogotá",
                    Direccion = "Carrera 15 # 93-47, Bogotá, Colombia",
                    Telefono = "+57-1-555-9876",
                    Email = "colombia@svydglobal.com",
                    MonedaLocal = "COP",
                    ZonaHoraria = "COT",
                    Activa = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Sucursal
                {
                    EmpresaId = empresa.Id,
                    Nombre = "Sucursal España",
                    Codigo = "ESP-MAD",
                    CodigoPais = "ESP",
                    Pais = "España",
                    Ciudad = "Madrid",
                    Direccion = "Calle Gran Vía 28, 28013 Madrid, España",
                    Telefono = "+34-91-555-5432",
                    Email = "espana@svydglobal.com",
                    MonedaLocal = "EUR",
                    ZonaHoraria = "CET",
                    Activa = true,
                    FechaCreacion = DateTime.UtcNow
                }
            };

            _context.Sucursales.AddRange(sucursales);
            await _context.SaveChangesAsync();

            // Crear período contable
            var periodo = new PeriodoContable
            {
                EmpresaId = empresa.Id,
                Codigo = "202501",
                FechaInicio = new DateTime(2025, 1, 1),
                FechaFin = new DateTime(2025, 1, 31),
                Descripcion = "Enero 2025",
                Estado = "Abierto",
                FechaCreacion = DateTime.UtcNow
            };

            _context.PeriodosContables.Add(periodo);

            // Crear algunas cuentas contables básicas
            var cuentas = new List<CuentaContable>
            {
                new CuentaContable
                {
                    EmpresaId = empresa.Id,
                    Codigo = "1000",
                    Nombre = "ACTIVOS",
                    Descripcion = "Cuenta principal de Activos",
                    TipoCuenta = "Activo",
                    Naturaleza = "Débito",
                    Nivel = 1,
                    AceptaMovimientos = false,
                    RequiereCentroCosto = false,
                    Activa = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new CuentaContable
                {
                    EmpresaId = empresa.Id,
                    Codigo = "1100",
                    Nombre = "ACTIVO CORRIENTE",
                    Descripcion = "Activos de corto plazo",
                    TipoCuenta = "Activo",
                    Naturaleza = "Débito",
                    Nivel = 2,
                    AceptaMovimientos = false,
                    RequiereCentroCosto = false,
                    Activa = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new CuentaContable
                {
                    EmpresaId = empresa.Id,
                    Codigo = "1101",
                    Nombre = "EFECTIVO Y EQUIVALENTES",
                    Descripcion = "Caja y bancos",
                    TipoCuenta = "Activo",
                    Naturaleza = "Débito",
                    Nivel = 3,
                    AceptaMovimientos = true,
                    RequiereCentroCosto = false,
                    Moneda = "USD",
                    Activa = true,
                    FechaCreacion = DateTime.UtcNow
                }
            };

            _context.CuentasContables.AddRange(cuentas);

            // Crear tasas de cambio
            var tasas = new List<TasaCambio>
            {
                new TasaCambio
                {
                    MonedaOrigen = "USD",
                    MonedaDestino = "COP",
                    Fecha = DateTime.Today,
                    Tasa = 4200.50m,
                    Fuente = "Banco Central",
                    FechaCreacion = DateTime.UtcNow,
                    CreadoPor = "Sistema"
                },
                new TasaCambio
                {
                    MonedaOrigen = "USD",
                    MonedaDestino = "EUR",
                    Fecha = DateTime.Today,
                    Tasa = 0.85m,
                    Fuente = "Banco Central",
                    FechaCreacion = DateTime.UtcNow,
                    CreadoPor = "Sistema"
                }
            };

            _context.TasasCambio.AddRange(tasas);

            await _context.SaveChangesAsync();
        }
    }
}