-- =============================================
-- Script Template
-- =============================================

-- =============================================
-- FIN_TipoAsiento
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Primeros valores para TipoAsiento
-----------------------------------------------------------
INSERT INTO dbo.FIN_TipoAsiento VALUES ('AS')

GO
-- =============================================
-- FIN_Identificador
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Primeros valores para Identificadores de cuenta
-----------------------------------------------------------
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Activo', 1)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Pasivo', 2)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Patrimonio', 3)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Ingresos', 4)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Costos', 5)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Gastos', 6)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Otros Ingresos', 7)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Otros Gastos', 8)