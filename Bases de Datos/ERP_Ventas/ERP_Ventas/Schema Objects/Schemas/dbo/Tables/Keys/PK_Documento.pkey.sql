﻿ALTER TABLE [dbo].[Documento]
    ADD CONSTRAINT [PK_Documento] PRIMARY KEY CLUSTERED ([IdDocumento] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

