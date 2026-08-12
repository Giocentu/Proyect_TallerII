====================================================================
CARPETA: Models (Entidades de la Base de Datos)
====================================================================
Propósito:
Contiene las clases POCO (Plain Old C# Objects) que representan exactamente las tablas y relaciones del script `create_mejorado.sql`.

¿Qué archivos van aquí?
- Persona.cs, Cliente.cs, Empleado.cs, Mesa.cs, Reserva.cs, Pago.cs, RolEmpleado.cs, TurnoEmpleado.cs, EstadoReserva.cs, UbicacionMesa.cs, Evento.cs.
- UsuarioSesion.cs.

REGLAS PARA EL EQUIPO:
- Cada propiedad en estas clases mapea a una columna en SQL.
- Las propiedades de navegación (ej. `public virtual Persona Persona { get; set; }`) permiten a EF Core realizar JOINs automáticamente mediante el método `.Include()`.
