====================================================================
CARPETA: Configurations (Mapeos con Fluent API)
====================================================================
Propósito:
Almacena clases de configuración individuales que implementan `IEntityTypeConfiguration<T>`.

¿Qué archivos van aquí?
- PersonaConfiguration.cs, ReservaConfiguration.cs, PagoConfiguration.cs.

REGLAS PARA EL EQUIPO:
- En lugar de saturar `RestoDbContext.cs` con cientos de líneas en `OnModelCreating`, colocamos aquí la configuración específica de claves primarias, claves foráneas, restricciones de longitud (`HasMaxLength`), índices únicos y tipos de precisión decimal (`HasPrecision(18, 2)`).
