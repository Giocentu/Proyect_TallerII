====================================================================
CARPETA: Interfaces (Contratos de Servicios)
====================================================================
Propósito:
Define las interfaces de C# que abstraen los servicios de la aplicación.

¿Qué archivos van aquí?
- Interfaces que declaran los métodos públicos de negocio.
- Ejemplos: IAuthService.cs, IReservaService.cs, IMesaService.cs.

REGLAS PARA EL EQUIPO:
- Las Vistas y ViewModels de Avalonia NUNCA instancian servicios directamente con `new AuthService()`. Siempre dependen de la interfaz `IAuthService` proporcionada por Inyección de Dependencias.
- Esto facilita la creación de pruebas unitarias y el desacoplamiento de capas.
