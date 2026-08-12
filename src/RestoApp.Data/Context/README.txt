====================================================================
CARPETA: Context (DbContext de Entity Framework Core)
====================================================================
Propósito:
Almacena la clase principal que representa la sesión de trabajo con la base de datos SQL Server.

¿Qué archivos van aquí?
- RestoDbContext.cs (hereda de DbContext de Microsoft.EntityFrameworkCore).

REGLAS PARA EL EQUIPO:
- Aquí se declaran los `DbSet<T>` para cada entidad (ej. `public DbSet<Reserva> Reservas { get; set; }`).
- En el método `OnModelCreating`, se registran las configuraciones de Fluent API.
