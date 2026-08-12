====================================================================
CARPETA: Migrations (Migraciones de Entity Framework Core)
====================================================================
Propósito:
Almacena el código C# autogenerado por la CLI de Entity Framework Core que registra el historial de cambios en el esquema de la base de datos.

¿Cómo se generan?
- Ejecutando desde la consola el comando:
  `dotnet ef migrations add NombreDeLaMigracion --project src/RestoApp.Data --startup-project src/RestoApp.Presentation`

REGLAS PARA EL EQUIPO:
- NUNCA editar los archivos de esta carpeta manualmente a menos que sea estrictamente necesario. Se gestionan mediante la herramienta CLI de EF Core.
- Todo el equipo debe tener aplicadas las mismas migraciones ejecutando `dotnet ef database update`.
