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

GO

-- =============================================
-- ERP_Entidades
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 05/10/2012
-- Descripcion: Ingresa una entidad con modulos, contactos, usuarios.
-----------------------------------------------------------
EXEC dbo.ERPSP_ActualizarEntidad 'LCO_Finanzas',
   '<Contactos>
		<Contacto enabled="1" nombre="Telefono" valor="2222-5564" />
		<Contacto enabled="1" nombre="Fax" valor="2222-5565" />
   </Contactos>',
   null,
   '1-1456787498',
   '<Paises>
		<Pais enabled="1">Costa Rica</Pais>
    </Paises>',
    1,
    '<Modulos>
		<Modulo enabled="1" nombre="Finanzas" base="LCO_Finanzas" />
	</Modulos>'
	
EXEC dbo.ERPSP_ActualizarUsuario 'admin',
	'123456',
	'<Contactos>
		<Contacto enabled="1" nombre="Telefono" valor="2888-5564" />
    </Contactos>',
	1,
	'<Modulos>
		<Modulo enabled="1" nombre="Finanzas" tipo="Administrador" />
	</Modulos>',
	'LCO_Finanzas',
	null,
	null

