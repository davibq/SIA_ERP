ALTER TABLE [dbo].[Articulo]
    ADD CONSTRAINT [FK_Articulo_UnidadMedida] FOREIGN KEY ([IdUnidadMedida]) REFERENCES [dbo].[UnidadMedida] ([IdUnidadMedida]) ON DELETE NO ACTION ON UPDATE NO ACTION;

