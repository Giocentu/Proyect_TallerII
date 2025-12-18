USE proyect_Resto;
GO

-- 1. Insertamos los nuevos roles de alto rango
-- Usamos IF NOT EXISTS para no duplicar si le das play varias veces
IF NOT EXISTS (SELECT * FROM rol_empleado WHERE id_rol = 3)
   INSERT INTO rol_empleado (id_rol, descripcion) VALUES (3, 'Gerente');

IF NOT EXISTS (SELECT * FROM rol_empleado WHERE id_rol = 4)
   INSERT INTO rol_empleado (id_rol, descripcion) VALUES (4, 'Dueño');
GO

-- 2. Agregamos tu atributo BIT a la tabla de ROLES
-- 1 = Control Total (Admin/Dueño)
-- 0 = Restringido (Vendedor/Mozo)
ALTER TABLE rol_empleado
ADD permiso_admin BIT NOT NULL DEFAULT 0;
GO


-- Verificamos cómo quedó
SELECT * FROM rol_empleado;