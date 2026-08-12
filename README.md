# Proyect_TallerII
Proyecto para la materia Taller II de la UNNE. Este proyecto consiste en crear una aplicacion ejecutable de un sistema gestor de  mesas,reservas y turnos de empleados
## Descripción del Trabajo

Nuestro proyecto consistirá en una base de datos enfocada a la administración y control de un bar/restaurante/local de comidas, la idea surge desde la experiencia de algunos alumnos del grupo que hemos podido trabajar en estos locales y presenciamos la precariedad de algunos de estos sistemas usados.

El objetivo general es realizar una base de datos para poder subsanar estos problemas y ofrecer un mejor manejo, consistencia y seguridad de datos a dichos emprendimientos.

Otros objetivos que queremos cumplir es poder ofrecer distintos tipos de perfiles para el manejo de la base de datos, cada uno con diferentes permisos, funciones y tipos de accesos. Mejorar la gestión de mesas, de clientes, de empleados y mejorar el registro de pagos.

## Alcance del Proyecto

El alcance del sistema está definido por las entidades y relaciones del esquema, cubriendo las siguientes áreas funcionales:

### 1. Gestión Integral de Reservas
- **Creación y Modificación**: Registrar y actualizar reservas asociadas a un Cliente (DNI, nombre, contacto) con fecha, hora y número de personas.
- **Asignación de Mesas**: Gestionar la asignación de una o varias Mesas a una reserva, considerando la capacidad y la UbicacionMesa (ej., terraza, salón).
- **Control de Estado**: Seguimiento del estado de la reserva a través de Estado_reserva (ej., confirmada, cancelada, completada).
- **Manejo de Cancelaciones**: Registro de las políticas o instancias de anulación en HorarioCancelacion.

### 2. Gestión de Mesas y Disponibilidad
- **Inventario**: Mantenimiento del catálogo de Mesas y sus características (número, capacidad).
- **Distribución**: Clasificación de mesas según su UbicacionMesa.
- **Disponibilidad en Tiempo Real**: Proveer información sobre qué mesas están libres u ocupadas en un momento dado, basándose en los registros de Reserva_Mesa.

### 3. Gestión de Clientes y Comunicación
- **Registro Central**: Almacenar datos básicos (Cliente).
- **Datos de Contacto**: Gestionar múltiples contactos por cliente (EmailCliente, TelefonoCliente).

### 4. Eventos y Promociones
- **Definición de Eventos**: Registro de EventosEspeciales (nombre, descripción).
- **Vinculación**: Asociación de reservas específicas con un evento mediante ReservaEvento.

### 5. Transacciones y Pagos
- **Registro de Pagos**: Documentación de cada pago realizado para una reserva (Pagos), incluyendo el monto y la fecha_pago.
- **Métodos de Pago**: Clasificación de las transacciones por MetodoPago (ej., tarjeta, efectivo).
- **Trazabilidad**: Identificación del Empleado responsable de procesar la transacción.

### 6. Gestión Básica de Personal y Turnos
- **Identificación**: Registro del Empleado (DNI, nombre).
- **Roles**: Asignación de funciones mediante RolEmpleado.
- **Horarios**: Planificación y registro de TurnoEmpleado (fecha, hora inicio/fin) para el personal.

## Exclusiones (Lo que NO está en el alcance)
- **Punto de Venta (POS)**: El sistema no abarca la gestión detallada de órdenes, menús, ingredientes, inventario de cocina o facturación fiscal compleja.
- **Nómina y Recursos Humanos**: No incluye el cálculo de sueldos, impuestos, vacaciones detalladas, o la gestión completa de RR.HH. del personal.
- **Marketing/Fidelización**: No incluye módulos avanzados de CRM, envío automatizado de correos de marketing o programas de lealtad.
- **OWO**:hernan
