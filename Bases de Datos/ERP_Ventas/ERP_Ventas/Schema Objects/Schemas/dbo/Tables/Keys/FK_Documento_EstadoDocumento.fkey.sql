<<<<<<< HEAD
﻿ALTER TABLE [dbo].[Documento]  ADD  CONSTRAINT [FK_Documento_EstadoDocumento] FOREIGN KEY([IdEstado])
=======
﻿ALTER TABLE [dbo].[Documento]  ADD  CONSTRAINT [FK_Documento_EstadoDocumento] FOREIGN KEY([IdEstado])
>>>>>>> 674ab780c3ed7d7b8d2a0823c347c88227c2bea0
REFERENCES [dbo].[EstadoDocumento] ([IdEstado]) ON DELETE NO ACTION ON UPDATE NO ACTION;