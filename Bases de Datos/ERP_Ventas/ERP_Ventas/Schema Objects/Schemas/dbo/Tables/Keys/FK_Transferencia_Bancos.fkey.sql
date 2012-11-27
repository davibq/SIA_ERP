<<<<<<< HEAD
﻿ALTER TABLE [dbo].[Transferencia]  ADD  CONSTRAINT [FK_Transferencia_Bancos] FOREIGN KEY([idBanco])
REFERENCES [dbo].[Bancos] ([idBanco]) ON DELETE NO ACTION ON UPDATE NO ACTION;

=======
﻿ALTER TABLE [dbo].[Transferencia]  ADD  CONSTRAINT [FK_Transferencia_Bancos] FOREIGN KEY([idBanco])
REFERENCES [dbo].[Bancos] ([idBanco]) ON DELETE NO ACTION ON UPDATE NO ACTION;

>>>>>>> 674ab780c3ed7d7b8d2a0823c347c88227c2bea0
