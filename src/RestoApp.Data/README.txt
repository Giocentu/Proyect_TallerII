====================================================================
CAPA DE ACCESO A DATOS (RestoApp.Data)
====================================================================
Propósito:
Esta capa encapsula la comunicación física con la base de datos SQL Server ejecutándose en Docker mediante Entity Framework Core (Code-First).

REGLAS DE ARQUITECTURA:
1. Contiene las Entidades del dominio mapeadas directamente a tablas de la BD.
2. Contiene el DbContext (`RestoDbContext.cs`).
3. Contiene la configuración de Fluent API y el historial de migraciones.

Estructura interna:
- /Context        : RestoDbContext.cs (Conexión y DbSets de EF Core).
- /Models         : Entidades de C# que mapean a las tablas de SQL (Persona.cs, Mesa.cs, Reserva.cs, etc.).
- /Configurations : Clases de configuración de Fluent API (IEntityTypeConfiguration).
- /Migrations     : Historial de migraciones automáticas de EF Core.
