/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO dbo.TipoDocumento(Nombre) VALUES
	('Order de Compra'),
	('Entrada de MercanciasC'),
	('Factura de Proveedores'),
	('Orden de Venta'),
	('Entrega de MercanciasV'),
	('Factura de Clientes')

  
INSERT INTO dbo.TipoSocioNegocio(Nombre) VALUES
	('Proveedor'),
	('Cliente')


-- TODO: agregar idMoneda y Cuenta Referencias
INSERT INTO dbo.SocioNegocio(Codigo, Nombre, IdTipoSocio, IdMoneda, _Cuenta) VALUES
	('C001', 'Cliente1', 2, 1, '1-1-02-01'),
	('C002', 'Cliente2', 2, 1, '1-1-02-01'),
	('C003', 'Cliente3', 2, 1, '1-1-02-02'),
	('P001', 'Proveedor1', 1, 1, '2-1-01-01'),
	('P002', 'Proveedor2', 1, 1, '2-1-01-01'),
	('P003', 'Proveedor3', 1, 1, '2-1-01-02')

INSERT INTO dbo.Bodega (Codigo, Nombre) VALUES
	('GEN', 'General'),
	('COS', 'Costeo'),
	('DAN', 'Dañado'),
	('CON', 'Consignación')

INSERT INTO dbo.UnidadMedida (Nombre) VALUES
	('Litro'),
	('Gramo')

INSERT INTO dbo.Articulo(Codigo, Nombre, IdUnidadMedida) VALUES
	('PT01', 'Producto 1', 1),
	('PT02', 'Producto 2', 2),
	('PT03', 'Producto 3', 1)