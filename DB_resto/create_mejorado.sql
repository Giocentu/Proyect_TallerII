-- =================================================================================
-- SCRIPT DE CREACION MEJORADO PARA ENTITY FRAMEWORK CORE CODE-FIRST
-- Base de datos: proyect_Resto
-- =================================================================================

-- 1. PERSONA (Base para Clientes y Empleados)
CREATE TABLE persona
(
  dni BIGINT NOT NULL,
  nombre VARCHAR(50) NOT NULL,
  apellido VARCHAR(50) NOT NULL,
  email VARCHAR(100) NOT NULL,
  telefono BIGINT NOT NULL,
  password NVARCHAR(255) NOT NULL, -- Ampliado a 255 para soportar hashes (BCrypt/Identity)
  CONSTRAINT pk_persona PRIMARY KEY (dni)
);
GO
ALTER TABLE persona
ADD CONSTRAINT chk_persona_dni_pos CHECK (dni > 0),
    CONSTRAINT chk_persona_telefono_pos CHECK (telefono > 0),
    CONSTRAINT chk_persona_email_formato CHECK (email LIKE '%@%.%');
ALTER TABLE persona ADD CONSTRAINT uq_persona_email UNIQUE (email);
ALTER TABLE persona ADD CONSTRAINT UQ_Persona_Tel UNIQUE (telefono);
GO

-- 2. UBICACION DE MESA
CREATE TABLE ubicacion_mesa
(
  id_ubicacion INT IDENTITY(1,1) NOT NULL,
  ubicacion VARCHAR(30) NOT NULL,
  CONSTRAINT pk_ubicacion PRIMARY KEY (id_ubicacion)
);
ALTER TABLE ubicacion_mesa 
ADD CONSTRAINT uq_ubicacion_name UNIQUE (ubicacion);
GO

-- 3. MESA
CREATE TABLE mesa
(
  id_mesa INT IDENTITY(1,1) NOT NULL,
  nro_mesa INT NOT NULL,
  capacidad INT NOT NULL,
  id_ubicacion INT NOT NULL,
  CONSTRAINT pk_mesa PRIMARY KEY (id_mesa),
  CONSTRAINT fk_mesa_ubicacion FOREIGN KEY (id_ubicacion) REFERENCES ubicacion_mesa(id_ubicacion)
);
ALTER TABLE mesa
ADD CONSTRAINT chk_mesa_capacidad_pos CHECK (capacidad > 0),
    CONSTRAINT chk_nro_mesa CHECK (nro_mesa > 0);
GO

-- 4. ESTADO DE RESERVA
CREATE TABLE estado_reserva
(
  id_estado INT NOT NULL,
  estado VARCHAR(30) NOT NULL,
  CONSTRAINT pk_estado PRIMARY KEY (id_estado)
);
ALTER TABLE estado_reserva
ADD CONSTRAINT chk_estado_id_pos CHECK (id_estado > 0),
    CONSTRAINT uq_estado_name UNIQUE (estado);
GO

-- 5. EVENTO ESPECIAL
CREATE TABLE evento
(
  id_evento INT NOT NULL,
  nombre_evento VARCHAR(50) NOT NULL,
  CONSTRAINT pk_evento PRIMARY KEY (id_evento)
);
ALTER TABLE evento
ADD CONSTRAINT chk_evento_id_pos CHECK (id_evento > 0),
    CONSTRAINT uq_evento_name UNIQUE (nombre_evento);
GO

-- 6. CLIENTE (Relación 1:1 con Persona)
CREATE TABLE cliente
(
  dni_cliente BIGINT NOT NULL,
  CONSTRAINT pk_cliente PRIMARY KEY (dni_cliente),
  CONSTRAINT fk_cliente_persona FOREIGN KEY (dni_cliente) REFERENCES persona(dni) ON DELETE CASCADE
);
GO

-- 7. ROL DE EMPLEADO
CREATE TABLE rol_empleado
(
  id_rol INT NOT NULL,
  descripcion VARCHAR(30) NOT NULL,
  permiso_admin BIT NOT NULL DEFAULT 0,
  CONSTRAINT pk_rolempleado PRIMARY KEY (id_rol)
);
ALTER TABLE rol_empleado
ADD CONSTRAINT chk_rol_id_pos CHECK (id_rol > 0),
    CONSTRAINT uq_rol UNIQUE (descripcion);
GO

-- 8. TURNO DE EMPLEADO
CREATE TABLE turno_empleado
(
  id_turno INT IDENTITY(1,1) NOT NULL,
  inicio_turno TIME NOT NULL,
  fin_turno TIME NOT NULL,
  CONSTRAINT pk_turno PRIMARY KEY (id_turno)
);
ALTER TABLE turno_empleado
ADD CONSTRAINT chk_turno_valido CHECK (fin_turno > inicio_turno OR inicio_turno > fin_turno);
GO

-- 9. EMPLEADO (Mejorado: Clave Primaria Subrogada id_empleado para mapeo óptimo en EF Core)
CREATE TABLE empleado
(
  id_empleado INT IDENTITY(1,1) NOT NULL,
  dni_empleado BIGINT NOT NULL,
  id_rol INT NOT NULL,
  id_turno INT NOT NULL,
  activo_en_rol BIT NOT NULL DEFAULT 1,
  CONSTRAINT pk_empleado PRIMARY KEY (id_empleado),
  CONSTRAINT fk_persona_empleado FOREIGN KEY (dni_empleado) REFERENCES persona(dni),
  CONSTRAINT fk_rol_empleado FOREIGN KEY (id_rol) REFERENCES rol_empleado(id_rol),
  CONSTRAINT fk_turno_empleado FOREIGN KEY (id_turno) REFERENCES turno_empleado(id_turno)
);
GO
-- Asegura que un mismo DNI solo tenga 1 rol activo a la vez
CREATE UNIQUE NONCLUSTERED INDEX UQ_Empleado_RolActivo
ON empleado (dni_empleado)
WHERE (activo_en_rol = 1);
GO

-- 10. RESERVA (FK hacia id_empleado y id_evento NULLABLE)
CREATE TABLE reserva
(
  id_reserva INT IDENTITY(1,1) NOT NULL,
  fecha_reserva DATETIME NOT NULL,
  cant_personas INT NOT NULL,
  fecha_max_cancelacion AS DATEADD(hour, -48, fecha_reserva),
  id_estado INT NOT NULL,
  id_evento INT NULL, -- Flexibilizado: NULL cuando la reserva no es de un evento especial
  dni_cliente BIGINT NOT NULL,
  id_empleado INT NOT NULL,
  CONSTRAINT pk_reserva PRIMARY KEY (id_reserva),
  CONSTRAINT fk_reserva_estado FOREIGN KEY (id_estado) REFERENCES estado_reserva(id_estado),
  CONSTRAINT fk_reserva_evento FOREIGN KEY (id_evento) REFERENCES evento(id_evento),
  CONSTRAINT fk_reserva_cliente FOREIGN KEY (dni_cliente) REFERENCES cliente(dni_cliente),
  CONSTRAINT fk_reserva_empleado FOREIGN KEY (id_empleado) REFERENCES empleado(id_empleado)
);
ALTER TABLE reserva
ADD CONSTRAINT chk_reserva_cant CHECK (cant_personas > 0),
    CONSTRAINT chk_reserva_fecha_futura CHECK (fecha_reserva >= GETDATE());
GO 

-- 11. RESERVA_MESA (Relación Muchos a Muchos entre Reserva y Mesa)
CREATE TABLE reserva_mesa
(
  id_reserva INT NOT NULL,
  id_mesa INT NOT NULL,
  CONSTRAINT pk_reserva_mesa PRIMARY KEY (id_reserva, id_mesa),
  CONSTRAINT fk_reserva FOREIGN KEY (id_reserva) REFERENCES reserva(id_reserva) ON DELETE CASCADE,
  CONSTRAINT fk_mesa FOREIGN KEY (id_mesa) REFERENCES mesa(id_mesa)
);
GO

-- 12. METODO DE PAGO
CREATE TABLE metodo_pago
(
  id_metodo INT NOT NULL,
  forma_pago VARCHAR(30) NOT NULL,
  CONSTRAINT pk_metodo_pago PRIMARY KEY (id_metodo)
);
ALTER TABLE metodo_pago
ADD CONSTRAINT chk_metodo_id_pos CHECK (id_metodo > 0),
    CONSTRAINT uq_metodo_name UNIQUE (forma_pago);
GO

-- 13. PAGOS (Mejorado: monto con DECIMAL(18,2) e id_pago como PK simple)
CREATE TABLE pagos
(
  id_pago INT IDENTITY(1,1) NOT NULL,
  monto DECIMAL(18,2) NOT NULL, -- Corrección del tipo de dato float -> decimal(18,2)
  fecha_pago DATETIME NOT NULL,
  id_metodo INT NOT NULL,
  id_reserva INT NOT NULL,
  CONSTRAINT pk_pagos PRIMARY KEY (id_pago),
  CONSTRAINT uq_pago_reserva UNIQUE (id_reserva),
  CONSTRAINT fk_pago_metodo FOREIGN KEY (id_metodo) REFERENCES metodo_pago(id_metodo),
  CONSTRAINT fk_pago_reserva FOREIGN KEY (id_reserva) REFERENCES reserva(id_reserva) ON DELETE CASCADE  
);
ALTER TABLE pagos
ADD CONSTRAINT chk_pagos_monto CHECK (monto > 0),
    CONSTRAINT chk_pagos_fecha_actual CHECK (fecha_pago <= GETDATE());
GO
