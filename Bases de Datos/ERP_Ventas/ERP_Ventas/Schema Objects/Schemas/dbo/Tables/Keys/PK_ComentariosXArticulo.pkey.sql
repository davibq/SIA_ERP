﻿ALTER TABLE [dbo].[ComentariosXArticulo]
    ADD CONSTRAINT [PK_ComentariosXArticulo] PRIMARY KEY CLUSTERED ([IdComentario] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

