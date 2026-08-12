====================================================================
CARPETA: DTOs (Data Transfer Objects)
====================================================================
Propósito:
Objetos simples de transferencia de datos diseñados para mover información entre la UI (Presentación) y la Capa de Negocio.

¿Qué archivos van aquí?
- Clases livianas compuestas principalmente por propiedades auto-implementadas.
- Ejemplos: LoginRequestDto.cs, ReservaCreacionDto.cs, EmpleadoResumenDto.cs.

REGLAS PARA EL EQUIPO:
- Evitar pasar entidades completas de Entity Framework a la interfaz de usuario si contienen datos sensibles o referencias circulares. Usar DTOs en su lugar.
