-- =============================================
-- Script Template
-- =============================================

-- =============================================
-- ERP_Permisos
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Primeros valores para permisos
-----------------------------------------------------------
INSERT INTO dbo.ERP_Permisos VALUES ('Lectura', 'Solo puede leer registros')
INSERT INTO dbo.ERP_Permisos VALUES ('Escritura', 'Puede modificar registros')
INSERT INTO dbo.ERP_Permisos VALUES ('Lectura-Escritura', 'Puede leer y modificar registros')

GO

-- =============================================
-- ERP_TipoUsuario
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Primeros valores para Tipos de usuario
-----------------------------------------------------------
INSERT INTO dbo.ERP_TipoUsuario VALUES ('Administrador')
INSERT INTO dbo.ERP_TipoUsuario VALUES ('Operador')
INSERT INTO dbo.ERP_TipoUsuario VALUES ('Supervisor')
GO

-- =============================================
-- ERP_PermisosXTipoUsuario
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Asocia los primeros permisos a los tipos de usuarios
-----------------------------------------------------------
DECLARE @IdPermiso INT
SELECT @IdPermiso = IdPermisos FROM dbo.ERP_Permisos WHERE Nombre = 'Lectura-Escritura' 

DECLARE @IdTipoUsuario INT
SELECT @IdTipoUsuario = IdTipoUsuario FROM dbo.ERP_TipoUsuario WHERE Nombre = 'Administrador' 

INSERT INTO dbo.ERP_PermisosXTipoUsuario(IdPermisos, IdTipoUsuario) VALUES (@IdPermiso, @IdTipoUsuario)