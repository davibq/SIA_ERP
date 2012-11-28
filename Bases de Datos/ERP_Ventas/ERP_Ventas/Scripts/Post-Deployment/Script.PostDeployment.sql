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

INSERT INTO dbo.TipoTransferencia(Nombre) VALUES
	('Recibido'),
	('Efectuado')

INSERT INTO dbo.TipoDocumento(Nombre) VALUES
	('Orden de Compra'),
	('Entrada de Mercancias'),
	('Factura de Proveedores'),
	('Orden de Venta'),
	('Entrega de Mercancias'),
	('Factura de Clientes')

  
INSERT INTO dbo.TipoSocioNegocio(Nombre) VALUES
	('Proveedor'),
	('Cliente')

-- TODO: agregar idMoneda y Cuenta Referencias
INSERT INTO dbo.SocioNegocio(Codigo, Nombre, IdTipoSocio, IdMoneda, _Cuenta, LimiteCredito) VALUES
	('C001', 'Cliente1', 2, 1, '1-1-02-01', 50000),
	('C002', 'Cliente2', 2, 1, '1-1-02-01', 10000),
	('C003', 'Cliente3', 2, 1, '1-1-02-02', 35000),
	('P001', 'Proveedor1', 1, 1, '2-1-01-01', 80000),
	('P002', 'Proveedor2', 1, 1, '2-1-01-01', 60000),
	('P003', 'Proveedor3', 1, 1, '2-1-01-02', 40000)

INSERT INTO dbo.Bodega (Codigo, Nombre) VALUES
	('GEN', 'General'),
	('COS', 'Costeo'),
	('DAN', 'Dañado'),
	('CON', 'Consignación')	

INSERT INTO dbo.UnidadMedida (Nombre) VALUES
	('Litro'),
	('Gramo'),
	('Metro'),
	('Kilogramo'),
	('Ampere'),
	('Kelvin'),
	('Mol'),
	('Candela')

INSERT INTO dbo.Articulo(Codigo, Nombre, Descripcion, IdUnidadMedida) VALUES
	('PT01', 'Producto 1', 'Producto 1', 1),
	('PT02', 'Producto 2', 'Producto 2', 2),
	('PT03', 'Producto 3', 'Producto 3', 1)

INSERT INTO dbo.ArticuloXBodega (IdArticulo, IdBodega, Stock, Comprometido, Solicitado, codCuentasExistencia, codCuentasVentas, codCuentasCostos) VALUES
	(1, 1, 0, 0, 0, '1-1-03-01', '4-1-01', '5-1-01'),
	(1, 2, 0, 0, 0, '1-1-03-01', '4-1-01', '5-1-01'),
	(1, 3, 0, 0, 0, '1-1-03-02', '4-1-01', '5-1-01'),
	(1, 4, 0, 0, 0, '1-1-03-03', '4-1-01', '5-1-01'),
	(2, 1, 0, 0, 0, '1-1-03-01', '4-1-01', '5-1-01'),
	(2, 2, 0, 0, 0, '1-1-03-01', '4-1-01', '5-1-01'),
	(2, 3, 0, 0, 0, '1-1-03-02', '4-1-01', '5-1-01'),
	(2, 4, 0, 0, 0, '1-1-03-03', '4-1-01', '5-1-01'),
	(3, 1, 0, 0, 0, '1-1-03-01', '4-1-01', '5-1-01'),
	(3, 2, 0, 0, 0, '1-1-03-01', '4-1-01', '5-1-01'),
	(3, 3, 0, 0, 0, '1-1-03-02', '4-1-01', '5-1-01'),
	(3, 4, 0, 0, 0, '1-1-03-03', '4-1-01', '5-1-01')


INSERT INTO dbo.CostoXArticuloXBodega (IdArticulo, IdBodega, Costo, FechaActualizacion) VALUES
	(1, 1, 0, GETDATE()),
	(1, 2, 0, GETDATE()),
	(1, 3, 0, GETDATE()),
	(1, 4, 0, GETDATE()),
	(2, 1, 0, GETDATE()),
	(2, 2, 0, GETDATE()),
	(2, 3, 0, GETDATE()),
	(2, 4, 0, GETDATE()),
	(3, 1, 0, GETDATE()),
	(3, 2, 0, GETDATE()),
	(3, 3, 0, GETDATE()),
	(3, 4, 0, GETDATE())

INSERT INTO dbo.Bancos (Nombre, Moneda, NoCuenta, CuentaMayor) VALUES
	('BNCR','USD','11111-1','1-1-01-02'),
	('BNCR','CRC','22222-2','1-1-01-03'),
	('BCR','CRC','22222-0','1-1-01-04')

INSERT INTO dbo.EstadoDocumento (Detalle) VALUES
	('Pendiente'),
	('Cancelado')