====================================================================
CAPA DE NEGOCIO (RestoApp.Business)
====================================================================
Propósito:
Esta capa contiene la lógica central del dominio y las reglas de negocio del sistema del restaurante (gestión de reservas, asignación de mesas, validaciones de turnos y permisos de usuarios).

REGLAS DE ARQUITECTURA:
1. Esta capa NO conoce ni debe tener referencias a Avalonia UI ni a nada visual.
2. Contiene las interfaces (contratos) y las implementaciones de los servicios.
3. Utiliza los DTOs para transferir información hacia la Capa de Presentación.

Estructura interna:
- /Services    : Implementación concreta de la lógica de negocio (Servicios).
- /Interfaces  : Contratos e interfaces (IAuthService, IReservaService).
- /DTOs        : Data Transfer Objects (Objetos para transporte de datos entre capas).
