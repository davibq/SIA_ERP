﻿ALTER TABLE [dbo].[FIN_TipoAsiento]
    ADD CONSTRAINT [PK_FIN_TipoAsiento] PRIMARY KEY CLUSTERED ([IdTipoAsiento] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

