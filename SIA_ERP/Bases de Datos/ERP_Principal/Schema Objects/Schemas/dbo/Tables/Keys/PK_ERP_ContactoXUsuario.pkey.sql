﻿ALTER TABLE [dbo].[ERP_ContactoXUsuario]
    ADD CONSTRAINT [PK_ERP_ContactoXUsuario] PRIMARY KEY CLUSTERED ([IdContacto] ASC, [IdUsuario] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
