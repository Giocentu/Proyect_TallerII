# Especificación de Requisitos de Software (ERS) - RestoApp
---
## 1. Introducción

### 1.1 Propósito
El propósito de este documento es definir de forma clara, precisa y estandarizada la **Especificación de Requisitos de Software (ERS)** para el sistema **RestoApp**. Este documento servirá como contrato de desarrollo entre el equipo de diseño/desarrollo y los colaboradores del establecimiento gastronómico, estableciendo tanto las capacidades funcionales como las restricciones y los atributos de calidad del sistema.

### 1.2 Alcance del Sistema
**RestoApp** es un sistema de escritorio integral orientado a la administración operativa de bares, restaurantes y locales gastronómicos. El sistema resuelve la precariedad de los métodos tradicionales ofreciendo:
- Gestión centralizada de personas (clientes y empleados).
- Control de perfiles y seguridad basada en roles (RBAC).
- Gestión de disponibilidad de mesas y sectores en tiempo real.
- Administración integral de reservas simples y grupales.
- Gestión de personal, roles y turnos de trabajo.
- Vinculación de eventos especiales.
- Control de caja, transacciones y medios de pago.

### 1.3 Definiciones, Acrónimos y Abreviaturas
- **ERS:** Especificación de Requisitos de Software.
- **RBAC:** *Role-Based Access Control* (Control de Acceso Basado en Roles).
- **DNI:** Documento Nacional de Identidad.
- **MVVM:** *Model-View-ViewModel* (Patrón arquitectónico de interfaz de usuario).
- **EF Core:** Entity Framework Core (ORM para persistencia en .NET).
- **MSSQL:** Microsoft SQL Server.
- **Docker:** Plataforma de contenedorización para la base de datos.
- **POS:** *Point of Sale* (Punto de Venta gastronómico completo).
---

## 2. Descripción General

### 2.1 Perspectiva del Producto
**RestoApp** es un sistema autónomo de arquitectura multicapa (.NET C#) diseñado para ejecutarse en entornos de escritorio cross-platform (Linux/Windows) mediante **Avalonia UI**, conectado a un motor de base de datos **MSSQL** desplegado en un contenedor **Docker**.

```
+-------------------------------------------------------------+
|               RestoApp.Presentation (Avalonia UI)            |
|                  Vistas XAML + ViewModels MVVM              |
+-------------------------------------------------------------+
                              |
                              v
+-------------------------------------------------------------+
|                RestoApp.Business (Capas de Servicio)         |
|              Reglas de Negocio, DTOs, Validaciones          |
+-------------------------------------------------------------+
                              |
                              v
+-------------------------------------------------------------+
|                RestoApp.Data (Capa de Acceso a Datos)       |
|             EF Core / SqlClient, Modelos y Mapeos           |
+-------------------------------------------------------------+
                              |
                              v
+-------------------------------------------------------------+
|              Base de Datos MSSQL (Docker Container)          |
|                       `proyect_Resto`                       |
+-------------------------------------------------------------+
```

### 2.2 Funcionalidades Principales del Producto
1. **Administración de Usuarios y Perfiles:** Registro, autenticación mediante DNI/contraseña (hashing seguro), control de permisos.
2. **Gestión de Personal y Turnos:** Asignación de roles y franjas horarias (`inicio_turno`, `fin_turno`) con control de estado activo (`activo_en_rol`).
3. **Gestión de Mesas y Distribución:** Distribución por sectores (`ubicacion_mesa`), capacidad de comensales y disponibilidad en tiempo real.
4. **Gestión de Clientes y Reservas:** Padrón de clientes con contacto único; reservas asociadas a una o varias mesas, fecha/hora, número de personas y política de cancelación automática.
5. **Gestión de Eventos Especiales:** Registro de fechas festivas o promociones y su vinculación con reservas específicas.
6. **Gestión de Transacciones y Medios de Pago:** Registro de cobros (`Pagos`) asociados a reservas, especificando monto, fecha y método de pago (Efectivo, Tarjeta, Transferencia).
7. **Control de Caja:** Registro de apertura, arqueo de transacciones por empleado y cierre de caja.

### 2.3 Características de los Usuarios y Matriz de Roles (RBAC)

El sistema implementa un control de acceso estricto según la función asignada al empleado:

| Funcionalidad / Módulo | Dueño | Gerente | Recepcionista | Cajero |
| :--- | :---: | :---: | :---: | :---: |
| **Gestión de Usuarios (Crear/Editar)** | ✅ Todos | ✅ Excepto Gerente/Dueño | ❌ | ❌ |
| **Asignación de Turnos y Roles** | ✅ | ✅ | ❌ | ❌ |
| **Gestión de Mesas y Ubicaciones** | ✅ | ✅ | 👁️ Lectura | 👁️ Lectura |
| **Crear y Modificar Reservas** | ✅ | ✅ | ✅ | ❌ |
| **Vinculación con Eventos** | ✅ | ✅ | ✅ | ❌ |
| **Registrar Pagos y Cuentas** | ✅ | ✅ | ❌ | ✅ |
| **Apertura y Cierre de Caja** | ✅ | ✅ | ❌ | ✅ |
| **Consulta de Estado de Mesas** | ✅ | ✅ | ✅ | ✅ |

### 2.4 Entorno Operativo
- **Sistema Operativo Cliente:** Linux (Ubuntu 22.04 LTS o superior) / Microsoft Windows 10/11.
- **Entorno de Ejecución:** .NET 8.0 Runtime o superior.
- **Base de Datos:** MSSQL Server 2022 o superior instanciado sobre Docker Linux Container (`mcr.microsoft.com/mssql/server:2022-latest`).
- **Resolución de Pantalla Mínima:** 1024 x 768 píxeles (optimizado para pantallas táctiles de escritorio en caja/recepción).

### 2.5 Restricciones de Diseño e Implementación
- La aplicación **debe utilizar exclusivamente Avalonia UI** con patrón MVVM en el frontend.
- El backend se estructurará mediante la solución multicapa definida (`RestoApp.Presentation`, `RestoApp.Business`, `RestoApp.Data`).
- La autenticación utilizará contraseñas encriptadas con algoritmos de hashing seguro (BCrypt/Identity) almacenadas en un campo `NVARCHAR(255)`.
- El acceso está restringido al personal del establecimiento (sin interfaz pública dirigida a clientes externos).

### 2.6 Suposiciones y Dependencias
- Se asume que el local cuenta con una red de área local (LAN) estable para conectar la estación de trabajo desktop con el servidor/contenedor de la base de datos MSSQL.
- Se asume que el Dueño o Gerente cargará inicialmente el inventario de mesas y el personal habilitado.

---

## 3. Requisitos Funcionales 

### RF-01: Gestión de Usuarios, Autenticación y Perfiles
- **RF-01.1 Login de Usuarios:** El sistema debe permitir el inicio de sesión del personal ingresando su DNI y contraseña.
- **RF-01.2 Autenticación Segura:** El sistema debe validar la contraseña comparando el hash BCrypt contra el registro de la tabla `persona`.
- **RF-01.3 Verificación de Estado Activo:** Solo se permitirá el acceso a aquellos empleados cuyo registro tenga `activo_en_rol = 1`.
- **RF-01.4 Cierre de Sesión:** El sistema debe proveer una opción visible en todo momento para cerrar la sesión activa y retornar a la pantalla de Login.

### RF-02: Gestión de Personal, Roles y Turnos
- **RF-02.1 Alta y Edición de Personal:** El Dueño o Gerente podrá dar de alta nuevos empleados registrando sus datos personales (`DNI`, `Nombre`, `Apellido`, `Email`, `Teléfono`).
- **RF-02.2 Asignación de Roles:** Asignar un rol específico de la tabla `rol_empleado` (`Dueño`, `Gerente`, `Recepcionista`, `Cajero`, `Mozo`).
- **RF-02.3 Regla de Rol Activo Único:** El sistema debe garantizar (mediante restricción de índice SQL) que un mismo DNI posea únicamente un (1) rol activo al mismo tiempo.
- **RF-02.4 Planificación de Turnos:** Registrar franjas horarias en `turno_empleado` (`inicio_turno`, `fin_turno`) y vincularlas a los empleados.

### RF-03: Gestión de Mesas y Ubicaciones
- **RF-03.1 Registro de Sectores:** Crear y editar ubicaciones físicas (`ubicacion_mesa`, ej.: *Salón Principal*, *Terraza*, *Barra*, *VIP*).
- **RF-03.2 Catálogo de Mesas:** Definir mesas con su número identificador (`nro_mesa`), capacidad máxima de personas (`capacidad`) y sector asignado.
- **RF-03.3 Estado de Disponibilidad en Tiempo Real:** Visualizar de forma gráfica/colorimétrica el estado operativo de cada mesa (*Libre*, *Reservada*, *Ocupada*, *En Limpieza/Inhabilitada*).

### RF-04: Gestión Centralizada de Clientes
- **RF-04.1 Registro de Clientes:** Almacenar la información de los clientes (`DNI`, `Nombre`, `Apellido`, `Email`, `Teléfono`).
- **RF-04.2 Control de Duplicados:** Garantizar que no se registren clientes duplicados por DNI, Email o Teléfono.
- **RF-04.3 Historial de Reservas:** Consultar las reservas históricas asociadas a un cliente específico mediante su DNI.

### RF-05: Gestión Integral de Reservas
- **RF-05.1 Creación de Reserva:** El Recepcionista/Gerente/Dueño podrá registrar una reserva indicando Cliente (DNI), Fecha y Hora, Cantidad de Personas y Empleado responsable de la carga.
- **RF-05.2 Asignación de Mesas (Simple y Grupal):** El sistema debe permitir asociar una o múltiples mesas (`reserva_mesa`) a una misma reserva para cubrir la cantidad de comensales requerida.
- **RF-05.3 Validación de Capacidad:** El sistema debe advertir o impedir si la capacidad combinada de las mesas asignadas es menor a la cantidad de personas de la reserva.
- **RF-05.4 Control de Estados de Reserva:** Actualizar el estado de la reserva (`confirmada`, `en_espera`, `cancelada`, `completada`).
- **RF-05.5 Cálculo Automático de Límite de Cancelación:** El sistema calculará automáticamente la `fecha_max_cancelacion` restando 48 horas a la fecha fijada para la reserva.

### RF-06: Gestión de Eventos Especiales
- **RF-06.1 Catálogo de Eventos:** Definir eventos o fechas especiales (`evento`, ej.: *Día del Amigo*, *Año Nuevo*, *Cena Show*).
- **RF-06.2 Vinculación Opcional:** Permitir asociar una reserva a un evento especial activo mediante el identificador `id_evento`.

### RF-07: Transacciones y Pagos
- **RF-07.1 Registro de Pagos:** Registrar el cobro final de una reserva/cuenta indicando el monto total (`monto`), fecha/hora y el empleado (Cajero/Gerente/Dueño) que procesó el pago.
- **RF-07.2 Métodos de Pago:** Seleccionar la forma de cobro a través del catálogo `metodo_pago` (*Efectivo*, *Tarjeta de Débito*, *Tarjeta de Crédito*, *Transferencia MP/QR*).
- **RF-07.3 Unicidad de Pago por Reserva:** Garantizar que cada reserva liquidada posea su comprobante de pago único registrado.

### RF-08: Control de Caja
- **RF-08.1 Apertura de Caja:** Permitir al Cajero/Gerente registrar el monto inicial de efectivo disponible al inicio de la jornada.
- **RF-08.2 Balance de Turno (Arqueo):** Sumarizar los ingresos agrupados por método de pago durante el turno activo.
- **RF-08.3 Cierre de Caja:** Registrar la finalización del turno de caja, calculando automáticamente discrepancias entre el efectivo esperado y el contado.

---

## 4. Requisitos No Funcionales (RNF)

### RNF-01: Seguridad y Control de Acceso
- **RNF-01.1 Encriptación de Contraseñas:** Las contraseñas de los usuarios deben almacenarse encriptadas mediante algoritmos de hashing unidireccional con sal (*salting*) como **BCrypt** o **ASP.NET Core Identity PasswordHasher**. En ningún caso se almacenará texto plano.
- **RNF-01.2 Control Basado en Roles (RBAC):** La interfaz de usuario debe ocultar o deshabilitar automáticamente los módulos y botones que no correspondan a los permisos del rol autenticado.
- **RNF-01.3 Aislamiento de Red:** La base de datos MSSQL solo aceptará conexiones locales o desde la subred autorizada por Docker.

### RNF-02: Rendimiento y Tiempos de Respuesta
- **RNF-02.1 Tiempo de Respuesta de Consultas:** Las búsquedas de reservas, verificación de disponibilidad de mesas y login de usuarios deben responder en **menos de 1 segundo** bajo carga normal de trabajo.
- **RNF-02.2 Tiempo de Carga de la Aplicación:** El inicio de la aplicación de escritorio Avalonia UI no debe exceder los **3 segundos** en hardware estándar.

### RNF-03: Usabilidad e Interfaz de Usuario
- **RNF-03.1 Diseño Adaptativo Desktop:** La interfaz XAML de Avalonia UI utilizará una paleta cromática profesional de alto contraste, botones de tamaño adecuado (mínimo 44x44 px) aptos para pantallas táctiles y navegación por teclado.
- **RNF-03.2 Retroalimentación Visual:** El sistema debe mostrar indicadores claros (mensajes de confirmación, diálogos de error informativos) ante cualquier acción realizada por el usuario.

### RNF-04: Integridad, Concurrencia y Persistencia
- **RNF-04.1 Integridad Referencial:** La base de datos debe aplicar restricciones *FOREIGN KEY*, *CHECK* y *UNIQUE* en las entidades críticas (`persona`, `mesa`, `reserva`, `pagos`).
- **RNF-04.2 Prevención de Sobrereserva:** El sistema debe utilizar transacciones SQL para evitar la asignación simultánea de la misma mesa a dos reservas en solapamiento horario.
- **RNF-04.3 Persistencia en Docker:** La base de datos alojada en Docker debe utilizar volúmenes persistentes (`named volumes`) para evitar la pérdida de datos ante reinicios del contenedor.

### RNF-05: Mantenibilidad y Arquitectura de Software
- **RNF-05.1 Separación en Capas:** El código fuente respetará una arquitectura desacoplada:
  - `RestoApp.Presentation`: Capa gráfica en Avalonia UI con patrón MVVM.
  - `RestoApp.Business`: Capa de lógica de negocio y validación de reglas.
  - `RestoApp.Data`: Capa de acceso a datos utilizando EF Core y SqlClient.
- **RNF-05.2 Estándar de Código:** Código documentado en C# siguiendo las guías oficiales de Microsoft .NET.

---

## 5. Reglas de Negocio (RN)

- **RN-01 (Política de Cancelación):** Una reserva solo podrá ser cancelada sin penalidad si la solicitud se efectúa antes de la `fecha_max_cancelacion` (48 horas previas al turno de la reserva).
- **RN-02 (Unicidad de Persona y Contacto):** No se pueden registrar dos personas con el mismo DNI, Email o Teléfono (`uq_persona_email`, `UQ_Persona_Tel`).
- **RN-03 (Jerarquía de Creación de Usuarios):**
  - El **Dueño** puede crear usuarios con cualquier rol (`Dueño`, `Gerente`, `Recepcionista`, `Cajero`, `Mozo`).
  - El **Gerente** puede crear usuarios excepto con rol `Gerente` o `Dueño`.
  - Los demás roles no poseen permisos de administración de usuarios.
- **RN-04 (Restricción de Capacidad):** Una reserva no podrá ser confirmada si la suma de capacidades de las mesas asignadas en `reserva_mesa` es inferior a `cant_personas`.
- **RN-05 (Cierre de Reserva por Pago):** Una reserva pasa automáticamente al estado `completada` una vez registrado su correspondiente pago en la tabla `pagos`.

---

## 6. Delimitación y Alcance Futuro 

### 6.1 Exclusiones del Alcance Actual 
- **Punto de Venta de Cocina (POS de Comandas):** No se incluye comandeo detallado a cocina.
- **Nómina y Recursos Humanos:** No incluye cálculo de liquidación de sueldos, aportes ni gestión de licencias.
- **Facturación Fiscal Compleja:** No incluye emisión directa de comprobantes electrónicos AFIP/Fiscales.

### 6.2 Alcance Futuro

1. **Módulo de Menú y Carta Digital:** Registro del catálogo de platos, bebidas, precios y menú del día.
2. **Módulo de Gestión de Insumos y Alertas de Stock:** Control de stock de materias primas y generación automática de notificaciones ante faltantes de insumos en cocina/barra.

---