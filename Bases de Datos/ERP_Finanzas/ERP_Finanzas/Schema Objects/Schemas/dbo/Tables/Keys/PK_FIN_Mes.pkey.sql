﻿ALTER TABLE [dbo].[FIN_Mes]
    ADD CONSTRAINT [PK_FIN_Mes] PRIMARY KEY CLUSTERED ([IdMes] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
