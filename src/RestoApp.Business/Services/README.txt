====================================================================
CARPETA: Services (Servicios de Lógica de Negocio)
====================================================================
Propósito:
Contiene la implementación de los casos de uso del sistema. Aquí se programa la lógica real del restaurante.

¿Qué archivos van aquí?
- Clases que implementan las interfaces de negocio.
- Ejemplos: AuthService.cs, ReservaService.cs, MesaService.cs, PersonalService.cs.

REGLAS PARA EL EQUIPO:
1. Toda la validación de negocio (ej. "no permitir reservar una mesa si ya está ocupada en ese rango horario" o "verificar si un empleado tiene rol de admin") va dentro de estas clases.
2. Los servicios consultan la base de datos a través de EF Core (DbContext o Repositorios) inyectados por constructor.
